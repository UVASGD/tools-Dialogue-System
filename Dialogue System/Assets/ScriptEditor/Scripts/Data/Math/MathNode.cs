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

        const float Header = 13, Left = 15;
        new const float Bottom = 17;
        const float splashHeight = 36, buttonTop = 3, buttonLeft = 7;
        static Vector2 baseBox = new Vector2(153, 87);
        static Vector2 baseButton = new Vector2(55, 14);
        Vector2 boxSize;
        [SerializeField] protected string splashText;

        /// <summary> What function is performed by node </summary>
        public enum OpType {
            Abs, SQRT, Add, Subtract, Multiply, Divide, And, Or, Not, Xor, POW,
            EqualTo, LessThan, GreaterThan, LessOrEqual, GreaterOrEqual
        }

        public OpType SubType() { return op; }

        public virtual void Construct() {

        }

        /// <summary>
        /// Build pins for node
        /// </summary>
        /// <param name="title"></param>
        public void Construct(OpType type, VarType nT, int inCount) {
            base.SetName(GetTitle(type));
            op = type;

            // set description
            switch (type) {
                case OpType.Add:
                    description = "Adds all input nodes. Unconnected nodes are not applied from the top. " +
                        "If nothing is connected, the default value is 0.";
                    name = "Addition Function";
                    splashText = "+";
                    break;
                case OpType.Subtract:
                    description = "Subtracts all input nodes from the first connected input from the top. " +
                        "If nothing is connected, the default value is 0.";
                    name = "Subtraction Function";
                    splashText = "-";
                    break;
                case OpType.Multiply:
                    description = "Multiplies all input nodes to the first connected input from the top. " +
                        "If nothing is connected, the default value is 0.";
                    name = "Multiplication Function";
                    splashText = "X";
                    break;
                case OpType.Divide:
                    description = "Divides the first connected input by all succeeding input. If nothing is connected, " +
                        "the default value is 0. Avoids dividing by 0";
                    name = "Division Function";
                    splashText = "/";
                    break;
                case OpType.Abs:
                    description = "Calculates the absolute value of the input node. Defaults to 0 if nothing is connected.";
                    name = "Absolute Value Function";
                    splashText = "ABS";
                    break;
                case OpType.SQRT:
                    description = "Calculates the square root of the input node. Defaults to 0 if nothing is connected.";
                    name = "Squareroot Function";
                    splashText = "SQRT";
                    break;
                case OpType.Not:
                    description = "Logically negates input node. Defaults to false if nothing is connected.";
                    name = "Logical Negation";
                    splashText = "NOT";
                    break;
                case OpType.And:
                    description = "Performs AND on each subsequent connected input node. Defaults to false if nothing is connected.";
                    name = "Boolean AND Function";
                    splashText = "AND";
                    break;
                case OpType.Or:
                    description = "Performs OR on each subsequent connected input node. Defaults to false if nothing is connected.";
                    name = "Boolean OR Function";
                    splashText = "OR";
                    break;
                case OpType.Xor:
                    description = "Performs XOR on each subsequent connected input node. Defaults to false if nothing is connected.";
                    name = "Boolean XOR Function";
                    splashText = "XOR";
                    break;
                case OpType.POW:
                    description = "Calculates the first value to the power of the second. Second value defaults to 2 if not connected." +
                        " First value defaults to 0. If nothing is connected, the default output is 0.";
                    name = "Power Function";
                    splashText = "^";
                    break;
                case OpType.EqualTo:
                    description = "Compares equality between values.";
                    name = "Equal To";
                    break;
                case OpType.GreaterThan:
                    description = "Compares if first value is greater than the second.";
                    name = "Greater Than";
                    break;
                case OpType.GreaterOrEqual:
                    description = "Compares if first value is greater than or equal to the second..";
                    name = "Greater Than or Equal To";
                    break;
                case OpType.LessThan:
                    description = "Compares if first value is less than the second.";
                    name = "Less Than";
                    break;
                case OpType.LessOrEqual:
                    description = "Compares if first value is less than or equal to the second.";
                    name = "Less Than or Equal To";
                    break;
            }

            // create pins;
            switch (type) {
                case OpType.Add:
                case OpType.Subtract:
                case OpType.Multiply:
                case OpType.Divide:
                    for (int i = 0; i < inCount; i++) {
                        ValueInputPin p = new ValueInputPin(this, nT);
                        inPins.Add(p);
                    }
                    outPins.Add(new ValueOutputPin(this, nT));
                    multiplePins = true;
                    break;
                case OpType.Abs:
                case OpType.SQRT:
                    inPins.Add(new ValueInputPin(this, nT));
                    outPins.Add(new ValueOutputPin(this, nT));
                    break;
                case OpType.POW:
                    inPins.Add(new ValueInputPin(this, nT));
                    inPins.Add(new ValueInputPin(this, nT));
                    outPins.Add(new ValueOutputPin(this, nT));
                    break;
                case OpType.Not:
                    inPins.Add(new ValueInputPin(this, VarType.Bool));
                    outPins.Add(new ValueOutputPin(this, VarType.Bool));
                    break;
                case OpType.And:
                case OpType.Or:
                case OpType.Xor:
                    for (int i = 0; i < inCount; i++)
                        inPins.Add(new ValueInputPin(this, VarType.Bool));
                    outPins.Add(new ValueOutputPin(this, VarType.Bool));
                    multiplePins = true;
                    break;
                case OpType.EqualTo:
                case OpType.GreaterThan:
                case OpType.GreaterOrEqual:
                case OpType.LessThan:
                case OpType.LessOrEqual:
                    inPins.Add(new ValueInputPin(this, nT));
                    inPins.Add(new ValueInputPin(this, nT));
                    outPins.Add(new ValueOutputPin(this, VarType.Bool));
                    break;

            }

            if (multiplePins) description += " Maximum pins allowed is 16.";
            Resize();
        }

        /// <summary>
        /// Resize the body of node. Necessary when number of pins changes or the width of text to be displayed changes.
        /// </summary>
        const float margin = 22;
        public override void Resize() {
            float outH = baseBox.y;
            float inH = NodePin.padding + inPins.Count * NodePin.Top;

            float txtW = 4 * margin + NodePin.pinSize.x +
                skin.GetStyle("NodeMathSelected").CalcSize(new GUIContent(splashText)).x;

            body = new Rect(pan.x, pan.y, Mathf.Max(txtW, baseBox.x + 2 * Left), Mathf.Max(outH, inH) + Header + Bottom);

            // disconnected input sizes

            boxSize = new Vector2(body.width - 2 * Left, body.height - Header - Bottom);

            // relocate pins
            foreach (NodePin pin in AllPins) {
                Vector2 pos = Vector2.zero;
                if (pin.GetType().Equals(typeof(OutputPin))) {
                    pos.x = body.width - margin - NodePin.pinSize.x;
                    pos.y = Center.y - NodePin.pinSize.y / 2f;
                } else {
                    pos.x = margin;
                    pos.y = inPins.Count == 1 ? Center.y - NodePin.pinSize.y / 2f :
                        Center.y - NodePin.Top * (inPins.Count / 2f - inPins.IndexOf((InputPin)pin));
                }
                pin.bounds.position = pos;
                //Debug.Log(pin + ": " + pos);
            }
        }

        /// <summary>
        /// Point in the middle of visual Box
        /// </summary>
        private Vector2 Center {
            get {
                return new Vector2(boxSize.x / 2f + Left, boxSize.y / 2f + Header);
            }
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Math;
        }

#if UNITY_EDITOR
        public override void DrawNode(Event e, Rect viewRect) {
            this.viewRect = viewRect;
            ProcessEvents(e, viewRect);

            GUI.Box(body, splashText,
                isSelected ? skin.GetStyle("NodeMathSelected") :
                skin.GetStyle("NodeMathBackground"));
            DrawPins();

            // pin buttons
            float bw = Mathf.Max(baseButton.x, skin.GetStyle("NodeMathButton")
                .CalcSize(new GUIContent("- Remove Pin")).x);
            if (multiplePins) {
                if (inPins.Count < 16)
                    if (GUI.Button(new Rect(body.position.x + boxSize.x + Left - bw - buttonLeft,
                        body.position.y + Center.y - (boxSize.y / 4f + buttonTop + Header / 2f), bw, baseButton.y),
                        "+ Add Pin"/*, skin.GetStyle("NodeMathButton")*/))
                        AddInputPin();
                if (inPins.Count > 2)
                    if (GUI.Button(new Rect(body.position.x + boxSize.x + Left - bw - buttonLeft,
                        body.position.y + Center.y + (boxSize.y / 4f + buttonTop + buttonTop - Header / 2f), bw, baseButton.y),
                        "- Remove Pin"/*, skin.GetStyle("NodeMathButton")*/))
                        RemovePin();
            }
        }
#endif

        private void ProcessContextMenu(Event e) {

        }

        private void ContextCallback(object obj) {

        }

        /// <summary> first active inPin </summary>
        private int Start() {
            for (int i = 0; i < inPins.Count; i++)
                if (inPins[i].isConnected)
                    return i;
            return inPins.Count;
        }

        #region Override
        /// <summary> calculate value of output </summary>
        public override void Lookup(bool compileTime) {
            base.Lookup(compileTime);

            if (compileTime) return;
            int start = Start();
            switch (op) {
                case OpType.Abs:
                    switch (outPins[0].varType) {
                        case VarType.Integer:
                        case VarType.Float:
                            outPins[0].Value = inPins[0].isConnected ?
                                (Mathf.Abs((float)inPins[0].Value)) : 0;
                            break;
                        case VarType.Vector2:
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
                        case VarType.Integer:
                        case VarType.Float:
                            outPins[0].Value = 0;
                            foreach (InputPin p in inPins) {
                                outPins[0].Value = (float)outPins[0].Value + (p.isConnected ? (float)p.Value : 0);
                            }
                            break;
                        case VarType.String:
                            outPins[0].Value = "";
                            foreach (InputPin p in inPins) {
                                outPins[0].Value = (string)outPins[0].Value + (p.isConnected ? (string)p.Value : "");
                            }
                            break;
                        case VarType.Vector2:
                            outPins[0].Value = Vector2.zero;
                            foreach (InputPin p in inPins) {
                                outPins[0].Value = (Vector2)outPins[0].Value + (p.isConnected ? (Vector2)p.Value : Vector2.zero);
                            }
                            break;
                        case VarType.Vector3:
                            outPins[0].Value = Vector3.zero;
                            foreach (InputPin p in inPins) {
                                outPins[0].Value = (Vector3)outPins[0].Value + (p.isConnected ? (Vector3)p.Value : Vector3.zero);
                            }
                            break;
                        case VarType.Vector4:
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
                        case VarType.Integer:
                        case VarType.Float:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                outPins[0].Value = (float)outPins[0].Value - (inPins[i].isConnected ? (float)inPins[i].Value : 0);
                            }
                            break;
                        case VarType.Vector2:
                            for (int i = 1; i < inPins.Count; i++) {
                                outPins[0].Value = (Vector2)outPins[0].Value - (inPins[i].isConnected ? (Vector2)inPins[i].Value : Vector2.zero);
                            }
                            break;
                        case VarType.Vector3:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                outPins[0].Value = (Vector3)outPins[0].Value - (inPins[i].isConnected ? (Vector3)inPins[i].Value : Vector3.zero);
                            }
                            break;
                        case VarType.Vector4:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                outPins[0].Value = (Vector4)outPins[0].Value - (inPins[i].isConnected ? (Vector4)inPins[i].Value : Vector4.zero);
                            }
                            break;
                    }
                    break;
                case OpType.Multiply:
                    if (start < inPins.Count) outPins[0].Value = inPins[start].Value;
                    switch (outPins[0].varType) {
                        case VarType.Integer:
                        case VarType.Float:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                if (inPins[i].isConnected)
                                    outPins[0].Value = (float)outPins[0].Value * (float)inPins[i].Value;
                            }
                            break;
                        case VarType.Vector2:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                if (inPins[i].isConnected)
                                    outPins[0].Value = Vector2.Scale((Vector2)outPins[0].Value, (Vector2)inPins[i].Value);
                            }
                            break;
                        case VarType.Vector3:
                            for (int i = start + 1; i < inPins.Count; i++) {
                                if (inPins[i].isConnected)
                                    outPins[0].Value = Vector2.Scale((Vector3)outPins[0].Value, (Vector3)inPins[i].Value);
                            }
                            break;
                        case VarType.Vector4:
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
                        case VarType.Integer:
                        case VarType.Float:
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
            pins.Add(VarType.Float.ToString());
            pins.Add(VarType.Integer.ToString());
            pins.Add(VarType.String.ToString());
            pins.Add(VarType.Vector2.ToString());
            pins.Add(VarType.Vector3.ToString());
            pins.Add(VarType.Vector4.ToString());
            validCombos.Add(OpType.Add, pins);
            validCombos.Add(OpType.Subtract, pins);

            pins = new List<string>();
            pins.Add(VarType.Float.ToString());
            pins.Add(VarType.Integer.ToString());
            pins.Add(VarType.Vector2.ToString());
            pins.Add(VarType.Vector3.ToString());
            pins.Add(VarType.Vector4.ToString());
            validCombos.Add(OpType.Multiply, pins);


            pins = new List<string>(new string[] {
                VarType.Float.ToString(), VarType.Integer.ToString() });
            validCombos.Add(OpType.Divide, pins);
            validCombos.Add(OpType.SQRT, pins);
            validCombos.Add(OpType.Abs, pins);
            validCombos.Add(OpType.POW, pins);

            pins = new List<string>(new string[] { VarType.Float.ToString(),
                VarType.Integer.ToString() });

            pins = new List<string>(new string[] { VarType.Bool.ToString() });
            validCombos.Add(OpType.And, pins);
            validCombos.Add(OpType.Or, pins);
            validCombos.Add(OpType.Not, pins);
            validCombos.Add(OpType.Xor, pins);
        }
    }
}
