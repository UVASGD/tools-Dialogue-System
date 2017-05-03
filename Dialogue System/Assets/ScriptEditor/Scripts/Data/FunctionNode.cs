using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptEditor.Graph {
    public class FunctionNode : NodeBase {

        private FunctionType funcType;
        public static Dictionary<FunctionType, List<string>> validCombos;

        public enum FunctionType {
            Magnitude, SplitVector2, SplitVector3, SplitVector4, BuildVector2, BuildVector3, BuildVector4,
            GetLocation
        }

        /// <summary>
        /// Create input and output pins depending on function type
        /// </summary>
        /// <param name="funcType"></param>
        public void Construct(FunctionType funcType, PinType nT) {
            base.SetName(Enum.GetName(typeof(FunctionType), funcType));
            this.funcType = funcType;

            // set name and description
            switch (funcType) {
                case FunctionType.Magnitude:
                    description = "Calculates the absolute value of the input node. Defaults to 0 if nothing is connected.";
                    name = "Vector Magnitude";
                    break;
                case FunctionType.SplitVector2:
                    description = "Breaks a Vector2 into its components";
                    name = "Split Vector2";
                    break;
                case FunctionType.SplitVector3:
                    description = "Breaks a Vector3 into its components";
                    name = "Split Vector3";
                    break;
                case FunctionType.SplitVector4:
                    description = "Breaks a Vector4 into its components";
                    name = "Split Vector4";
                    break;
                case FunctionType.BuildVector2:
                    description = "Breaks a Vector2 into its components";
                    name = "Build Vector2";
                    break;
                case FunctionType.BuildVector3:
                    description = "Breaks a Vector3 into its components";
                    name = "Build Vector3";
                    break;
                case FunctionType.BuildVector4:
                    description = "Breaks a Vector4 into its components";
                    name = "Build Vector4";
                    break;
            }

            // set input and output pins
            switch (funcType) {
                case FunctionType.Magnitude:
                    inPins.Add(new InputPin(this, nT));
                    outPins.Add(new OutputPin(this, PinType.Float));
                    break;
                case FunctionType.SplitVector2:
                    inPins.Add(new InputPin(this, PinType.Vector2));

                    outPins.Add(new OutputPin(this, PinType.Float));
                    outPins.Add(new OutputPin(this, PinType.Float));
                    outPins[0].Name = "x";
                    outPins[1].Name = "y";
                    break;
                case FunctionType.SplitVector3:
                    inPins.Add(new InputPin(this, PinType.Vector3));

                    outPins.Add(new OutputPin(this, PinType.Float));
                    outPins.Add(new OutputPin(this, PinType.Float));
                    outPins[0].Name = "x";
                    outPins[1].Name = "y";
                    outPins[2].Name = "z";
                    break;
                case FunctionType.SplitVector4:
                    inPins.Add(new InputPin(this, PinType.Vector4));

                    outPins.Add(new OutputPin(this, PinType.Float));
                    outPins.Add(new OutputPin(this, PinType.Float));
                    outPins.Add(new OutputPin(this, PinType.Float));
                    outPins.Add(new OutputPin(this, PinType.Float));
                    outPins[0].Name = "r";
                    outPins[1].Name = "g";
                    outPins[2].Name = "b";
                    outPins[2].Name = "a";
                    break;
                case FunctionType.BuildVector2:
                    outPins.Add(new OutputPin(this, PinType.Vector2));

                    inPins.Add(new InputPin(this, PinType.Float));
                    inPins.Add(new InputPin(this, PinType.Float));
                    inPins[0].Name = "x";
                    inPins[1].Name = "y";
                    break;
                case FunctionType.BuildVector3:
                    outPins.Add(new OutputPin(this, PinType.Vector3));

                    inPins.Add(new InputPin(this, PinType.Float));
                    inPins.Add(new InputPin(this, PinType.Float));
                    inPins[0].Name = "x";
                    inPins[1].Name = "y";
                    inPins[2].Name = "z";
                    break;
                case FunctionType.BuildVector4:
                    outPins.Add(new OutputPin(this, PinType.Vector4));

                    inPins.Add(new InputPin(this, PinType.Float));
                    inPins.Add(new InputPin(this, PinType.Float));
                    inPins.Add(new InputPin(this, PinType.Float));
                    inPins.Add(new InputPin(this, PinType.Float));
                    inPins[0].Name = "r";
                    inPins[1].Name = "g";
                    inPins[2].Name = "b";
                    inPins[2].Name = "a";
                    break;
            }
            Resize();
        }

        /// <summary>
        /// calculate the flow of values
        /// </summary>
        /// <param name="e"></param>
        public override void UpdateNode(Event e) {
            base.UpdateNode(e);

            switch (funcType) {
                case FunctionType.Magnitude:
                    switch (outPins[0].varType) {
                        case PinType.Vector2:
                            outPins[0].Value = (inPins[0].isConnected) ?
                                ((Vector2)inPins[0].Value).magnitude
                                : 0;
                            break;
                        case PinType.Vector3:
                            outPins[0].Value = (inPins[0].isConnected) ?
                                ((Vector3)inPins[0].Value).magnitude
                                : 0;
                            break;
                        case PinType.Vector4:
                            outPins[0].Value = (inPins[0].isConnected) ?
                                ((Vector4)inPins[0].Value).magnitude
                                : 0;
                            break;
                    }
                    break;
            }
        }
        static FunctionNode() {
            validCombos = new Dictionary<FunctionType, List<string>>();

            validCombos.Add(FunctionType.Magnitude, new List<string>(
                new string[] { PinType.Vector2.ToString(), PinType.Vector3.ToString(),
                    PinType.Vector4.ToString() }));
        }

    }

}