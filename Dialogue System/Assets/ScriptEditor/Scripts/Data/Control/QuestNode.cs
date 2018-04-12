using System;

namespace ScriptEditor.Graph
{
	public class QuestNode : ControlNode
	{
		public QuestNode ()
		{
		}
		public override void Execute() {
            finished = true;
        }

		public override void Construct() {
		//set information
		name = "Quest";
        description = "Defines a quest and shit";
		// Create Pins
		execInPins.Add(new ExecInputPin(this));

        execOutPins.Add(new ExecOutputPin(this));
		}
	}
}

