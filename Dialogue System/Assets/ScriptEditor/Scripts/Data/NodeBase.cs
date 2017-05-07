﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
//using ScriptEditor.EditorScripts;
#endif
using System;

namespace ScriptEditor.Graph {

    public enum NodeType {
        Math, Function, Fetch, Dialog, Control, Event
    }

    public enum PinType {
        Logic, Bool, Integer, Float, String, Vector2, Vector3, Vector4, Object, Actor,
    }

    [Serializable]
    public abstract class NodeBase : ScriptableObject {
        
        public string  description;
        public bool isSelected;
        public bool multiplePins = false;
        public OutputPin ActiveOutput;
        public InputPin ActiveInput;
        public NodeGraph parentGraph;
        // visual constants
        public const float Top = 56;
        private const float Bottom = 23 + NodePin.padding;
        private const float Width = 175;

        public List<OutputPin> outPins;
        public List<InputPin> inPins;
        public int MaxNodes { get { return Mathf.Max(inPins.Count, outPins.Count); } }
        public NodeType TypeNode { get { return nodeType; } }
        public string LongestInName {
            get {
                string res = "";
                foreach (InputPin i in inPins)
                    if (!String.IsNullOrEmpty(i.Name))
                        res = (i.Name.Length > res.Length) ? i.Name : res;
                return res;
            }
        }
        public string LongestOutName {
            get {
                string res = "";
                foreach (OutputPin o in outPins)
                    if (!String.IsNullOrEmpty(o.Name))
                        res = (o.Name.Length > res.Length) ? o.Name : res;
                return res;
            }
        }
        public List<NodePin> AllPins { get {
                List<NodePin> pins = new List<NodePin>(outPins.ToArray());
                pins.AddRange(inPins.ToArray());
                return pins;
            } }

        public string NTName { get { return Enum.GetName(typeof(NodeType), nodeType); } }
        public string STName {
            get {
                switch (nodeType) {
                    case NodeType.Control:
                        return Enum.GetName(typeof(ControlNode.ControlType),
                            ((ControlNode)this).SubType());
                    case NodeType.Function:
                        return Enum.GetName(typeof(FunctionNode.FunctionType),
                            ((FunctionNode)this).SubType());
                    case NodeType.Math:
                        return Enum.GetName(typeof(MathNode.OpType),
                            ((MathNode)this).SubType());
                }

                return "none";
            }
        }

        /// <summary> whether or not more input pins can be added to node </summary>
        protected NodeType nodeType;
        protected Rect body, viewRect;
        protected GUISkin skin;

        public Rect GetBody() { return body; }
        public void SetPos(Vector2 pos) { body.position = pos; }
        public void Pan(Vector2 pan) { body.position += pan; this.pan += pan; }
        protected Vector2 pan=Vector2.zero;

        public void OnEnable() {
            GetEditorSkin();
        }
        protected void SetName (string title) {
            this.name = title;
        }

        /// <summary>
        /// Resize the body of node. Necessary when number of pins changes or the width of text to be displayed changes.
        /// </summary>
        protected virtual void Resize() {
            GUIStyle style = GUI.skin.box;
            float nameWidth = style.CalcSize(new GUIContent(LongestInName)).x +
                style.CalcSize(new GUIContent(LongestOutName)).x;
            float pinWidth = 2 * 50;
            float w = pinWidth + nameWidth + 20;
            body = new Rect(0, 0, w > Width ? w : Width, Top + NodePin.Top * MaxNodes + Bottom);

            // set pin visual information
            foreach(NodePin pin in AllPins) {
                bool isOutput = pin.GetType().Equals(typeof(OutputPin));
                float x = isOutput ? body.width - NodePin.margin.x - NodePin.pinSize.x : NodePin.margin.x;
                float y = isOutput ? outPins.IndexOf((OutputPin)pin) :
                    inPins.IndexOf((InputPin)pin);

                pin.bounds.position = new Vector2(x, Top + NodePin.margin.y + y * Top);
            }
        }

        /// <summary> makes the node ready for display and value storage </summary>
        public virtual void Initialize() {
            GetEditorSkin();
            inPins = new List<InputPin>();
            outPins = new List<OutputPin>();
            //hideFlags = HideFlags.HideInHierarchy;
        }

        /// <summary> begin logical execution of node </summary>
        public virtual void Execute() {
            foreach(OutputPin no in outPins) {
                if (no.isConnected && no.varType == PinType.Logic) {
                        // execute connected node

                        break;
                    }
            }
        }

        /// <summary> add new pin with base variable type</summary>
        public void AddInputPin() {
            if (multiplePins && inPins.Count < 16) {
                inPins.Add(new InputPin(this, inPins[0].varType));
                Resize();
            }
        }

        /// <summary> remove last input pin</summary>
        public void RemovePin() {
            if (multiplePins && inPins.Count > 2) {
                inPins.RemoveAt(inPins.Count - 1);
                Resize();
            }
        }

        /// <summary> Update flow of values through connected pins  </summary>
        /// <param name="e"></param>
        public virtual void UpdateNode(Event e) {

        }

        /// <summary>
        /// check for mouse collision
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool Contains(Vector2 pos) {
            return body.Contains(pos);
        }

        /// <summary>
        /// check for mouse collision with pins
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        public NodePin InsidePin(Vector2 mousePos) {
            foreach(NodePin pin in AllPins) {
                if (pin.Contains(mousePos)) return pin;
            }
            return null;
        }

        /// <summary>
        /// remove the a pin from the node
        /// </summary>
        /// <param name="o"></param>
        public void RemovePin(object o) {
            
        }

        /// <summary>
        /// start connection from pin
        /// </summary>
        /// <param name="output"></param>
        private void HandleConnection(OutputPin output) {

        }

        /// <summary>
        /// finish connection to pin
        /// </summary>
        /// <param name="input"></param>
        private void FinalizeConnection(InputPin input) {

        }

        // get name of variable from History
        public static string GetVarName(object variable) {
            return variable.ToString();
        }

        /// <summary>
        /// determine whether or not to use Unity Dark Theme and set the appropiate skin
        /// </summary>
        private void GetEditorSkin() {
            string skinName = EditorGUIUtility.isProSkin ? "NodeEditorDark" : "NodeEditorLight";
            skin = Resources.Load<GUISkin>("GUI Skins/Editor/" + skinName);
        }

#if UNITY_EDITOR
        public virtual void DrawNode(Event e, Rect viewRect) {
            this.viewRect = viewRect;
            ProcessEvents(e, viewRect);

            GUI.Box(body, name,
                isSelected ? skin.GetStyle("Node" + NTName + "Selected") :
                skin.GetStyle("Node" + NTName + "Background"));
            DrawPins();
        }

        public virtual void DrawPins() {
            GUILayout.BeginArea(body);
            foreach(NodePin pin in AllPins) {
                GUI.Box(pin.bounds, "", skin.GetStyle(pin.StyleName));
            }
            GUILayout.EndArea();
        }

        public void DrawConnections() {
            foreach (InputPin ip in inPins)
                ip.DrawConnection();
        }

        /// <summary>
        /// handle events and input
        /// </summary>
        /// <param name="e"></param>
        /// <param name="viewRect"></param>
        public virtual void ProcessEvents(Event e, Rect viewRect) {

            // Drag around the node
            if (isSelected && e.button == 0) {
                if (e.type == EventType.MouseDrag) {
                    body.position += e.delta;
                }
            }
        }

        /// <summary>
        /// completely disconnect node. NOTE: this function does not work as intended!
        /// </summary>
        /// <param name="pin"></param>
        public void RemoveAllPins () {
            outPins = new List<OutputPin>();
            inPins = new List<InputPin>();
        }

        public static void RemoveConnection(object p) {
            NodePin pin = (NodePin)p;
            pin.isConnected = false;

            if (pin.GetType().Equals(typeof(OutputPin))) {
                // find InputPin from ID
                //InputFromID(((OutputPin)pin).ConnectedInputID).isSelected = false;
                //InputFromID(((OutputPin)pin).ConnectedInputID).ConnectedOutput = null;
                ((OutputPin)pin).ConnectedInput.isConnected = false;
                ((OutputPin)pin).ConnectedInput.ConnectedOutput = null;
                ((OutputPin)pin).ConnectedInput = null;
                //((OutputPin)pin).ConnectedInputID = -1;

            } else {
                ((InputPin)pin).ConnectedOutput.isConnected = false;
                //((InputPin)pin).ConnectedOutput.ConnectedInputID = -1;
                ((InputPin)pin).ConnectedOutput.ConnectedInput = null;
                ((InputPin)pin).ConnectedOutput = null;
            }
        }

        public void DrawNodeStatus() {

        }
#endif
    }

    #region NodeExtras

    public abstract class NodePin {

        public string Name;
        public bool isConnected = false;
        public NodeBase node;
        public PinType varType;
        public Rect bounds;
        public object Value { get { return val; }
            set {
                if(value!=null)
                    switch (varType) {
                        case PinType.Bool: val = (bool)value; break;
                        case PinType.Float: val = (float)value; break;
                        case PinType.Integer: val = (int)value; break;
                        case PinType.String: val = (string)value; break;
                        case PinType.Vector2: val = (Vector2)value; break;
                        case PinType.Vector3: val = (Vector3)value; break;
                        case PinType.Vector4: val = (Vector4)value; break;
                    }
            }
        }

        public string StyleName {
            get {
                string color = "";
                switch (varType) {
                    case PinType.Bool: color = "Red"; break;
                    case PinType.Actor: color = "Orange"; break;
                    case PinType.Vector2:
                    case PinType.Vector3:
                    case PinType.Vector4: color = "Yellow"; break;
                    case PinType.Float: color = "Green"; break;
                    case PinType.Integer: color = "Cyan"; break;
                    case PinType.Object: color = "Blue"; break;
                    case PinType.String: color = "Purple"; break;
                    case PinType.Logic: color = "White"; break;
                }

                return "Pin" + color + (this.isConnected ? "Closed" : "Open");
            }
        }

        public static Vector2 margin = new Vector2(25, 13);
        public static Vector2 pinSize = new Vector2(23, 17);
        public const float padding = 16;
        public static float Top { get { return margin.y+padding; } }

        private object val;
        private Rect body;

        public NodePin(NodeBase n, object val) : this(n, GetVarType(val)) {
            Value = val;
            // set name of node to name of variable
        }

        public NodePin(NodeBase n, PinType varType) {
            this.varType = varType;
            node = n;
            bounds = new Rect(Vector2.zero, pinSize);
        }

        /// <summary>
        /// returns the name of the base connected node
        /// </summary>
        /// <returns></returns>
        public abstract string ConName();

        public static PinType GetVarType(object variable) {
            if (variable.GetType().Equals(typeof(int))) return PinType.Integer;
            if (variable.GetType().Equals(typeof(bool))) return PinType.Bool;
            if (variable.GetType().Equals(typeof(float))) return PinType.Float;
            if (variable.GetType().Equals(typeof(string))) return PinType.String;
            if (variable.GetType().Equals(typeof(GameObject))) return PinType.Actor; //TODO
            if (variable.GetType().Equals(typeof(Vector2))) return PinType.Vector2;
            if (variable.GetType().Equals(typeof(Vector3))) return PinType.Vector3;
            if (variable.GetType().Equals(typeof(Vector4))) return PinType.Vector4;
            return PinType.Object;
        }

        public Color Color { get{ 
            switch (varType) {
                case PinType.Logic: return Color.white;
                case PinType.Actor:  return Color.Lerp(Color.red, Color.yellow, .5f);
                case PinType.Bool:  return Color.red;
                case PinType.Float:  return Color.green;
                case PinType.Integer:  return Color.cyan;
                case PinType.String:  return Color.Lerp(Color.red, Color.blue, .5f);
                case PinType.Vector2:
                case PinType.Vector3:
                case PinType.Vector4:  return Color.yellow;
                default:  return Color.blue;
            }
} }

#if UNITY_EDITOR

        public bool Contains(Vector2 pos) {
            Rect b = new Rect(bounds);
            b.position += node.GetBody().position;
            return b.Contains(pos);
        }

        public Vector2 Position { get {
                if(bounds==null) return Vector2.zero;
                return bounds.position + node.GetBody().position;
            }
        }
        public Vector2 Center {
            get {
               return Position + new Vector2(pinSize.y / 2f, 8);
            }
        }
#endif  
    }

    /// <summary> Input pin. If disconnected, a constant can be provided </summary>
    [Serializable]
    public class InputPin : NodePin {
        public OutputPin ConnectedOutput = null;
        //public bool isConnected { get {
        //        return ConnectedOutput != null; } }

        public InputPin(NodeBase n, object val) : base(n, val) { }
        public InputPin(NodeBase n, PinType t) : base(n, t) { }

        public override string ConName() {
            return ConnectedOutput.node.name;
        }
        public override string ToString() {
            return "(IN) Node: " + node.STName + " | " + varType+"\n"+node.description;
        }

#if UNITY_EDITOR
        public void DrawConnection() {
            if (!isConnected) return;

            // draw bezier curve from output pin to input pin
            try {
                Vector3 start = ConnectedOutput.Center;
                Vector3 end = this.Center;
                Vector2 startTangent, endTangent;

                float offset = Mathf.Abs(start.x - end.x) / 1.75f;
                offset *= (end.x < start.x) ? -1 : 1;
                startTangent = new Vector2(start.x + offset, start.y);
                endTangent = new Vector2(end.x - offset, end.y);
                //Debug.Log(start + " | " + end + "\n" + startTangent + " | " + endTangent);
                Handles.BeginGUI();
                {
                    Handles.color = Color.white;
                    Handles.DrawBezier(start, end, startTangent, endTangent, this.Color, null, 2);
                }
                Handles.EndGUI();
            } catch {
                Debug.Log("Unable to draw: " + this);
                //Debug.Log("ConnO: " + ConnectedOutput);
            }
        }
#endif
    }

    /// <summary> Output connection. Must always have a value, even if disconnected </summary>
    [Serializable]
    public class OutputPin : NodePin {
        //public int ConnectedInputID = -1;
        public InputPin ConnectedInput = null;
        //public bool isConnected { get { return ConnectedInputID != -1; } }

        public OutputPin(NodeBase n, object val) : base(n, val) { }
        public OutputPin(NodeBase n, PinType t) : base(n, t) { }

        public override string ConName() {
            return "???";
        }
        public override string ToString() {
            return "(OUT) Node: " + (node==null?"???":node.STName) + " | " + varType;
        }
    }

    #endregion
}
