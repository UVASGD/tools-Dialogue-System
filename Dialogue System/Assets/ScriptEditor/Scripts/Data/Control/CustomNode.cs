using System;

namespace ScriptEditor.Graph
{
	public class ControlNodeCustom : ControlNode
	{
		public ControlNodeCustom ()
		{
		}

		public override void Execute() {

        }

        public override void Construct() {
            // set information
            name = "Custom";
            description = "Calls a custom function";
            // Create pins
			inPins.Add(new InputPin(this, VarType.Exec));
            outPins.Add(new OutputPin(this, VarType.Exec));
          }
	}
}

