using System;
using UnityEngine;

namespace ScriptEditor.Graph {
    public class PlaySoundNode : ControlNode {

        [Tooltip("Path to what sound to play. (e.g. Assets/Resources/Sounds/Jump)")]
        public string pathToSound = "Assets/";

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
            valInPins.Add(new ValueInputPin(this, VarType.Bool));
            valInPins.Add(new ValueInputPin(this, VarType.Vector3));
            valInPins[0].Name = "Loop";
            valInPins[1].Name = "Location";

            execOutPins.Add(new ExecOutputPin(this));
            nodeType = NodeType.Dialog;
        }
    }
}