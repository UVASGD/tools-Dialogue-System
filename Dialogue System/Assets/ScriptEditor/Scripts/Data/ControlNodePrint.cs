using System;

namespace ScriptEditor.Graph{
	public class ControlNodePrint : ControlNode
	{
		public ControlNodePrint()
		{
		}
		public override void Execute() {

		}

		public override void Construct() {
		//set information
		name = "Print Text";
        description = "Prints text to a console";
		// Create Pins
		inPins.Add(new InputPin(this, VarType.Exec));
        inPins.Add(new InputPin(this, VarType.String));
        inPins.Add(new InputPin(this, VarType.Bool));
        inPins.Add(new InputPin(this, VarType.Bool));
        inPins[1].Name = "In String";
        inPins[1].Default = "Hello World";
        inPins[2].Name = "Print to Screen";
        inPins[3].Name = "Print to Console";
        inPins[3].Default = true;

        outPins.Add(new OutputPin(this, VarType.Exec));
		}
	}
}
