using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
//using ScriptEditor.EditorScripts;
#endif
using System;

namespace ScriptEditor.Graph {

    public enum NodeType {
        Math, Function, Fetch, Dialog, Control, Event, Variable
    }

    public enum VarType {
        Exec, Bool, Integer, Float, String, Vector2, Vector3, Vector4,
        Color, Object, Actor,
    }

    /// <summary>
    /// The base node class. Nodes are what the graphs are made from and can contain a number of 
    /// either input or output pins, or a combination of both. See _____ for a list of child classes
    /// </summary>
    [Serializable]
    public abstract class NodeBase : ScriptableObject {
        
        public string  description;
        public bool isSelected;

        /// <summary> is the node finished executing </summary>
        protected bool finished = false;

        /// <summary> whether or not more input pins can be added to node is ultimately defined by its type </summary>
        [SerializeField] protected NodeType nodeType;

        [RequiredInHierarchy(typeof(DialogueController))]
        protected DialogueController dc;

        public bool multiplePins = false;
        public bool compiled = false;
        public NodeGraph parentGraph;
        // visual constants
        public const float Top = 56;
        public const float Bottom = 23 + NodePin.padding;
        protected const float Width = 175;
        protected bool _set = false;
        protected bool setupCompleted { get { return _set; } set { Debug.Log("djidjiedjijed"); _set = value; } }
        
        public List<OutputPin> outPins;
        public List<InputPin> inPins;
        public List<NodeError> errors;
        public int MaxNodes { get { return Mathf.Max(inPins.Count, outPins.Count); } }
        public NodeType Node_Type { get { return nodeType; } }

        /// <summary>
        /// Whether node has finished execution
        /// </summary>
        public bool IsFinished { get { return finished; } }

        /// <summary>[unused] whether or not the node can be logically executed </summary>
        public bool IsExecutable { get {
                foreach (InputPin ip in inPins) if (ip.varType == VarType.Exec)
                        return true;
                return false;
            } }

        ///<summary> whether or not the executable pin's output can be ignored; true only when exec input is disconnected </summary>
        public bool Ignorable {
            get {
                foreach (InputPin ip in inPins)
                    if (ip.varType == VarType.Exec)
                        return !ip.isConnected;
                return false;
            }
        }

        /// <summary> get the longest name name from input pins </summary>
        public string LongestInName {
            get {
                string res = "";
                foreach (InputPin i in inPins)
                    if (!String.IsNullOrEmpty(i.Name))
                        res = (i.Name.Length > res.Length) ? i.Name : res;
                return res;
            }
        }

        /// <summary> get the longest name from the output pins </summary>
        public string LongestOutName {
            get {
                string res = "";
                foreach (OutputPin o in outPins)
                    if (!String.IsNullOrEmpty(o.Name))
                        res = (o.Name.Length > res.Length) ? o.Name : res;
                return res;
            }
        }

        /// <summary> all input and output pins attached to node </summary>
        public List<NodePin> AllPins { get {
                List<NodePin> pins = new List<NodePin>(outPins.ToArray());
                pins.AddRange(inPins.ToArray());
                return pins;
            } }

        /// <summary> string evaluation of nodeType </summary>
        public string NTName { get { return Enum.GetName(typeof(NodeType), nodeType); } }

        /// <summary> string evaluation of node's subtype. value depends on nodeType </summary>
        public string STName {
            get {
                switch (nodeType) {
                    case NodeType.Control:
                        return this.GetType().ToString();
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

        /// <summary> draw area of the node, including borders and margins </summary>
        [SerializeField] protected Rect body;
        [SerializeField] protected Rect viewRect;
        public GUISkin skin;

        public Rect GetBody() { return body; }
        public void SetPos(Vector2 pos) { body.position = pos; }
        public void Pan(Vector2 pan) { body.position += pan; this.pan += pan; }
        protected Vector2 pan=Vector2.zero;

        public void OnEnable() {
            errors = new List<NodeError>();
            GetEditorSkin();
        }
        protected void SetName (string title) {
            this.name = title;
        }

        /// <summary>
        /// debug version, meant to preserve body location
        /// </summary>
        public void Resize(bool t) {
            Vector2 pos = body.position;
            Resize();
            body.position = pos;
        }

        /// <summary>
        /// Resize the body of node. Necessary when number of pins changes or the width of text to be displayed changes.
        /// </summary>
        public virtual void Resize() {
            GUIStyle style = skin.label;
            float nameWidth = style.CalcSize(new GUIContent(LongestInName)).x +
                style.CalcSize(new GUIContent(LongestOutName)).x;
            float pinWidth = 50;
            float w = 2 * pinWidth + nameWidth + 30;
            body = new Rect(0, 0, Mathf.Max(w, Width), Top + NodePin.Top * MaxNodes + Bottom);

            // widen body based on disconnected, changeable inputs
            // for types with multiple inputs, this offset is based only on widest field
            Vector2 inputWidths = Vector2.zero;
            //foreach (NodePin pin in AllPins) {
            //    if (!pin.isConnected) {
            //        switch (pin.varType) {
            //            case VarType.Bool: // checkbox
            //                break;
            //            case VarType.Integer: // int field
            //                break;
            //            case VarType.Float: // float field
            //                break;
            //            case VarType.String: // text field
            //                break;
            //            case VarType.Vector2: // 2 float fields
            //                break;
            //            case VarType.Vector3: // 3 float fields
            //                break;
            //            case VarType.Vector4: // 4 float fields
            //                break;
            //            case VarType.Color: // box showing color
            //                break;
            //        }
            //    }
            //}

            body.size += inputWidths;

            // set pin visual information
            foreach (NodePin pin in AllPins) {
                  float x = !pin.isInput ? body.width - NodePin.margin.x - NodePin.pinSize.x : NodePin.margin.x;
                float y = !pin.isInput ? outPins.IndexOf((OutputPin)pin) :
                    inPins.IndexOf((InputPin)pin);

                //float x, y; x = y = 9;
                pin.bounds.position = new Vector2(x, Top + NodePin.margin.y + y * NodePin.Top);
            }
        }

        /// <summary> makes the node ready for display and value storage </summary>
        public virtual void Initialize() {
            GetEditorSkin();
            inPins = new List<InputPin>();
            outPins = new List<OutputPin>();
            errors = new List<NodeError>();
            //hideFlags = HideFlags.HideInHierarchy;
        }

        /// <summary> logical execution of node </summary>
        public virtual void Execute() {
            if (!setupCompleted) Setup();
            Finalization();
        }

        /// <summary> necessary setup for the beginning of logical execution </summary>
        protected virtual void Setup() {
            setupCompleted = true;
        }

        /// <summary> finish execution of node, prepare for next node </summary>
        protected virtual void Finalization() {
            finished = true;
        }

        /// <summary> Return the next executable node </summary>
        public virtual NodeBase GetNextNode() {
            if (outPins.Count > 0) {
                if (outPins[0].varType == VarType.Exec)
                    return outPins[0].ConnectedInput.node;
            }

            return null;
        }

        /// <summary> add new pin with base variable type</summary>
        public virtual void AddInputPin() {
            if (multiplePins && inPins.Count < 16) {
                //ScriptableObject.Instantiate<InputPin>();
                inPins.Add(new ValueInputPin(this, inPins[0].varType));
                Resize();
            }
        }

        /// <summary> remove last input pin</summary>
        public virtual void RemovePin() {
            if (multiplePins && inPins.Count > 2) {
                InputPin pin = inPins[inPins.Count - 1];
                inPins.Remove(pin);
                RemoveConnection(pin);
                Resize();
            }
        }

        public void setCompiled() {
            compiled = true;
            //errors.RemoveAt(errors.IndexOf(new NodeError(NodeError.ErrorType.NotConnected)));
        }
        
        /// <summary> Update flow of values through connected pins  </summary>
        public virtual void Lookup(bool compileTime) {
            if (compileTime && parentGraph.lookupStack.Contains(this)) {
                errors.Add(new NodeError(NodeError.ErrorType.DependencyCycle));
                return;
            }

            setCompiled();
            //Debug.Log("Look up "+this);
            parentGraph.lookupStack.Push(this);
            foreach(InputPin ip in inPins) {
                if (ip.varType!=VarType.Exec && ip.isConnected) {
                    // recurse
                    ip.ConnectedOutput.node.Lookup(compileTime);
                }
            }
            parentGraph.lookupStack.Pop();
        }

        /// <summary>
        /// Process node connections and prune for errors
        /// </summary>
        public virtual void Compile() {
            // check if infinite loop is possible
            if (parentGraph.compileStack.Contains(this)) {
                ControlNode cn;
                if ((cn = this as ControlNode) != null) {
                    if (cn.GetType() != typeof(BranchNode) &&
                        cn.GetType() != typeof(ChoiceNode)) {
                        errors.Add(new NodeError(NodeError.ErrorType.InfiniteLoop));
                        return;
                    } else {
                        // branch must be controlled by variable to prevent infinite loop!
                        if (cn.GetType() == typeof(BranchNode) &&
                            !cn.inPins[1].isConnected) {
                            errors.Add(new NodeError(NodeError.ErrorType.InfiniteLoop));
                            return;
                        }
                    }
                } else {
                    errors.Add(new NodeError(NodeError.ErrorType.InfiniteLoop));
                    return;
                }
            }

            // check for default connection
            if(this is ChoiceNode) {
                ChoiceNode c = ((ChoiceNode)this);
                if (!c.outPins[c.defaultChoice].isConnected)
                    errors.Add(new NodeError(NodeError.ErrorType.NoDefault));
            }

            setCompiled();
            if (this as EndNode != null) parentGraph.foundEnd = true;
            Debug.Log("Compiling: " + this);
            parentGraph.compileStack.Push(this);
            foreach(OutputPin op in outPins) {
                if (op.isConnected)
                    if (op.varType == VarType.Exec) {
                        //Debug.Log("OP.CO.NO: " + op.ConnectedInput.node);
                        op.ConnectedInput.node.Compile();
                    }
            }

            foreach(InputPin ip in inPins) {
                if (ip.varType != VarType.Exec && ip.isConnected) {
                    if (!ip.ConnectedOutput.node.Ignorable)
                        ip.ConnectedOutput.node.Lookup(true);
                }
            }
            parentGraph.compileStack.Pop();
        }

        /// <summary>
        /// check if the mouse is inside the entire node
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
        /// determine if the mouse's location is inside a pin;
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns> the pin in which the mouse collides</returns>
        public NodePin InsidePinText(Vector2 mousePos) {
            foreach(NodePin pin in AllPins) {
                if (pin.TxtContains(mousePos)) return pin;
            }
            return null;
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

            string header = name;
            if (this is DialogueNode) {
                DialogueNode node = (DialogueNode) this;
                if (!String.IsNullOrEmpty(node.text)) {
                    // make sure this text simplification doesn't lag shit every draw call
                    if (String.IsNullOrEmpty(node.header)) {
                        node.header = Miscellaneous.MinimalizeWidthFull(node.text,
                       body.width - 60, skin.label);
                    }
                    header = node.header;
                }
            }
                   
            GUI.Box(body, 
                header,
                isSelected ? skin.GetStyle("Node" + NTName + "Selected") :
                skin.GetStyle("Node" + NTName + "Background"));
            DrawPins();
        }

        /// <summary>
        /// Draws the pins for the Node; 
        /// Each NodeType is drawn with a different color
        /// </summary>
        public  void DrawPins() {
            //try {
                GUILayout.BeginArea(body);
                foreach (NodePin pin in AllPins) {
                    Rect txt = pin.TextBox;
                    GUI.Box(pin.bounds, "", skin.GetStyle(pin.StyleName));
                    GUI.Label(txt, pin.Name);

                // casting error? wtf, C#
                Vector2 offset = new Vector2(txt.size.x-15, 0);
                    if (!pin.isConnected && pin.isInput) {
                    //Debug.Log("Pin: " + pin + "\nVal: " + pin.Value);
                        switch (pin.varType) {
                            case VarType.Bool: // checkbox
                                //pin.Value = (bool)EditorGUI.Toggle(new Rect(txt.position + offset, new Vector2(15, 15)),
                                //    (bool)pin.Value);
                                break;
                            case VarType.Integer: // int field
                            //pin.Value = (int)EditorGUI.IntField(new Rect(txt.position + offset, new Vector2(35, 20)),
                            //    (int)pin.Value);
                            break;
                            case VarType.Float: // float field
                            //pin.Value = (float)EditorGUI.FloatField(new Rect(txt.position + offset, new Vector2(45, 20)),
                            //    (float)pin.Value);
                            break;
                            case VarType.String: // text field
                            //pin.Value = (string)EditorGUI.TextField(new Rect(txt.position + offset, new Vector2(75, 20)),
                            //    (string)pin.Value);
                            break;
                            case VarType.Vector2: // 2 float fields
                                //pin.Value = (Vector2)EditorGUI.Vector2Field(new Rect(txt.position + offset, new Vector2(65, 20)),
                                //    "", (Vector2)pin.Value);
                                break;
                            case VarType.Vector3: // 3 float fields
                                //pin.Value = (Vector3)EditorGUI.Vector3Field(new Rect(txt.position + offset, new Vector2(85, 20)),
                                //    "", (Vector3)pin.Value);
                                break;
                            case VarType.Vector4: // 4 float fields
                                //pin.Value = (Vector4)EditorGUI.Vector2Field(new Rect(txt.position + offset, new Vector2(105, 20)),
                                //    "", (Vector2)pin.Value);
                                break;
                            case VarType.Color: // box showing color
                                //pin.Value = (Color) EditorGUI.ColorField(new Rect(txt.position + offset, new Vector2(65, 20)),
                                //    (Color)pin.Value);
                                break;
                        }
                    }
                }
                GUILayout.EndArea();
            //} catch { }
        }

        public void DrawConnections() {
            foreach (NodePin pin in AllPins)
                if((!pin.isInput && pin.varType==VarType.Exec) || 
                    (pin.isInput && pin.varType != VarType.Exec))
                    pin.DrawConnection();
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
                    //body.position = StaticMethods.SnapTo(body.position, 5);
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
            pin.node.parentGraph.ResetCompiledStatus();

            if (pin.isConnected) {
                if (pin is OutputPin) {
                    ((OutputPin)pin).ConnectedInput.isConnected = false;
                    ((OutputPin)pin).ConnectedInput.ConnectedOutput = null;
                    ((OutputPin)pin).ConnectedInput = null;

                } else {
                    ((InputPin)pin).ConnectedOutput.isConnected = false;
                    ((InputPin)pin).ConnectedOutput.ConnectedInput = null;
                    ((InputPin)pin).ConnectedOutput = null;
                }
            }   
        }

        public void DrawNodeStatus() {

        }
#endif
    }
}
