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
            inPins.Add(new EventInputPin(this));

            outPins.Add(new EventOutputPin(this));
        }

        public override void Execute() {
            finished = true;

        }
    }
}

