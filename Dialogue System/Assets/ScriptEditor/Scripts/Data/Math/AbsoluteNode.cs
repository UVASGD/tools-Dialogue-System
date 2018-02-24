using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptEditor.Graph {
    class AbsoluteNode : MathNode {

        public override void Construct() {
            description = "Calculates the absolute value of the input node. Defaults to 0 if nothing is connected.";
            name = "Absolute Value Function";
            splashText = "ABS";

            //inPins.Add(new InputPin(this, nT));
            //outPins.Add(new OutputPin(this, nT));
            Resize();
        }

        public override void Execute() {
            base.Execute();
        }
    }
}
