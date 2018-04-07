using System;

namespace ScriptEditor.Graph
{
	public class DelayNode : ControlNode
	{
		public DelayNode ()
		{
		}
		public override void Execute() {

        }

        public override void Construct() {
            // set information
            name = "Delay";
            description = "Delays execution for a given number of seconds";
            // Create pins
			inPins.Add(new EventInputPin(this));
            inPins.Add(new ValueInputPin(this, VarType.Float));
            inPins[1].Name = "Duration";
            inPins[1].Default = 1.5f;

            outPins.Add(new ExecOutputPin(this));
          }
	}
}

