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
            inPins.Add(new InputPin(this, VarType.Exec));

            outPins.Add(new OutputPin(this, VarType.Exec));
        }

        public override void Execute()
        {

        }
    }
}

