using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptEditor.Graph {
    class SplitVectorNode : FunctionNode {

        public override void Construct() {
            string type = "Vector2";

            // set input and output pins
            inPins.Add(new ValueInputPin(this, nT));
            switch (nT) {
                case VarType.Vector2:
                   outPins.Add(new ValueOutputPin(this, VarType.Float));
                   outPins.Add(new ValueOutputPin(this, VarType.Float));
                    outPins[0].Name = "x";
                    outPins[1].Name = "y";
                    break;
                case VarType.Vector3:
                    type = "Vector3";
                   outPins.Add(new ValueOutputPin(this, VarType.Float));
                   outPins.Add(new ValueOutputPin(this, VarType.Float));
                    outPins[0].Name = "x";
                    outPins[1].Name = "y";
                    outPins[2].Name = "z";
                    break;
                case VarType.Color:
                    type = "Color";
                   outPins.Add(new ValueOutputPin(this, VarType.Float));
                   outPins.Add(new ValueOutputPin(this, VarType.Float));
                   outPins.Add(new ValueOutputPin(this, VarType.Float));
                   outPins.Add(new ValueOutputPin(this, VarType.Float));
                    outPins[0].Name = "r";
                    outPins[1].Name = "g";
                    outPins[2].Name = "b";
                    outPins[3].Name = "a";
                    outPins[3].Description = "Color transparency";
                    break;
            }

            description = "Breaks a " + type + " into its components";
            name = "Split "+type;

            Resize();
        }

        public override void Execute() {
            base.Execute();
        }
    }
}
