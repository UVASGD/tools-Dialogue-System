using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace ScriptEditor.Graph {
    [Serializable]
    public abstract class NodePin {

        [SerializeField] public string Name, Description;
        [SerializeField] public NodeBase parentNode;
        [SerializeField] public VarType varType;
        [SerializeField] public Rect bounds;

        public virtual bool IsConnected { get; set; }
        
        public string StyleName {
            get {
                string color = "";
                switch (varType) {
                    case VarType.Bool: color = "Red"; break;
                    case VarType.Actor: color = "Orange"; break;
                    case VarType.Vector2:
                    case VarType.Vector3:
                    case VarType.Color:
                    case VarType.Float: color = "Green"; break;
                    case VarType.Integer: color = "Cyan"; break;
                    case VarType.Object: color = "Blue"; break;
                    case VarType.String: color = "Purple"; break;
                    case VarType.Exec: color = "White"; break;
                }

                return "Pin" + color + (this.IsConnected ? "Closed" : "Open");
            }
        }

        public Rect TextBox {
            get {
                Vector2 lblPos = bounds.position;
                Vector2 lblSiz = parentNode.skin.label.CalcSize(new GUIContent(Name + "  "));
                lblPos.x += (isInput ? NodePin.margin.x : -lblSiz.x);
                return new Rect(lblPos, lblSiz);
            }
        }

        public static Vector2 margin = new Vector2(25, 13);
        public static Vector2 pinSize = new Vector2(23, 17);
        public const float padding = 16;
        public static float Top { get { return margin.y + padding; } }


        public NodePin(NodeBase n, VarType varType) {
            this.varType = varType;
            parentNode = n;
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
            return VarType.Object;
        }

        /// <summary> is the NodePin an InputPin? </summary>
        public bool isInput { get { return this is InputPin; } }

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
                    default: return Color.blue;
                }
            }
        }
        

#if UNITY_EDITOR
        public bool Contains(Vector2 pos) {
            Rect b = new Rect(bounds);
            b.position += parentNode.GetBody().position;
            return b.Contains(pos);
        }

        /// <summary> determine if mouse is inside textbox for name </summary>
        public bool TxtContains(Vector2 pos) {
            Rect b = new Rect(TextBox);
            b.position += parentNode.GetBody().position;
            return b.Contains(pos);
        }

        public Vector2 Position {
            get {
                if (bounds == Rect.zero) return Vector2.zero;
                return bounds.position + parentNode.GetBody().position;
            }
        }

        public Vector2 Center {
            get {
                return Position + new Vector2(isInput ? -pinSize.y / 2f :
                    pinSize.x*1.5f, 8);
            }
        }

        public void DrawConnection() {
            if (!IsConnected || isInput) return;

            // draw bezier curve from output pin to input pin
            try {
                //Debug.Log("Planetarium: "+this.GetType());
                Vector3 start = this.Center;
                Vector3 end = ((OutputPin)this).ConnectedInput.Center;
                Vector2 startTangent, endTangent;

                float offset = Mathf.Max(Mathf.Abs(start.x - end.x) / 1.75f, 1);
                startTangent = new Vector2(start.x + offset, start.y);
                endTangent = new Vector2(end.x - offset, end.y);
                // Debug.Log(start + " | " + end + "\n" + startTangent + " | " + endTangent);
                Handles.BeginGUI();
                {
                    Handles.color = Color.white;
                    Handles.DrawBezier(start, end, startTangent, endTangent, this._Color, null, 2);
                } Handles.EndGUI();
            } catch (Exception e) {
                Debug.Log("Unable to draw: " + this);
                Debug.Log(e);
            }
        }
#endif  
    }



    
}