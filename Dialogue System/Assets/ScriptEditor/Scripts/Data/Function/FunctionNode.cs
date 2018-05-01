using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptEditor.Graph {
    /// <summary>
    /// Node that retrieves and builds data from variables or other nodes
    /// </summary>
    public class FunctionNode : NodeBase {

        private FunctionType funcType;
        protected VarType /*argumentType*/nT;
        public static Dictionary<FunctionType, List<string>> validCombos;

        public enum FunctionType {
            Magnitude, Length, SplitVector2, SplitVector3, SplitColor, BuildVector2, BuildVector3, BuildColor,
            GetLocation
        }

        public FunctionType SubType() { return funcType; }

        public virtual void Construct() {

        }

        /// <summary>
        /// Create input and output pins depending on function type
        /// </summary>
        /// <param name="funcType"></param>
        public void Construct(FunctionType funcType, VarType nT) {
            //base.SetName(Enum.GetName(typeof(FunctionType), funcType));
            this.funcType = funcType;
            this.nodeType = NodeType.Function;

            // set name and description
            switch (funcType) {
                case FunctionType.Magnitude:
                    description = "Calculates the magnitude of the input vector. Defaults to 0 if nothing is connected.";
                    name = "Vector Magnitude";
                    break;
                case FunctionType.Length:
                    description = "Calculates the length of the passed object";
                    name = "Length";
                    break;
                case FunctionType.SplitVector2:
                    description = "Breaks a Vector2 into its components";
                    name = "Split Vector2";
                    break;
                case FunctionType.SplitVector3:
                    description = "Breaks a Vector3 into its components";
                    name = "Split Vector3";
                    break;
                case FunctionType.SplitColor:
                    description = "Breaks a Color into its components";
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
                case FunctionType.BuildColor:
                    description = "Breaks a Vector4 into its components";
                    name = "Build Vector4";
                    break;
            }

            // set input and output pins
            switch (funcType) {
                case FunctionType.Magnitude:
                    valInPins.Add(new ValueInputPin(this, nT));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                    break;
                case FunctionType.Length:
                    valInPins.Add(new ValueInputPin(this, VarType.Object));

                   valOutPins.Add(new ValueOutputPin(this, VarType.Integer));
                    break;
                case FunctionType.SplitVector2:
                    valInPins.Add(new ValueInputPin(this, VarType.Vector2));

                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                    valOutPins[0].Name = "x";
                    valOutPins[1].Name = "y";
                    break;
                case FunctionType.SplitVector3:
                    valInPins.Add(new ValueInputPin(this, VarType.Vector3));

                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                    valOutPins[0].Name = "x";
                    valOutPins[1].Name = "y";
                    valOutPins[2].Name = "z";
                    break;
                case FunctionType.SplitColor:
                    valInPins.Add(new ValueInputPin(this, VarType.Color));

                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                   valOutPins.Add(new ValueOutputPin(this, VarType.Float));
                    valOutPins[0].Name = "r";
                    valOutPins[1].Name = "g";
                    valOutPins[2].Name = "b";
                    valOutPins[3].Name = "a";
                    break;
                case FunctionType.BuildVector2:
                   valOutPins.Add(new ValueOutputPin(this, VarType.Vector2));

                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins[0].Name = "x";
                    valInPins[1].Name = "y";
                    break;
                case FunctionType.BuildVector3:
                   valOutPins.Add(new ValueOutputPin(this, VarType.Vector3));

                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins[0].Name = "x";
                    valInPins[1].Name = "y";
                    valInPins[2].Name = "z";
                    break;
                case FunctionType.BuildColor:
                   valOutPins.Add(new ValueOutputPin(this, VarType.Color));

                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float));
                    valInPins.Add(new ValueInputPin(this, VarType.Float, 1.0f));
                    valInPins[0].Name = "r";
                    valInPins[1].Name = "g";
                    valInPins[2].Name = "b";
                    valInPins[3].Name = "a";
                    valInPins[3].Description = "Color transparency";
                    break;
            }
            Resize();
        }

        public override void Execute() {
            
        }

        /// <summary> calculate the flow of values </summary>
        public override void Lookup(bool compileTime) {
            base.Lookup(compileTime);
            switch (funcType) {
                default:
                    break;
            }
        }
        static FunctionNode() {
            validCombos = new Dictionary<FunctionType, List<string>>();

            validCombos.Add(
                FunctionType.Magnitude, new List<string>(
                new string[] {
                    VarType.Vector2.ToString(),
                    VarType.Vector3.ToString(),
                    VarType.Color.ToString() }));
        }

    }

}