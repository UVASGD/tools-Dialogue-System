using System;
using UnityEngine;

namespace ScriptEditor.Graph
{
    public class SetSubStartNode : ControlNode
    {
        public string newStart;

        public SetSubStartNode()
        {
        }

        public override void Construct()
        {
            // set information
            name = "Set Sub Start";
            description = "Changes where the dialogue script starts the next time it is activated";
            // Create pins
            execInPins.Add(new ExecInputPin(this));

            execOutPins.Add(new ExecOutputPin(this));
        }

        public override void Execute() {
            finished = true;

        }
    }
}

