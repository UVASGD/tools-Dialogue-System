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
		inPins.Add(new EventInputPin(this));

        outPins.Add(new EventOutputPin(this));
		}
	}
}

