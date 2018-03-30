using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptEditor.Graph;
using UnityEngine.UI;

namespace ScriptEditor {

    [RequireComponent(typeof(AudioSource))]
    public class DialogueController : MonoBehaviour {
        // ------------- canvas fields --------------
        /// <summary>Dialogue box window. Parent of all ialogue related GUI objects </summary>
        [Tooltip("Dialogue box window. Parent of all dialogue related GUI objects")]
        public GameObject dialogueArea;
        /// <summary> Dialogue textbox; body of the dialogue </summary>
        [Tooltip("Main body of the dialogue")]
        public Text outputTextbox;
        /// <summary> Header for the dialogue box </summary>
        [Tooltip("Header for the dialogue box")]
        public Text outputHeader;
        /// <summary> </summary>
        [Tooltip("Dialogue continuation indicator")]
        public Image continueIndicator;
        /// <summary> Icon for the current speaker </summary>
        [Tooltip("Icon for the current speaker")]
        public Image speakerIcon;
        /// <summary> Button template for dialogue choices </summary>
        [Tooltip("Button template for dialogue choices")]
        public GameObject choiceTemplate;
        /// <summary> </summary>
        [Tooltip("Debug Console")]
        public Text debugConsole;

        [Tooltip("How the dialogue pops in and out of view; can be based on world or screen coordinates")]
        public DialogueAnimationType dialogueAnimationType = DialogueAnimationType.Sudden;
        [Tooltip("If the dialogue box should animate between separate dialogues")]
        public bool animateBetweenPages = false;

        [Tooltip("Sound for clicking continue on a dialogue page")]
        public AudioClip PressSound;
        [Tooltip("Sound for showing text to screen")]
        public AudioClip TextSound;
        [Tooltip("Sound for showing the dialogue window")]
        public AudioClip ShowDialogueSound;
        [Tooltip("Sound for hiding the dialogue window")]
        public AudioClip HideDialogueSound;

        /// <summary>
        /// How the dialogue pops in and out of view
        /// </summary>
        public enum DialogueAnimationType {
            FromBottom, FromTop, FromLeft, FromRight, FromCenter, // Screen dependent
            ActorPopup,                                           // World dependent
            Fade, Sudden, Custom                                  // Independent
        }

        // ------------- global fields --------------
        /// <summary> use this to prevent the player from moving </summary>
        public bool isPlayerLocked = false;
        public VariableDictionary sceneVariables, gameVariables;

        // ------------- hidden fields --------------
        private NodeGraph currentScript;
        public NodeBase currentNode;
        private bool isCanvasShown = false;
        private Animator animator;
        private List<GameObject> choices;
        private AudioSource audioSrc;

        // ------------- static fields ---------------
        /// <summary> node types that modify the canvas and must have it be shown </summary>
        public static List<string> CanvasDependentNodes;

        // Use this for initialization
        void Start() {
            sceneVariables = new VariableDictionary();
            gameVariables = new VariableDictionary();
            audioSrc = GetComponent<AudioSource>();
            audioSrc.loop = false;

            if (dialogueArea != null) {
                // attach animator to area
                // attach animation

                // put dialogue area in default area
                // (if the initial animtion state is not properly set)
                // since these animations are made by us, this might not be necessary
            }

            // hide shit
            if (choiceTemplate) choiceTemplate.gameObject.SetActive(false);
            if (continueIndicator) continueIndicator.gameObject.SetActive(false);
        }
        
        void Update() {
            if (currentNode) {
                currentNode.Execute();
                if (currentNode.IsFinished) {
                    currentNode = currentNode.GetNextNode();
                    if (currentNode == null) {
                        // finished graph!

                        currentNode = null;
                        currentScript = null;
                    } else {
                        if (isCanvasDependent(currentNode)){
                            ShowDialogueBox();
                        } else if(isCanvasShown){
                            HideDialogueBox();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Activate a dialogue script
        /// </summary>
        /// <param name="script"></param>
        public void StartDialogue(NodeGraph script) {
            if (this.currentScript != null) return; // already running a script!
            
            // check if script has compiled with no errors
            if (script.compiled) {
                if (!script.hasErrors) {
                    this.currentScript = script;
                    this.currentNode = script.CurrentSubStart;
                } else
                    Debug.LogError("Script \"" + script + "\" contains errors and therefore cannot be run.");
            } else {
                Debug.LogError("Script \"" + script + "\" has not been compiled and therefore cannot run.");
            }
        }

        /// <summary>
        /// Add a choice button to the canvas; 
        /// ONLY called by Choice Nodes
        /// </summary>
        /// <param name="choice"></param>
        public void AddChoice(string choice, bool enabled) {
            bool hideDisabled = ((ChoiceNode)currentNode).hideDisabledChoices;
            if ((!enabled && hideDisabled) || !choiceTemplate) return;
            if (!choiceTemplate.GetComponentInChildren<Button>()) return;

            // copy TEMPLATE button
            choices.Add(Instantiate(choiceTemplate));
            int n = choices.Count - 1;
            Button button = choices[n].GetComponentInChildren<Button>();

            // place new button at next index
            Vector3 offset = Vector2.zero;
            float baseOffset = choiceTemplate.GetComponent<RectTransform>().rect.height + 10f;
            switch (((ChoiceNode)currentNode).choiceOrientation) {
                case ChoiceNode.ChoiceOrientation.VerticalFromBottom:
                    offset = new Vector3(0, baseOffset, 0);
                    break;
                case ChoiceNode.ChoiceOrientation.VerticalFromTop:
                    offset = new Vector3(0, -baseOffset, 0);
                    break;
                case ChoiceNode.ChoiceOrientation.HorizontalFromLeft:
                    offset = new Vector3(baseOffset, 0, 0);
                    break;
                case ChoiceNode.ChoiceOrientation.HorizontalFromRight:
                    offset = new Vector3(-baseOffset, 0, 0);
                    break;
                case ChoiceNode.ChoiceOrientation.Radial:
                    // complicated radial math
                    // your boi can't do that shit
                    break;
            }
            choices[n].transform.position += n*offset;

            // set button grayed out
            button.interactable = enabled;
            choices[n].gameObject.SetActive(true);

            // set button text
            choices[n].GetComponentInChildren<Text>().text = choice;

            // set button click method
            button.onClick.AddListener(delegate { ChoiceClicked(n); });
        }

        // This might be a bit unsavory in terms of efficiency,
        // since we basically create and delete choices everytime we reach
        // ac ChoiceNode... could simplify this to show/hiding the ones we need
        /// <summary> delete choice buttons </summary>
        public void ResetChoiceList() {
            foreach(GameObject c in choices) Destroy(c);
            choices.Clear();
        }

        /// <summary>
        /// returns the amount of time left on the choice node to use for display;
        /// returns 0 if not currently on ChoiceNode or time is not being used
        /// </summary>
        public float GetChoiceTime() {
            if (!(currentNode is ChoiceNode)) return 0;
            ChoiceNode n = (ChoiceNode)currentNode;
            return n.timed? n.timerLength - n.TotalTime : 0;
        }

        /// <summary>
        /// Input handler for selecting a choice button
        /// </summary>
        /// <param name="c"></param>
        public void ChoiceClicked(int c) {
            //((ChoiceNode)node).choice = index;
            Debug.Log("Clicked[" + c + "]");
        }

        /// <summary>
        /// continue or skip display of dialogue
        /// </summary>
        public void ContinueDialogue() {
            //if (!(node is DialogueNode) && !(node is ChoiceNode)) return;
            if (!isCanvasDependent(currentNode)) return;

            audioSrc.clip = PressSound;
            audioSrc.Play();

            if(currentNode is DialogueNode) {
                ((DialogueNode)currentNode).SoftSkip();
            } else if(currentNode is ChoiceNode) {

            }
        }

        /// <summary>
        /// Skip dialogue immediately
        /// </summary>
        public void SkipDialogue() {
            if (!isCanvasDependent(currentNode)) return;
            if (!isCanvasDependent(currentNode)) return;

            audioSrc.clip = PressSound;
            audioSrc.Play();

            if (currentNode is DialogueNode) {

            } else if (currentNode is ChoiceNode) {

            }
        }

        /// <summary>
        /// if script is currently running, immediately stop
        /// </summary>
        public void ForceQuit() {

        }

        public AudioSource GetAudioSrc() {
            return audioSrc;
        }

        /// <summary>
        /// Start the dialogue box intro animation
        /// </summary>
        private void ShowDialogueBox() {
            if(animator!=null & dialogueAnimationType != DialogueAnimationType.Custom) {
                isCanvasShown = true;
                animator.SetTrigger("Show");
                audioSrc.clip = ShowDialogueSound;
                audioSrc.Play();
            }
        }

        /// <summary> Hide the dialogue from screen, start outro animation </summary>
        private void HideDialogueBox() {
            if (animator != null & dialogueAnimationType != DialogueAnimationType.Custom) {
                animator.SetTrigger("Hide");
                audioSrc.clip = HideDialogueSound;
                audioSrc.Play();
            }
        }

        private static bool isCanvasDependent(NodeBase node) {
            string t = node.GetType().ToString();
            t = t.Substring(t.LastIndexOf(".") + 1);
            Debug.Log("CanvasDependentNode? " + t);
            return CanvasDependentNodes.Contains(t);
        }

        static DialogueController() {
            CanvasDependentNodes = new List<string>();
            CanvasDependentNodes.Add("DialogueNode");
            CanvasDependentNodes.Add("ChoiceNode");
        }
    }

}
