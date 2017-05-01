using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptEditor.Graph {
    public class MathNode : NodeBase {

        /// <summary> possible combinations of functions and value types that can be created </summary>
        public static Dictionary<OpType, List<string>> validCombos;
        OpType op = OpType.Abs;

        /// <summary> What function is performed by node </summary>
        public enum OpType {
            Abs, SQRT, Add, Subtract, Multiply, Divide, And, Or, Not, Xor,  POW
        }
        
        /// <summary>
        /// Build pins for node
        /// </summary>
        /// <param name="title"></param>
        public void Construct(OpType type, PinType nT, int inCount) {
            base.Construct(GetTitle(type));
            op = type;
            bool isFloat = nT == PinType.Float;

            // set description
            switch (type) {
                case OpType.Add:
                    description = "Adds all input nodes. Unconnected nodes are not applied from the top. " +
                        "If nothing is connected, the default value is 0.";
                    name = "Addition Function";
                    break;
                case OpType.Subtract:
                    description = "Subtracts all input nodes from the first connected input from the top. " +
                        "If nothing is connected, the default value is 0.";
                    name = "Subtraction Function";
                    break;
                case OpType.Multiply:
                    description = "Multiplies all input nodes to the first connected input from the top. " +
                        "If nothing is connected, the default value is 0.";
                    name = "Multiplication Function";
                    break;
                case OpType.Divide:
                    description = "Divides the first connected input by all succeeding input. If nothing is connected, " +
                        "the default value is 0. Avoids dividing by 0";
                    name = "Division Function";
                    break;
                case OpType.Abs:
                    description = "Calculates the absolute value of the input node. Defaults to 0 if nothing is connected.";
                    name = "Absolute Value Function";
                    break;
                case OpType.SQRT:
                    description = "Calculates the square root of the input node. Defaults to 0 if nothing is connected.";
                    name = "Squareroot Function";
                    break;
                case OpType.Not:
                    description = "Logically negates input node. Defaults to false if nothing is connected.";
                    name = "Logical Negation";
                    break;
                case OpType.And:
                    description = "Performs AND on each subsequent connected input node. Defaults to false if nothing is connected.";
                    name = "Boolean AND Function";
                    break;
                case OpType.Or:
                    description = "Performs OR on each subsequent connected input node. Defaults to false if nothing is connected.";
                    name = "Boolean OR Function";
                    break;
                case OpType.Xor:
                    description = "Performs XOR on each subsequent connected input node. Defaults to false if nothing is connected.";
                    name = "Boolean XOR Function";
                    break;
                case OpType.POW:
                    description = "Calculates the first value to the power of the second. Second value defaults to 2 if not connected." +
                        " First value defaults to 0. If nothing is connected, the default output is 0.";
                    name = "Power Function";
                    break;
            }

            // create pins;
            switch (type) {
                case OpType.Add:
                case OpType.Subtract:
                case OpType.Multiply:
                case OpType.Divide:
                    for (int i = 0; i < inCount; i++)
                        inPins.Add(new InputPin(this, nT));
                    outPins.Add(new OutputPin(this, nT));
                    multiplePins = true;
                    break;
                case OpType.Abs:
                case OpType.SQRT:
                    inPins.Add(new InputPin(this, nT));
                    outPins.Add(new OutputPin(this, nT));
                    break;
                case OpType.POW:
                    inPins.Add(new InputPin(this, nT));
                    inPins.Add(new InputPin(this, nT));
                    outPins.Add(new OutputPin(this, nT));
                    break;
                case OpType.Not:
                    inPins.Add(new InputPin(this, PinType.Bool));
                    outPins.Add(new OutputPin(this, PinType.Bool));
                    break;
                case OpType.And:
                case OpType.Or:
                case OpType.Xor:
                    for (int i = 0; i < inCount; i++)
                        inPins.Add(new InputPin(this, PinType.Bool));
                    outPins.Add(new OutputPin(this, PinType.Bool));
                    multiplePins = true;
                    break;
            }

            if (multiplePins) description += " Maximum pins allowed is 16.";
            Resize();
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Function;
        }

#if UNITY_EDITOR
        public override void DrawNode(Event e, Rect viewRect) {
            base.DrawNode(e, viewRect);

            Debug.Log(name + ": " + body);
            DrawPins();
        }
#endif

        private void ProcessContextMenu(Event e) {

        }

        private void ContextCallback(object obj) {

        }

        /// <summary> first active inPin </summary>
        private int Start (){
                for (int i = 0; i < inPins.Count; i++)
                    if (inPins[i].isConnected)
                        return i;
                return inPins.Count;
        }

        #region Override
        /// <summary> calculate value of output </summary>
        public override void UpdateNode(Event e) {
            base.UpdateNode(e);

            int start = Start();
            switch (op) {
                case OpType.Abs:
                    switch (outPins[0].varType) {
                        case PinType.Integer:
                        case PinType.Float:
                            outPins[0].Value = inPins[0].isConnected ?
                                (Mathf.Abs((float)inPins[0].Value)) : 0;
                            break;
                        case PinType.Vector2:
                            if (inPins[0].isConnected) {
                                Vector2 tmp = (Vector2)inPins[0].Value;
                                outPins[0].Value = new Vector2(Mathf.Abs(tmp.x),
                                                         Mathf.Abs(tmp.y));
                            } else outPins[0].Value = Vector2.zero;
                            break;
                    }
                    break;
                case OpType.Add:
                    switch (outPins[0].varType) {
                        case PinType.Integer:
                        case PinType.Float:
                            outPins[0].Value = 0;
                            foreach (InputPin p in inPins) {
                                outPins[0].Value = (float)outPins[0].Value + (p.isConnected ? (float)p.Value : 0);
                            }
                            break;
                        case PinType.String:
                            outPins[0].Value = "";
                            foreach (InputPin p in inPins) {
                                outPins[0].Value = (string)outPins[0].Value + (p.isConnected ? (string)p.Value : "");
                            }
                            break;
                        case PinType.Vector2:
                            outPins[0].Value = Vector2.zero;
                            foreach (InputPin p in inPins) {
                                outPins[0].Value = (Vector2)outPins[0].Value + (p.isConnected ? (Vector2)p.Value : Vector2.zero);
                            }
                            break;
                        case PinType.Vector3:
                            outPins[0].Value = Vector3.zero;
                            foreach (InputPin p in inPins) {
                                outPins[0].Value = (Vector3)outPins[0].Value + (p.isConnected ? (Vector3)p.Value : Vector3.zero);
                            }
                            break;
                        case PinType.Vector4:
                            outPins[0].Value = Vector4.zero;
                            foreach (InputPin p in inPins) {
                                outPins[0].Value = (Vector4)outPins[0].Value + (p.isConnected ? (Vector4)p.Value : Vector4.zero);
                            }
                            break;
                    }
                    break;
                case OpType.Subtract:
                    if (start < inPins.Count) outPins[0].Value = inPins[start].Value;
                    switch (outPins[0].varType) {
                        case PinType.Integer:
                        case PinType.Float:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                outPins[0].Value = (float)outPins[0].Value - (inPins[i].isConnected ? (float)inPins[i].Value : 0);
                            }
                            break;
                        case PinType.Vector2:
                            for (int i = 1; i < inPins.Count; i++) {
                                outPins[0].Value = (Vector2)outPins[0].Value - (inPins[i].isConnected ? (Vector2)inPins[i].Value : Vector2.zero);
                            }
                            break;
                        case PinType.Vector3:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                outPins[0].Value = (Vector3)outPins[0].Value - (inPins[i].isConnected ? (Vector3)inPins[i].Value : Vector3.zero);
                            }
                            break;
                        case PinType.Vector4:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                outPins[0].Value = (Vector4)outPins[0].Value - (inPins[i].isConnected ? (Vector4)inPins[i].Value : Vector4.zero);
                            }
                            break;
                    }
                    break;
                case OpType.Multiply:
                    if (start < inPins.Count) outPins[0].Value = inPins[start].Value;
                    switch (outPins[0].varType) {
                        case PinType.Integer:
                        case PinType.Float:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                if (inPins[i].isConnected)
                                    outPins[0].Value = (float)outPins[0].Value * (float)inPins[i].Value;
                            }
                            break;
                        case PinType.Vector2:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                if (inPins[i].isConnected)
                                    outPins[0].Value = Vector2.Scale((Vector2)outPins[0].Value, (Vector2)inPins[i].Value);
                            }
                            break;
                        case PinType.Vector3:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                if (inPins[i].isConnected)
                                    outPins[0].Value = Vector2.Scale((Vector3)outPins[0].Value, (Vector3)inPins[i].Value);
                            }
                            break;
                        case PinType.Vector4:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                if (inPins[i].isConnected)
                                    outPins[0].Value = Vector4.Scale((Vector4)outPins[0].Value, (Vector4)inPins[i].Value);
                            }
                            break;
                    }
                    break;
                case OpType.Divide:
                    if (start < inPins.Count) outPins[0].Value = inPins[start].Value;
                    switch (outPins[0].varType) {
                        case PinType.Integer:
                        case PinType.Float:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                if (inPins[i].isConnected)
                                    if (!inPins[i].Equals(0))
                                        outPins[0].Value = (float)outPins[0].Value / (float)inPins[i].Value;
                                    else {
                                        outPins[0].Value = 0;
                                        break;
                                    }
                            }
                            break;
                    }
                    break;
                case OpType.SQRT:
                    outPins[0].Value = inPins[0].isConnected ?
                        (float)inPins[0].Value >= 0 ? Mathf.Sqrt((float)inPins[0].Value)
                        : 0 : 0;
                    break;
                case OpType.Not:
                    outPins[0].Value = inPins[0].isConnected ? !(bool)inPins[0].Value
                        : false;
                    break;
                case OpType.And:
                    if (start < inPins.Count) outPins[0].Value = inPins[start].Value;
                    for (int i = start + 1; i < inPins.Count; i++) {
                        if (inPins[i].isConnected)
                            outPins[0].Value = (bool)outPins[0].Value && (bool)inPins[i].Value;
                    }
                    break;
                case OpType.Or:
                    if (start < inPins.Count) outPins[0].Value = inPins[start].Value;
                    for (int i = start + 1; i < inPins.Count; i++) {
                        if (inPins[i].isConnected)
                            outPins[0].Value = (bool)outPins[0].Value || (bool)inPins[i].Value;
                    }
                    break;
                case OpType.Xor:
                    if (start < inPins.Count) outPins[0].Value = inPins[start].Value;
                    for (int i = start + 1; i < inPins.Count; i++) {
                        if (inPins[i].isConnected)
                            outPins[0].Value = (bool)outPins[0].Value ^ (bool)inPins[i].Value;
                    }
                    break;
                case OpType.POW:
                    float in1 = inPins[0].isConnected ? (float)inPins[0].Value : 0;
                    float in2 = inPins[1].isConnected ? (float)inPins[1].Value : 2;
                    outPins[0].Value = (in1 == 0 && in2 == 2) ? 0 : Mathf.Pow(in1, in2);
                    break;
            }
        }

        private static string GetTitle(OpType type) {
            return type.ToString() + " Function";
        }
        #endregion
        
        static MathNode() {
            validCombos = new Dictionary<OpType, List<string>>();

            List<string> pins = new List<string>();
            pins.Add(PinType.Float.ToString());
            pins.Add(PinType.Integer.ToString());
            pins.Add(PinType.String.ToString());
            pins.Add(PinType.Vector2.ToString());
            pins.Add(PinType.Vector3.ToString());
            pins.Add(PinType.Vector4.ToString());
            validCombos.Add(OpType.Add, pins);
            validCombos.Add(OpType.Subtract, pins);

            pins = new List<string>();
            pins.Add(PinType.Float.ToString());
            pins.Add(PinType.Integer.ToString());
            pins.Add(PinType.Vector2.ToString());
            pins.Add(PinType.Vector3.ToString());
            pins.Add(PinType.Vector4.ToString());
            validCombos.Add(OpType.Multiply, pins);

            pins = new List<string>(new string[] {
                PinType.Float.ToString(), PinType.Integer.ToString() });
            validCombos.Add(OpType.Divide, pins);
            validCombos.Add(OpType.SQRT, pins);
            validCombos.Add(OpType.Abs, pins);
            validCombos.Add(OpType.POW, pins);

            pins = new List<string>(new string[] { PinType.Float.ToString(),
                PinType.Integer.ToString() });
            
            validCombos.Add(OpType.And, pins);
            validCombos.Add(OpType.Or, pins);
            validCombos.Add(OpType.Not, pins);
            validCombos.Add(OpType.Xor, pins);
        }
    }
}
