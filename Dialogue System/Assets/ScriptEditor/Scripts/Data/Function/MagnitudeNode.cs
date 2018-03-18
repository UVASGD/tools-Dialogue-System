using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptEditor.Graph {
    class MagnitudeNode : FunctionNode {

        public override void Construct() {
            description = "Calculates the magnitude of the input vector. Defaults to 0 if nothing is connected.";
            name = "Vector Magnitude";

            // set input and output pins
            inPins.Add(new ValueInputPin(this, nT));
           outPins.Add(new ValueOutputPin(this, VarType.Float));
        }

        public override void Execute() {
            base.Execute();
        }
    }
}
