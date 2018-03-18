using System;

namespace ScriptEditor.Graph
{
	public class ForLoopNode : ControlNode
	{
		public ForLoopNode ()
		{
		}
		public override void Execute() {

        }

        public override void Construct() {
            // set information
            name = "For Loop";
            description = "Loops through execution based on index, from start to finish.";
            // Create pins
			inPins.Add(new EventInputPin(this));
            inPins.Add(new ValueInputPin(this, VarType.Integer));
            inPins.Add(new ValueInputPin(this, VarType.Integer));
            inPins[1].Name = "First";
            inPins[1].Description = "Initial value of the index";
            inPins[1].Default = 0;
            inPins[2].Name = "Last";
            inPins[2].Description = "Final value of the index";
            inPins[2].Default = 0;

            outPins.Add(new EventOutputPin(this));
           outPins.Add(new ValueOutputPin(this, VarType.Integer));
            outPins.Add(new EventOutputPin(this));
            outPins[0].Name = "Loop Body";
            outPins[0].Description = "The sequence of actions to execute during the loop";
            outPins[1].Name = "Index";
            outPins[1].Description = "The current location of the loop";
            outPins[2].Name = "Completed";
            outPins[2].Description = "The execution path when the loop has finished (i.e. Index>=Last)";
          }
	}
}

