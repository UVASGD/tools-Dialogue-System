using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptEditor.Graph {
    class BuildVectorNode : FunctionNode {

        public override void Construct() {
            string type = "Vector2";

            // set input and output pins
           valOutPins.Add(new ValueOutputPin(this,nT));
            switch (nT) {
                case VarType.Vector2:
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins[0].Name = "x";
                    valInPins[1].Name = "y";
                    break;
                case VarType.Vector3:
                    type = "Vector3";
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins[0].Name = "x";
                    valInPins[1].Name = "y";
                    valInPins[2].Name = "z";
                    break;
                case VarType.Color:
                    type = "Color";
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins[0].Name = "r";
                    valInPins[1].Name = "g";
                    valInPins[2].Name = "b";
                    valInPins[3].Name = "a";
                    valInPins[3].Default = 1.0f;
                    valInPins[3].Description = "Color transparency";
                    break;
            }

            description = "Builds a " + type + " from its components";
            name = "Build "+type;

            Resize();
        }

        public override void Execute() {
            base.Execute();
        }
    }
}
