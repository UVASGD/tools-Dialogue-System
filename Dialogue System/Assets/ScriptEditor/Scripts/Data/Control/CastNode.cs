using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptEditor.Graph {
    public class CastNode : ControlNode {

        public CastNode() {
        }

        public override void Construct() {
            base.SetName("Cast Node");
        }

        public void Construct(VarType input, VarType output) {
            this.Construct();
            description = "Casts " + input.ToString() + " to " + output.ToString() + ".";
            inPins.Add(new ValueInputPin(this, input));
           outPins.Add(new ValueOutputPin(this, output));
        }
    }
}
