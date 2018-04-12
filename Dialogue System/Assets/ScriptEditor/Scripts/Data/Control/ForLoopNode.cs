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
			execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.Integer));
            valInPins.Add(new ValueInputPin(this, VarType.Integer));
            valInPins[0].Name = "First";
            valInPins[0].Description = "Initial value of the index";
            valInPins[0].Default = 0;
            valInPins[1].Name = "Last";
            valInPins[1].Description = "Final value of the index";
            valInPins[1].Default = 0;

            execOutPins.Add(new ExecOutputPin(this));
            valOutPins.Add(new ValueOutputPin(this, VarType.Integer));
            execOutPins.Add(new ExecOutputPin(this));
            execOutPins[0].Name = "Loop Body";
            execOutPins[0].Description = "The sequence of actions to execute during the loop";
            valOutPins[0].Name = "Index";
            valOutPins[0].Description = "The current location of the loop";
            execOutPins[1].Name = "Completed";
            execOutPins[1].Description = "The execution path when the loop has finished (i.e. Index>=Last)";
          }
	}
}

