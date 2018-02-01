using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptEditor.Graph;
using UnityEngine;
using UnityEditor;

namespace ScriptEditor.EditorScripts {  


    /// <summary>
    /// Subwindow (View) that shows a tooltip of the current object
    /// </summary>
    public class NodeToolTipView : ViewBase {
        public Vector2 size;
        public string text="";
        private object parent;
        private float delay;
        private GUIStyle style;

        public object Parent { get { return parent; } }

        public NodeToolTipView(object parent, string toolTip) :base("Tooltip"){
            this.parent = parent;
            text = toolTip;
            
            style = skin.label;
            style.padding = new RectOffset(6,6,7,7);
            style.wordWrap = true;
            float w = StaticMethods.Clamp(style.CalcSize(new GUIContent(toolTip)).x, 1, 200);
            size = new Vector2(w, style.CalcHeight(new GUIContent(toolTip), w));
            delay = /*80f*/5; // why does this throttle?
        }

        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);

            // remove view if mouse no longer hovers over object
            bool remove = false;
            if (parent == null) {
                NodeBase node = graph.InsideNode(e.mousePosition);
                remove = (node != null);
            } else {
                if (parent as NodePin != null) {
                    remove = (!((NodePin)parent).Contains(e.mousePosition));
                } else {
                    if (parent as NodeBase != null) {
                        remove = (!((NodeBase)parent).Contains(e.mousePosition));
                        if (!graph.nodes.Contains((NodeBase)parent)) remove = true;
                    }
                }
            }

            if (remove) window.toolTipView = null;

            if (delay > 0)
                delay -= Time.deltaTime;
            else {
                try {
                    GUI.Box(body, "", skin.GetStyle("NodeCreationBackground"));
                    GUILayout.BeginArea(body);
                    GUILayout.Label(text, style);
                    GUILayout.EndArea();
                } catch { }
            }
        }
    }

    /// <summary>
    /// Subwindow (View) that shows a popup for naming promoted variables
    /// only; variable type dictated by selected pin.
    /// Will not create a variable of similar scope with same name
    /// </summary>
    public class VariableCreateView : ViewBase {

        public Vector2 mouseLoc;
        public static Vector2 DefaultSize = new Vector2(400, 100);

        private NodePin pinToAttach;
        private string varName;
        private Variable.VarScope scope = Variable.VarScope.Local;
        private Color defColor;

        public VariableCreateView(Vector2 loc,NodePin pin) : base("Variable Creation") {
            mouseLoc = loc;
            pinToAttach = pin;
            defColor = skin.font.material.color;
        }

        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);

            // reposition to fit on screen
            float dy = window.position.height - (body.height + body.y);
            if (dy < 0) body.y += dy;
            float dx = window.position.width - (body.width + body.x);
            if (dx < 0) body.x += dx;

            ProcessEvents(e);
            
            // check validity of input name
            // else case should look into the "Global" list of variables
            bool isValid = scope==Variable.VarScope.Local ? !graph.ContainsVariable(varName) : true;
            GUIStyle style = new GUIStyle();
            style.font.material.color = isValid ? defColor : Color.red;

            varName = EditorGUILayout.TextField(varName);
            scope = (Variable.VarScope)EditorGUILayout.EnumPopup("Scope:", scope);
            if(GUILayout.Button("Create Variable") && isValid && scope == Variable.VarScope.Local) {
                Promote();
            }
        }

        private void Promote() {
            Variable var = new Variable(varName, pinToAttach.varType, 
                pinToAttach.isInput?null:pinToAttach.Value, scope);

            // put variable into appropiate dictionary
            if (scope == Variable.VarScope.Local) {
                graph.CreateVariable(var);
            } else {

            }

            // create node
            Vector2 off = new Vector2(10, 0);
            off *= pinToAttach.isInput ? -1 : 1;
            Vector2 loc = pinToAttach.Center + off;
            VarNode node = NodeUtilities.CreateNode(graph, var, !pinToAttach.isInput, loc);


            // establish connection between selected pin and nodes pin
            if (pinToAttach != null) {
                graph.ConnectPins(pinToAttach.isInput ? (InputPin)pinToAttach : node.inPins[0],
                    pinToAttach.isInput ? node.outPins[0] : (OutputPin)pinToAttach);
            }


            //// establish connection between selected pin and first input/output of matching type
            //// e.g. float to float
            //if (pinToAttach != null) {
            //    if (!pinToAttach.isInput) {
            //        foreach (InputPin ip in node.inPins)
            //            if (ip.varType == pinToAttach.varType) {
            //                graph.ConnectPins(ip, (OutputPin)pinToAttach);
            //                break;
            //            }
            //    } else {
            //        foreach (OutputPin op in node.outPins)
            //            if (op.varType == pinToAttach.varType) {
            //                graph.ConnectPins((InputPin)pinToAttach, op);
            //                break;
            //            }
            //    }
            //}
        }
    }

    /// <summary>
    /// Subwindow (View) that allows the user to add a new node to the graph
    /// in the Node Editor
    /// </summary>
    public class NodeCreateView : ViewBase{

        public Vector2 mouseLoc, size;
        private Vector2 scrollPos;
        Array ops, cTypes, fTypes;
        NodePin pinToAttach;

        bool showMath, showControls, showDialogues, showFunc;

        public NodeCreateView(Vector2 mousePos, NodePin selected):base("Node View") {
            mouseLoc = mousePos;
            // to DO
            size = new Vector2(200, 100);
            scrollPos = new Vector2();
            pinToAttach = selected;

            ops = Enum.GetValues(typeof(MathNode.OpType));
            cTypes = Enum.GetValues(typeof(ControlNode.ControlType));
            fTypes = Enum.GetValues(typeof(FunctionNode.FunctionType));
        }

        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);

            // reposition to fit on screen
            float dy = window.position.height - (body.height + body.y);
            if (dy < 0) body.y += dy;
            float dx = window.position.width - (body.width + body.x);
            if (dx < 0) body.x += dx;

            ProcessEvents(e);

            Vector2 margin = new Vector2(6, 12);
            size = new Vector2(200, 100);

            //Rect viewRect = new Rect(0, 0, body.width, body.height);
            //Rect ctrlRect = new Rect(x,y, ctrlWidth, ctrlHeight);
            try {
                GUI.Box(body, "", skin.GetStyle("NodeCreationBackground"));
                GUILayout.BeginArea(new Rect(body.x + margin.x, body.y + margin.y,
                    body.width - 2 * margin.x, body.height - 2 * margin.y));
                scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
                EditorGUILayout.BeginVertical();
                {
                    if (showDialogues = EditorGUILayout.Foldout(showDialogues, "Dialogue")) {
                        foreach (ControlNode.ControlType cT in ControlNode.dialogControls) {
                            if (size.y < 400) size.y += 10;
                            if (GUILayout.Button(cT.ToString(), skin.GetStyle("NodeCreationButton"))) {
                                CreateNode(graph, NodeType.Control, VarType.Exec, cT);
                            }
                        }
                    }

                    if (showControls = EditorGUILayout.Foldout(showControls, "Controls")) {
                        foreach (ControlNode.ControlType cT in cTypes) {
                            if (ControlNode.dialogControls.Contains(cT)) continue;
                            if (size.y < 400) size.y += 10;
                            if (cT != ControlNode.ControlType.Custom &&
                                GUILayout.Button(cT.ToString(), skin.GetStyle("NodeCreationButton"))) {
                                if (cT == ControlNode.ControlType.Cast) {
                                    NodeCreatePopup.Init(NodeType.Control, cT, pinToAttach, mouseLoc);
                                    Destroy(true);
                                } else CreateNode(graph, NodeType.Control, VarType.Exec, cT);

                            }
                        }

                        if (GUILayout.Button("Sub Start", skin.GetStyle("NodeCreationButton"))) {
                            CreateNode(graph, NodeType.Event, VarType.Object, "New SubStart");
                        }

                        //if (GUILayout.Button("End", skin.GetStyle("NodeCreationButton"))) {
                        //    CreateNode(graph, NodeType.Event, VarType.Object, true);
                        //}
                    }

                    if (showMath = EditorGUILayout.Foldout(showMath, "Math")) {
                        foreach (MathNode.OpType op in ops) {
                            if (size.y < 400) size.y += 10;
                            if (GUILayout.Button(op.ToString(), skin.GetStyle("NodeCreationButton"))) {
                                // create math FunctionNode
                                MathNode mn = new MathNode();
                                mn.Initialize(); mn.Construct(op, VarType.Float, 2);
                                NodeCreatePopup.Init(NodeType.Math, op, pinToAttach, mouseLoc,
                                    mn.multiplePins ? -1 : 2);
                                Destroy(true);
                            }
                        }
                    }

                    if (showFunc = EditorGUILayout.Foldout(showFunc, "Functions")) {
                        foreach (FunctionNode.FunctionType cT in fTypes) {
                            if (size.y < 400) size.y += 10;
                            if (GUILayout.Button(cT.ToString(), skin.GetStyle("NodeCreationButton"))) {
                                CreateNode(graph, NodeType.Function, VarType.Object, cT);
                            }
                        }
                    }
                }

                EditorGUILayout.EndVertical();
                GUILayout.EndScrollView();
                GUILayout.EndArea();
            } catch {
                GUILayout.EndArea();
            }
        }

        public void CreateNode(NodeGraph GraphObj, NodeType nT, VarType pT, object subType, int nodeCount =2) {
            NodeBase node = null;
            switch (nT) {
                case NodeType.Math:
                    node = NodeUtilities.CreateNode(GraphObj, NodeType.Math, subType, 
                        pT, mouseLoc, nodeCount);
                    break;
                case NodeType.Event:
                    node = NodeUtilities.CreateNode(GraphObj, nT, subType, mouseLoc);
                    break;
                case NodeType.Fetch:

                    break;
                case NodeType.Control:
                    if ((ControlNode.ControlType)subType != ControlNode.ControlType.Cast)
                        node = NodeUtilities.CreateNode(GraphObj, nT, subType, mouseLoc);
                    break;
                case NodeType.Function:
                    node = NodeUtilities.CreateNode(GraphObj, nT, subType, 
                        pT, mouseLoc, nodeCount);
                    break;
            }
            //Debug.Log(node);

            // establish connection between selected pin and first input/output of matching type
            // e.g. float to float
            if (pinToAttach != null) {
                if (!pinToAttach.isInput) {
                    foreach(InputPin ip in node.inPins)
                        if (ip.varType == pinToAttach.varType) {
                            graph.ConnectPins(ip, (OutputPin)pinToAttach);
                            break;
                        }
                } else {
                    foreach (OutputPin op in node.outPins)
                        if (op.varType == pinToAttach.varType) {
                            graph.ConnectPins((InputPin)pinToAttach, op);
                            break;
                        }
                }
            }
            Destroy(false);
        }

        public void Destroy(bool popup) {
            if (window.workView.SelectedPin != null && !popup)
                window.workView.SelectedPin = null;
            window.nodeCreateView = null;
        }

        public override void ProcessEvents(Event e) {
            base.ProcessEvents(e);

            if (e.type == EventType.ContextClick || e.type == EventType.MouseDown) {
                //Debug.Log("NCV clicked");
                if (!body.Contains(e.mousePosition)){
                    // destroy window
                    Destroy(false);
                }

            }
        }

    }
}
