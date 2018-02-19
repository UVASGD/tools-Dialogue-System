using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptEditor.Graph {
    class BuildVectorNode : FunctionNode {

        public override void Construct() {
            string type = "Vector2";

            // set input and output pins
            outPins.Add(new OutputPin(this,nT));
            switch (nT) {
                case VarType.Vector2:
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins[0].Name = "x";
                    inPins[1].Name = "y";
                    break;
                case VarType.Vector3:
                    type = "Vector3";
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins[0].Name = "x";
                    inPins[1].Name = "y";
                    inPins[2].Name = "z";
                    break;
                case VarType.Vector4:
                    type = "Color";
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins[0].Name = "r";
                    inPins[1].Name = "g";
                    inPins[2].Name = "b";
                    inPins[3].Name = "a";
                    inPins[3].Default = 1.0f;
                    inPins[3].Description = "Color transparency";
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
