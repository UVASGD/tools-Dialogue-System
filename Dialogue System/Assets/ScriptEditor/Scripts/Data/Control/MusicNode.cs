using System;
using UnityEngine;

namespace ScriptEditor.Graph {
    public class MusicNode : ControlNode {

        [Tooltip("Path to what song to play. (e.g. Assets/Resources/Music/Main Theme)")]
        public string pathToSong = "Assets/";

        [Tooltip("Whether or not to make the main music stop playing")]
        public bool silenceMainMusic;

        public MusicNode() {
        }

        public override void Execute() {
            base.Execute();
        }

        public override void Construct() {
            //set information
            name = "Play Music";
            description = "Sets the bgm of the scene";
            // Create Pins
            execInPins.Add(new ExecInputPin(this));
            //valInPins.Add(new ValueInputPin(this, VarType.String, "Assets/"));
            valInPins.Add(new ValueInputPin(this, VarType.Bool, false));
            valInPins.Add(new ValueInputPin(this, VarType.Vector3));
            valInPins.Add(new ValueInputPin(this, VarType.Float, 100f));
            valInPins[0].Name = "Loop";
            valInPins[1].Name = "Location";
            valInPins[1].Description = "The location to play the song in World Coordinates";
            valInPins[2].Name = "Range";
            valInPins[2].Description = "The maximum distance the song can be heard";

            execOutPins.Add(new ExecOutputPin(this));
        }

        protected override void Setup() {

        }
    }
}
