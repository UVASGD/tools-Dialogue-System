using System;

namespace ScriptEditor.Graph {
    public class PlaySoundNode : ControlNode {
        public PlaySoundNode() {
        }

        public override void Execute() {
            finished = true;
        }

        public override void Construct() {
            //set information
            name = "Play Sound at Location";
            description = "";
            // Create Pins
            execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.String));
            valInPins.Add(new ValueInputPin(this, VarType.Bool));
            valInPins.Add(new ValueInputPin(this, VarType.Vector3));
            valInPins[0].Name = "Path to Sound";
            valInPins[0].Description = "Path to what sound to play. (e.g. Assets/Resources/Sounds/Jump)";
            valInPins[0].Default = "Assets/";
            valInPins[1].Name = "Loop";
            valInPins[1].Default = false;
            valInPins[2].Name = "Location";

            execOutPins.Add(new ExecOutputPin(this));
            nodeType = NodeType.Dialog;
        }
    }
}