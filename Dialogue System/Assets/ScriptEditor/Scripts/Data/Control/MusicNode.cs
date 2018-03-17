using System;

namespace ScriptEditor.Graph
{
    public class MusicNode : ControlNode
    {
        public MusicNode()
        {
        }

        public override void Execute()
        {

        }

        public override void Construct()
        {
            //set information
            name = "Play Music";
            description = "Sets the bgm of the scene";
            // Create Pins
            inPins.Add(new InputPin(this, VarType.Exec));
            inPins.Add(new InputPin(this, VarType.String));
            inPins.Add(new InputPin(this, VarType.Bool));
            inPins.Add(new InputPin(this, VarType.Vector3));
            inPins.Add(new InputPin(this, VarType.Float));
            inPins[1].Name = "Path to Song";
            inPins[1].Description = "Path to what song to play. (e.g. Assets/Resources/Music/Main Theme)";
            inPins[1].Default = "Assets/";
            inPins[2].Name = "Loop";
            inPins[2].Default = false;
            inPins[3].Name = "Location";
            inPins[3].Description = "The location to play the song in World Coordinates";
            inPins[4].Name = "Range";
            inPins[4].Description = "The maximum distance the song can be heard";
            inPins[4].Default = 100;

            outPins.Add(new OutputPin(this, VarType.Exec));
        }
    }
}
