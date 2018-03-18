using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptEditor.Graph;
using UnityEngine.UI;

namespace ScriptEditor {

    public class DialogueController : MonoBehaviour {

        // ------------- canvas fields --------------
        [Tooltip("Dialogue textbox")]
        public Text outputTextbox;
        [Tooltip("Header for the dialoguebox")]
        public Text outputHeader;
        [Tooltip("Dialogue continuation indicator")]
        public Image continueIndicator;
        [Tooltip("Icon for the current speaker")]
        public Image speakerIcon;

        [Tooltip("Debug Console")]
        public Text debugConsole;

        // ------------- hidden fields --------------
        public NodeGraph dialogue;
        public NodeBase node;

        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {
            if (node) {
                node.Execute();
                if (node.IsFinished) {
                    node = node.GetNextNode();
                    if (node == null) {
                        // finished graph!

                        node = null;
                        //dialogue.reset();
                        dialogue = null;
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

            Debug.Log("Step 0: "+dialogue);
            // check if script has compiled with no errors
            if (dialogue.compiled) {
                Debug.Log("Step 1 complete!");
                if (!dialogue.hasErrors) {
                    Debug.LogWarning("Donuts are real");
                    this.dialogue = dialogue;
                    this.node = dialogue.CurrentSubStart;
                } else
                    Debug.LogError("Script \"" + dialogue + "\" contains errors and therefore cannot be run.");
            } else {
                Debug.LogError("Script \"" + dialogue + "\" has not been compiled and therefore cannot run.");
            }
        }
    }

}
