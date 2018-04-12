using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptEditor.Graph {
    class SplitVectorNode : FunctionNode {

        public override void Construct() {
            string type = "Vector2";

            // set input and output pins
            valInPins.Add(new ValueInputPin(this, nT));
            switch (nT) {
                case VarType.Vector2:
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                    valOutPins[0].Name = "x";
                    valOutPins[1].Name = "y";
                    break;
                case VarType.Vector3:
                    type = "Vector3";
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                    valOutPins[0].Name = "x";
                    valOutPins[1].Name = "y";
                    valOutPins[2].Name = "z";
                    break;
                case VarType.Color:
                    type = "Color";
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                    valOutPins[0].Name = "r";
                    valOutPins[1].Name = "g";
                    valOutPins[2].Name = "b";
                    valOutPins[3].Name = "a";
                    valOutPins[3].Description = "Color transparency";
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
