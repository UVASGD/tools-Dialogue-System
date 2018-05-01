using System;
using UnityEngine;

namespace ScriptEditor.Graph {
    public class SequenceNode : ControlNode {

        public SequenceNode() {
        }

        public override void Execute() {
            finished = true;
        }

        public override void Construct() {
            //set information
            name = "Sequence";
            description = "Splits execution based on index";
            // Create Pins
            multiplePins = true;
            execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.Integer));
            valInPins[0].Name = "Index";
            valInPins[0].Description = "The index at which to split execution";

            execOutPins.Add(new ExecOutputPin(this));
            execOutPins.Add(new ExecOutputPin(this));
            execOutPins[1].Name = "Then 1";
            execOutPins[1].Name = "Then 2";
        }
    }
}