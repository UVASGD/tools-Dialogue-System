using System;
using UnityEngine;

namespace ScriptEditor.Graph {
    public class CustomNode : ControlNode {
        public CustomNode() {
        }

        public override void Execute() {
            finished = true;

        }

        public override void Construct() {
            // set information
            name = "Custom";
            description = "Calls a custom function";
            // Create pins
            execInPins.Add(new ExecInputPin(this));
            execOutPins.Add(new ExecOutputPin(this));
        }
    }
}

