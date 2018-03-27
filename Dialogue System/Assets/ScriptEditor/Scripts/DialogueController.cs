using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptEditor.Graph;
using UnityEngine.UI;

namespace ScriptEditor {

    public class DialogueController : MonoBehaviour {
        // ------------- canvas fields --------------
        /// <summary>Dialogue box window. Parent of all ialogue related GUI objects </summary>
        [Tooltip("Dialogue box window. Parent of all dialogue related GUI objects")]
        public Text dialogueArea;
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
        public Button choiceTemplate;
        /// <summary> </summary>
        [Tooltip("Debug Console")]
        public Text debugConsole;

        [Tooltip("How the dialogue pops in and out of view; can be based on world or screen coordinates")]
        public DialogueAnimationType dialogueAnimationType = DialogueAnimationType.Sudden;
        [Tooltip("If the dialogue box should animate between separate dialogues")]
        public bool animateBetweenPages = false;

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

        // ------------- hidden fields --------------
        private NodeGraph dialogue;
        private NodeBase node;
        private bool isCanvasShown = false;
        private Animator animator;
        private List<Button> choices;

        // ------------- static fields ---------------
        /// <summary> node types that modify the canvas and must have it be shown </summary>
        public static List<string> CanvasDependentNodes;

        // Use this for initialization
        void Start() {
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
            if (node) {
                node.Execute();
                if (node.IsFinished) {
                    node = node.GetNextNode();
                    if (node == null) {
                        // finished graph!

                        node = null;
                        dialogue = null;
                    } else {
                        string t = node.GetType().ToString();
                        t = t.Substring(t.LastIndexOf(".") + 1);
                        Debug.Log("CanvasDependentNode? " + t);
                        if (CanvasDependentNodes.Contains(t)){
                            ShowDialogueBox();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Activate a dialogue script
        /// </summary>
        /// <param name="dialogue"></param>
        public void StartDialogue(NodeGraph dialogue) {
            if (this.dialogue != null) return; // already running a script!
            
            // check if script has compiled with no errors
            if (dialogue.compiled) {
                if (!dialogue.hasErrors) {
                    this.dialogue = dialogue;
                    this.node = dialogue.CurrentSubStart;
                } else
                    Debug.LogError("Script \"" + dialogue + "\" contains errors and therefore cannot be run.");
            } else {
                Debug.LogError("Script \"" + dialogue + "\" has not been compiled and therefore cannot run.");
            }
        }

        /// <summary>
        /// Add a choice button to the canvas; 
        /// ONLY called by Choice Nodes
        /// </summary>
        /// <param name="choice"></param>
        public void AddChoice(string choice, bool enabled) {
            bool hideDisabled = ((ChoiceNode)node).hideDisabledChoices;
            if (!enabled && hideDisabled) return;

            // copy TEMPLATE button
            choices.Add(Instantiate(choiceTemplate));
            int n = choices.Count - 1;

            // place new button at next index
            Vector3 offset = Vector2.zero;
            float baseOffset = choiceTemplate.GetComponent<RectTransform>().rect.height
                + 10f;
            switch (((ChoiceNode)node).choiceOrientation) {
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
            choices[n].interactable = enabled;
            choices[n].gameObject.SetActive(true);

            // set button text
            choices[n].GetComponentInChildren<Text>().text = choice;

            // set button click method
            choices[n].onClick.AddListener(delegate { ChoiceClicked(n); });
        }

        // This might be a bit unsavory in terms of efficiency,
        // since we basically create and delete choices everytime we reach
        // ac ChoiceNode... could simplify this to show/hiding the ones we need
        /// <summary> delete choice buttons </summary>
        public void ResetChoiceList() {
            foreach(Button c in choices) {
                GameObject.Destroy(c.gameObject);
            }

            choices.Clear();
        }

        /// <summary>
        /// Input handler for selecting a choice button
        /// </summary>
        /// <param name="index"></param>
        public void ChoiceClicked(int index) {
            //((ChoiceNode)node).choice = index;
            Debug.Log("Clicked[" + index + "]");
        }

        /// <summary>
        /// Start the dialogue box intro animation
        /// </summary>
        private void ShowDialogueBox() {
            if(animator!=null & dialogueAnimationType != DialogueAnimationType.Custom) {
                animator.SetTrigger("Show");
            }
        }

        /// <summary> Hide the dialogue from screen, start outro animation </summary>
        private void HideDialogueBox() {
            if (animator != null & dialogueAnimationType != DialogueAnimationType.Custom) {
                animator.SetTrigger("Hide");
            }
        }

        static DialogueController() {
            CanvasDependentNodes = new List<string>();
            CanvasDependentNodes.Add("DialogueNode");
            CanvasDependentNodes.Add("ChoiceNode");
        }
    }

}
