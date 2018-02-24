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
                }
            }
        }

        void StartDialogue(NodeGraph dialogue)
        {
            this.dialogue = dialogue;
            this.node = dialogue.CurrentSubStart;
        }
    }

}
