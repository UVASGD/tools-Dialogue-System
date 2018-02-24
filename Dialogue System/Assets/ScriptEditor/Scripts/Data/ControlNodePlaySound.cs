using System;

namespace ScriptEditor.Graph{
public class ControlNodePlaySound : ControlNode
{
	public ControlNodePlaySound()
	{
	}

	public override void Execute() {

	}

	public override void Construct() {
	//set information
	name = "Play Sound at Location";
    description = "";
	// Create Pins
	inPins.Add(new InputPin(this, VarType.Exec));
    inPins.Add(new InputPin(this, VarType.String));
    inPins.Add(new InputPin(this, VarType.Bool));
    inPins.Add(new InputPin(this, VarType.Vector3));
    inPins[1].Name = "Path to Sound";
    inPins[1].Description = "Path to what sound to play. (e.g. Assets/Resources/Sounds/Jump)";
    inPins[1].Default = "Assets/";
    inPins[2].Name = "Loop";
    inPins[2].Default = false;
    inPins[3].Name = "Location";

    outPins.Add(new OutputPin(this, VarType.Exec));
    nodeType = NodeType.Dialog;
	}
}
}