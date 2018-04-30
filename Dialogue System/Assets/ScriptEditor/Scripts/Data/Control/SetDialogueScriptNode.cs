using System;
using UnityEngine;

namespace ScriptEditor.Graph {
    public class SetDialogueScriptNode : ControlNode {
        public NodeGraph newScript;

        public SetDialogueScriptNode() {
        }

        public override void Construct() {
            // set information
            name = "Set Dialogue Script";
            description = "Sets the indicated dialogue script attached to the given actor.";
            // Create pins
            execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.Actor));
            valInPins[0].Name = "Actor";
            valInPins[0].Description = "The Actor whose script will be changed";

            execOutPins.Add(new ExecOutputPin(this));
        }

        public override void Execute() {
            finished = true;

        }
    }
}