using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace ScriptEditor.Graph {
    //[Serializable]
    public abstract class NodePin {

        [SerializeField] public string Name, Description;
        [SerializeField] public bool isConnected = false;
        [SerializeField] public NodeBase node;
        [SerializeField] public VarType varType;
        [SerializeField] public Rect bounds;
        public object Value {
            get { return val; }
            set {
                if (value != null)
                    switch (varType) {
                        case VarType.Bool: val = (bool)value; break;
                        case VarType.Float: val = (float)value; break;
                        case VarType.Integer: val = (int)value; break;
                        case VarType.String: val = (string)value; break;
                        case VarType.Vector2: val = (Vector2)value; break;
                        case VarType.Vector3: val = (Vector3)value; break;
                        case VarType.Vector4: val = (Vector4)value; break;
                        case VarType.Color: val = (Color)value; break;
                    }
            }
        }

        public string StyleName {
            get {
                string color = "";
                switch (varType) {
                    case VarType.Bool: color = "Red"; break;
                    case VarType.Actor: color = "Orange"; break;
                    case VarType.Vector2:
                    case VarType.Vector3:
                    case VarType.Color:
                    case VarType.Vector4: color = "Yellow"; break;
                    case VarType.Float: color = "Green"; break;
                    case VarType.Integer: color = "Cyan"; break;
                    case VarType.Object: color = "Blue"; break;
                    case VarType.String: color = "Purple"; break;
                    case VarType.Exec: color = "White"; break;
                }

                return "Pin" + color + (this.isConnected ? "Closed" : "Open");
            }
        }

        public Rect TextBox {
            get {
                Vector2 lblPos = bounds.position;
                Vector2 lblSiz = node.skin.label.CalcSize(new GUIContent(Name + "  "));
                lblPos.x += (isInput ? NodePin.margin.x : -lblSiz.x);
                return new Rect(lblPos, lblSiz);
            }
        }

        public static Vector2 margin = new Vector2(25, 13);
        public static Vector2 pinSize = new Vector2(23, 17);
        public const float padding = 16;
        public static float Top { get { return margin.y + padding; } }

        [SerializeField] protected object val;

        public NodePin(NodeBase n, object val) : this(n, GetVarType(val)) {
            Value = val;
            // set name of node to name of variable
        }

        public NodePin(NodeBase n, VarType varType) {
            this.varType = varType;
            node = n;
            bounds = new Rect(Vector2.zero, pinSize);
        }

        /// <summary> returns the name of the base connected node </summary>
        public abstract string ConName();

        /// <summary> determines the PinType from the type of the passed variable  </summary>
        public static VarType GetVarType(object variable) {
            if (variable.GetType().Equals(typeof(int))) return VarType.Integer;
            if (variable.GetType().Equals(typeof(bool))) return VarType.Bool;
            if (variable.GetType().Equals(typeof(float))) return VarType.Float;
            if (variable.GetType().Equals(typeof(string))) return VarType.String;
            if (variable.GetType().Equals(typeof(GameObject))) return VarType.Actor; //TODO
            if (variable.GetType().Equals(typeof(Vector2))) return VarType.Vector2;
            if (variable.GetType().Equals(typeof(Vector3))) return VarType.Vector3;
            if (variable.GetType().Equals(typeof(Vector4))) return VarType.Vector4;
            return VarType.Object;
        }

        /// <summary> is the NodePin an InputPin? </summary>
        public bool isInput { get { return GetType().Equals(typeof(InputPin)); } }

        /// <summary> color respective of the type of the pin </summary>
        public Color _Color {
            get {
                switch (varType) {
                    case VarType.Exec: return Color.white;
                    case VarType.Actor: return Color.Lerp(Color.red, Color.yellow, .5f);
                    case VarType.Bool: return Color.red;
                    case VarType.Float: return Color.green;
                    case VarType.Integer: return Color.cyan;
                    case VarType.String: return Color.Lerp(Color.red, Color.blue, .5f);
                    case VarType.Vector2:
                    case VarType.Vector3:
                    case VarType.Color:
                    case VarType.Vector4: return Color.yellow;
                    default: return Color.blue;
                }
            }
        }

#if UNITY_EDITOR
        public bool Contains(Vector2 pos) {
            Rect b = new Rect(bounds);
            b.position += node.GetBody().position;
            bool res = b.Contains(pos);

            if (!res) {
                b = new Rect(TextBox);
                b.position += node.GetBody().position;
                res = b.Contains(pos);
            }

            return res;
        }

        public Vector2 Position {
            get {
                if (bounds == Rect.zero) return Vector2.zero;
                return bounds.position + node.GetBody().position;
            }
        }
        public Vector2 Center {
            get {
                return Position + new Vector2(isInput ? -pinSize.y / 2f :
                    pinSize.x*1.5f, 8);
            }
        }

        public void DrawConnection() {
            if (!isConnected) return;

            // draw bezier curve from output pin to input pin
            try {
                Vector3 start = (isInput) ? ((InputPin)this).ConnectedOutput.Center :
                    this.Center;
                Vector3 end = (isInput) ? this.Center : 
                    ((OutputPin)this).ConnectedInput.Center;
                Vector2 startTangent, endTangent;

                float offset = Mathf.Max(Mathf.Abs(start.x - end.x) / 1.75f, 1);
                startTangent = new Vector2(start.x + offset, start.y);
                endTangent = new Vector2(end.x - offset, end.y);
                //Debug.Log(start + " | " + end + "\n" + startTangent + " | " + endTangent);
                Handles.BeginGUI();
                {
                    Handles.color = Color.white;
                    Handles.DrawBezier(start, end, startTangent, endTangent, this._Color, null, 2);
                } Handles.EndGUI();
            } catch {
                Debug.Log("Unable to draw: " + this);
                //Debug.Log("ConnO: " + ConnectedOutput);
            }
        }
#endif  
    }

    /// <summary> Input pin. If disconnected, a constant can be provided </summary>
    [Serializable]
    public class InputPin : NodePin {
        public OutputPin ConnectedOutput = null;

        [SerializeField] private object defaultVal;
        /// <summary> Default value of the input when pin is constructed </summary>
        public object Default {
            get { return defaultVal; }
            set {
                if (value != null) {
                    switch (varType) {
                        case VarType.Bool: defaultVal = (bool)value; break;
                        case VarType.Float: defaultVal = (float)value; break;
                        case VarType.Integer: defaultVal = (int)value; break;
                        case VarType.String: defaultVal = (string)value; break;
                        case VarType.Vector2: defaultVal = (Vector2)value; break;
                        case VarType.Vector3: defaultVal = (Vector3)value; break;
                        case VarType.Vector4: defaultVal = (Vector4)value; break;
                        case VarType.Color: defaultVal = (Color)value; break;
                    }
                    val = defaultVal;
                }
            }
        }

        public InputPin(NodeBase n, object val) : base(n, val) {
            Default = val;
        }
        public InputPin(NodeBase n, VarType t) : base(n, t) { }

        public override string ConName() {
            return ConnectedOutput.node.name;
        }
        public override string ToString() {
            return "(IN) Node: " + node.STName + " | " + varType + "\n" + node.description;
        }
    }

    /// <summary> Output connection. Must always have a value, even if disconnected </summary>
    [Serializable]
    public class OutputPin : NodePin {
        //public int ConnectedInputID = -1;
        public InputPin ConnectedInput = null;
        //public bool isConnected { get { return ConnectedInputID != -1; } }

        public OutputPin(NodeBase n, object val) : base(n, val) { }
        public OutputPin(NodeBase n, VarType t) : base(n, t) { }

        public override string ConName() {
            return "???";
        }
        public override string ToString() {
            return "(OUT) Node: " + (node == null ? "???" : node.STName) + " | " + varType;
        }
    }
}