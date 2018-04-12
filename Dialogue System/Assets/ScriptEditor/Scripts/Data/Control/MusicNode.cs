using System;

namespace ScriptEditor.Graph
{
    public class MusicNode : ControlNode
    {
        public MusicNode()
        {
        }

        public override void Execute() {
            base.Execute();
        }

        public override void Construct()
        {
            //set information
            name = "Play Music";
            description = "Sets the bgm of the scene";
            // Create Pins
            execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.String));
            valInPins.Add(new ValueInputPin(this, VarType.Bool));
            valInPins.Add(new ValueInputPin(this, VarType.Vector3));
            valInPins.Add(new ValueInputPin(this, VarType.Float));
            valInPins[0].Name = "Path to Song";
            valInPins[0].Description = "Path to what song to play. (e.g. Assets/Resources/Music/Main Theme)";
            valInPins[0].Default = "Assets/";
            valInPins[1].Name = "Loop";
            valInPins[1].Default = false;
            valInPins[2].Name = "Location";
            valInPins[2].Description = "The location to play the song in World Coordinates";
            valInPins[3].Name = "Range";
            valInPins[3].Description = "The maximum distance the song can be heard";
            valInPins[3].Default = 100;

            execOutPins.Add(new ExecOutputPin(this));
        }

        protected override void Setup() {

        }
    }
}
