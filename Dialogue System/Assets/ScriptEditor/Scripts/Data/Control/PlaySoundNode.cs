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
            inPins.Add(new EventInputPin(this));
            inPins.Add(new ValueInputPin(this, VarType.String));
            inPins.Add(new ValueInputPin(this, VarType.Bool));
            inPins.Add(new ValueInputPin(this, VarType.Vector3));
            inPins[1].Name = "Path to Sound";
            inPins[1].Description = "Path to what sound to play. (e.g. Assets/Resources/Sounds/Jump)";
            inPins[1].Default = "Assets/";
            inPins[2].Name = "Loop";
            inPins[2].Default = false;
            inPins[3].Name = "Location";

            outPins.Add(new EventOutputPin(this));
            nodeType = NodeType.Dialog;
        }
    }
}