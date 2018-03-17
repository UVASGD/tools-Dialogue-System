using System;

namespace ScriptEditor.Graph
{
	public class ControlNodeQuest : ControlNode
	{
		public ControlNodeQuest ()
		{
		}
		public override void Execute() {

		}

		public override void Construct() {
		//set information
		name = "Quest";
        description = "Defines a quest and shit";
		// Create Pins
		inPins.Add(new InputPin(this, VarType.Exec));

        outPins.Add(new OutputPin(this, VarType.Exec));
		}
	}
}

