using System;

namespace ScriptEditor.Graph
{
	public class ControlNodeSequence : ControlNode
	{
		public ControlNodeSequence ()
		{
		}
		public override void Execute() {

		}

		public override void Construct() {
		//set information
		name = "Sequence";
        description = "Splits execution based on index";
		// Create Pins
		multiplePins = true;
        inPins.Add(new InputPin(this, VarType.Exec));
        inPins.Add(new InputPin(this, VarType.Integer));
        inPins[1].Name = "Index";
        inPins[1].Description = "The index at which to split execution";

        outPins.Add(new OutputPin(this, VarType.Exec));
        outPins.Add(new OutputPin(this, VarType.Exec));
        outPins[1].Name = "Then 1";
        outPins[1].Name = "Then 2";
		}
	}
}

