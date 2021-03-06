using System;
using UnityEngine;

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
			execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.Float, 1.5f));
            valInPins[0].Name = "Duration";

            execOutPins.Add(new ExecOutputPin(this));
          }
	}
}

