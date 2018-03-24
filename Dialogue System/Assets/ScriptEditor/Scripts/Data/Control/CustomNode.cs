using System;

namespace ScriptEditor.Graph
{
	public class CustomNode : ControlNode
	{
		public CustomNode ()
		{
		}

		public override void Execute() {
            finished = true;

        }

        public override void Construct() {
            // set information
            name = "Custom";
            description = "Calls a custom function";
            // Create pins
			inPins.Add(new EventInputPin(this));
            outPins.Add(new EventOutputPin(this));
          }
	}
}

