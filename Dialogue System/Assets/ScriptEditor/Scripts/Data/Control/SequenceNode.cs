using System;

namespace ScriptEditor.Graph
{
	public class SequenceNode : ControlNode
	{
		public SequenceNode ()
		{
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
        inPins.Add(new EventInputPin(this));
        inPins.Add(new ValueInputPin(this, VarType.Integer));
        inPins[1].Name = "Index";
        inPins[1].Description = "The index at which to split execution";

        outPins.Add(new ExecOutputPin(this));
        outPins.Add(new ExecOutputPin(this));
        outPins[1].Name = "Then 1";
        outPins[1].Name = "Then 2";
		}
	}
}

