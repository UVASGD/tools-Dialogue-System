using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptEditor.Graph;

namespace ScriptEditor
{

    public class DialogueController : MonoBehaviour
    {
        NodeGraph dialogue;
        NodeBase node;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (node)
            {
                node.Execute();
                if (node.IsFinished)
                {
                    node = node.GetNextNode();
                    if(node == null) {
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
        void StartDialogue(NodeGraph dialogue){
            if (this.dialogue != null) return; // already running a script!
            this.dialogue = dialogue;
            this.node = dialogue.CurrentSubStart;
        }
    }

}
