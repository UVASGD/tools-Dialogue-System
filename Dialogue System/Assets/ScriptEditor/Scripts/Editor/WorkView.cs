using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ScriptEditor.Graph;
using UnityEditor;

namespace ScriptEditor.EditorScripts {
    /// <summary>
    /// This class defines the base workspace for the node editor, where the user
    /// is able to modify a Dialogue Scripts and compile thema
    /// </summary>
    public class WorkView : ViewBase{

        Texture2D backgroundTexture;
        public NodePin SelectedPin;
        public Vector2 pan;

        public WorkView() : base(GetTitle()) {
            backgroundTexture = getTexture();
            if(backgroundTexture!=null)
                backgroundTexture.wrapMode = TextureWrapMode.Repeat;
        }

        #region Override
        /// <summary>
        /// draw view GUI, buttons and all
        /// </summary>
        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);
            ProcessEvents(e);
            if (graph != null) {
                title = graph.name;
            } else {
                title = GetTitle();
            }
            
            GUI.DrawTextureWithTexCoords(new Rect(body.position+pan, body.size), backgroundTexture, new Rect(0,0, body.width / backgroundTexture.width,
                body.height / backgroundTexture.height));
            GUI.Box(body, title, skin.GetStyle("WorkViewBackground"));
            if (graph != null) {
                float btnH = 25, btnMY = 10;
                graph.DrawGraph(e, body);
                if (GUI.Button(new Rect(0,editorRect.y+btnMY, 80, btnH), "Compile"))
                    graph.Compile();
                GUI.Toggle(new Rect(100, editorRect.y + btnMY, 200, btnH), graph.compiled, "Has Compiled: ");
                if (GUI.Button(new Rect(0, editorRect.y + btnH + 2 * btnMY, 80, btnH), "Save"))
                    NodeUtilities.UpdateGraph(graph);
                if (GUI.Button(new Rect(0, editorRect.y + 2 * (btnH + 2 * btnMY), 80, btnH), "Resize"))
                    foreach(NodeBase n in graph.nodes) n.Resize(true);

            }

            if (SelectedPin != null) {
            // is a window currently showing? if so, don't constantly draw to mouse pos
                if (NCPopup != null) {
                    DrawConnectionToMouse(NCPopup.mouseLoc);
                } else if (window.nodeCreateView != null)
                    DrawConnectionToMouse(window.nodeCreateView.mouseLoc);
                else DrawConnectionToMouse(e.mousePosition);
            }
        }

        /// <summary>
        /// Draw a bezier curve from selected node to the mouse
        /// </summary>
        /// <param name="end"> location of the mouse</param>
        private void DrawConnectionToMouse(Vector2 mousePos) {
            Vector2 end = SelectedPin.isInput ? SelectedPin.Center : mousePos;
            Vector3 start = SelectedPin.isInput ? mousePos : SelectedPin.Center;
            Vector2 startTangent, endTangent;
            
            float offset = Mathf.Max(Mathf.Abs(start.x - end.x) / 1.75f,1);
            startTangent = new Vector2(start.x + offset, start.y);
            endTangent = new Vector2(end.x - offset, end.y);

            Handles.BeginGUI(); {
                Handles.color = Color.white;
                Handles.DrawBezier(start, end, startTangent, endTangent, SelectedPin._Color, null, 2);
            }
            Handles.EndGUI();
        }

        /// <summary>
        /// determine whether the current unity is in proskin theme and returns the appropiate theme
        /// 
        /// NOTE: this should be located in a static class; 
        /// right now, it happens in multiple classes and can be combined
        /// </summary>
        /// <returns></returns>
        Texture2D getTexture() {
            string s = EditorGUIUtility.isProSkin ? "workViewBackgroundDark" : "workViewBackgroundLight";
            return Resources.Load<Texture2D>("Textures/Editor/" + s);
        }

        private static string GetTitle() {
            return "Work View";
        }

        private bool shift=false;

        /// <remarks>TODO: this function takes up most of Unity's OnGui call, leaving no time for anything else.
        /// Please optimize! </remarks>
        public override void ProcessEvents(Event e) {
            base.ProcessEvents(e);
            Vector2 mousePos = e.mousePosition;
            NodeBase node = (graph != null) ? graph.InsideNode(mousePos) : null;
            NodePin pin = null, txtPin = null;
            if (node != null) {
                pin = node.InsidePin(mousePos);
                txtPin = node.InsidePinText(mousePos);
            }

            // pan the view
            //***Not highest priority for optimization
            if (body.Contains(e.mousePosition)) {
                //pan += e.delta;
                if (e.type == EventType.MouseDrag && e.button == 2) {
                    foreach (var n in graph.nodes) {
                        n.Pan(e.delta);
                    }
                }
            }

            // zoom the view
            //if (e.type == EventType.ScrollWheel) {
            //    Debug.Log(e.delta);
            //     float tmp = StaticMethods.Clamp(window.zoomScale + e.delta.y, 1.0f / 25.0f, 2.0f, true);
            //    Debug.Log(tmp);
            //    window.zoomScale = tmp;
            //}

            // toolTip
            if (window.nodeCreateView == null) {
                if (window.toolTipView == null) {
                    if (node != null) {
                        if (pin == null && txtPin == null) {
                            window.toolTipView = new NodeToolTipView(node, node.description);
                        } else {
                            if (SelectedPin != null && pin != null) {
                                // check if selected input pin can be cast to the highlighted output pin and vice versa
                                // if check is valid, show a tooltip
                                if (!SelectedPin.GetType().Equals(pin.GetType()) && pin.varType != SelectedPin.varType) {
                                    if (ControlNode.castables.ContainsKey(SelectedPin.varType.ToString())) {
                                        if (ControlNode.castables[SelectedPin.varType.ToString()].Contains(pin.varType.ToString())) {
                                            window.toolTipView = new NodeToolTipView(pin,
                                                ("Cast " + Enum.GetName(typeof(VarType), SelectedPin.varType)
                                                 + " to " + Enum.GetName(typeof(VarType), pin.varType)));
                                        }
                                    }
                                }
                            } else {
                                NodePin p = pin == null ? txtPin : pin;
                                if (!String.IsNullOrEmpty(p.Description))
                                    window.toolTipView = new NodeToolTipView(p, p.Description);
                            }
                        }
                    } else if (SelectedPin != null) {
                        window.toolTipView = new NodeToolTipView(null, "Create New Node");
                    }
                } else {
                    if (pin != null)
                        if (!String.IsNullOrEmpty(pin.Description) && window.toolTipView.Parent != pin) {
                            window.toolTipView = new NodeToolTipView(pin, pin.Description);
                        }
                }
            }

            // hit escape
            // deselect the current pin
            if (e.keyCode == KeyCode.Escape) {
                if (SelectedPin != null) SelectedPin = null;
            }

            // hold shift
            // activate shift key relate functions
            if(e.keyCode == KeyCode.LeftShift || e.keyCode == KeyCode.RightShift) {
                shift = (e.type == EventType.KeyDown);
            }

            // press delete
            // delete the current node
            if (graph != null) {
                if (e.keyCode == KeyCode.Delete && graph.SelectedNode != null) {
                    graph.DeleteNode(graph.SelectedNode);
                    graph.SelectedNode = null;
                    Selection.activeObject = null;
                }
            }
            
            if (e.type == EventType.MouseDrag)
                window.toolTipView = null;

            // right click a thing
            // show a context menu
            if (window.nodeCreateView == null && e.type == EventType.ContextClick) {
                GenericMenu menu = new GenericMenu();
                if (graph != null) {
                    if (node != null) {
                        if (pin != null) {
                            //Debug.Log("Right Clicked a PIN");
                            // pin connection menu

                            //break all connections
                            if (pin.IsConnected) {
                                menu.AddItem(new GUIContent("Break Connection"), false, NodeBase.RemoveConnection, pin);
                                {
                                    //break individual Conections
                                    //if (pin.isInput) {
                                    //    foreach (OutputPin n in pin.node.outPins) {
                                    //        menu.AddItem(new GUIContent("Break Connection to " + pin.ConName()), false, node.RemovePin, "pin obj?");
                                    //    }
                                    //} else {
                                    //    foreach (InputPin n in pin.node.inPins) {
                                    //        menu.AddItem(new GUIContent("Break Connection to " + pin.ConName()), false, node.RemovePin, "pin obj?");
                                    //    

                                    //}
                                }

                                //add separator
                                menu.AddSeparator("");
                            }

                            // Promote input to variable Node
                            if (!pin.IsConnected && pin.isInput && pin.varType != VarType.Exec) {
                                menu.AddItem(new GUIContent("Promote to Variable"), false, PromoteVariable, pin);
                            }
                        }

                        // cut, copy, duplicate, delete
                        if (!node.GetType().Equals(typeof(StartNode))) {
                            menu.AddItem(new GUIContent("Cut Node"), false, CutNode, node);
                            menu.AddItem(new GUIContent("Copy Node"), false, CopyNode, node);
                            menu.AddItem(new GUIContent("Duplicate Node"), false, DupeNode, node);
                            menu.AddItem(new GUIContent("Delete Node"), false, graph.DeleteNode, node);
                        } else {
                            menu.AddDisabledItem(new GUIContent("Cut Node"));
                            menu.AddDisabledItem(new GUIContent("Copy Node"));
                            menu.AddDisabledItem(new GUIContent("Duplicate Node"));
                            menu.AddDisabledItem(new GUIContent("Delete Node"));
                        }

                        menu.ShowAsContext();
                        Event.current.Use();
                    } else if (window.nodeCreateView == null) {
                        // add node creation window
                        //Debug.Log("Right Clicked nowhere");
                        window.nodeCreateView = new NodeCreateView(mousePos, SelectedPin);
                        window.toolTipView = null;
                        //SelectedPin = null;
                    } else {
                        //Debug.Log("NCV: " + new Rect(window.nodeCreateView.mouseLoc, window.nodeCreateView.size));
                    }
                }
            }

            // left click a pin
            //***high priority for optimization
            if (window.nodeCreateView == null && e.button == 0 &&
                e.type == EventType.MouseDown) {
                if (node != null) {
                    if (pin != null) {
                        if (SelectedPin != null) {
                            // must connect inputs to outputs
                            if (!SelectedPin.GetType().Equals(pin.GetType())) {
                                if (pin.varType == SelectedPin.varType) {
                                    if (pin.node != SelectedPin.node) {
                                        //if (pin.isConnected && pin.isInput && pin.varType==VarType.Exec)
                                        //    NodeBase.RemoveConnection(pin);

                                        if (SelectedPin.isInput)
                                            graph.ConnectPins((InputPin)SelectedPin, (OutputPin)pin);
                                        else
                                            graph.ConnectPins((InputPin)pin, (OutputPin)SelectedPin);
                                    }
                                } else {
                                    // if vartype of selectedPin can be cast to clicked pin,
                                    if (ControlNode.castables.ContainsKey(SelectedPin.varType.ToString())) {
                                        if (ControlNode.castables[SelectedPin.varType.ToString()].Contains(pin.varType.ToString())) {
                                            //Debug.Log("sPin can be cast to Pin");
                                            // create a cast node
                                            ControlNode CN;
                                            if (SelectedPin.isInput) {
                                                CN = (ControlNode)NodeUtilities.CreateNode(graph, pin.varType,
                                                    SelectedPin.varType, e.mousePosition);
                                                //CN.outPins[0].ConnectedInputID = SelectedPin.node.inPins.IndexOf((InputPin)SelectedPin);
                                                //((OutputPin)pin).ConnectedInputID = 0;

                                                graph.ConnectPins((InputPin)SelectedPin, CN.outPins[0]);
                                                graph.ConnectPins(CN.inPins[0], (OutputPin)pin);
                                            } else {
                                                CN = (ControlNode)NodeUtilities.CreateNode(graph, SelectedPin.varType,
                                                    pin.varType, e.mousePosition);
                                                //CN.outPins[0].ConnectedInputID = pin.node.inPins.IndexOf((InputPin)pin);
                                                //((OutputPin)SelectedPin).ConnectedInputID = 0;

                                                graph.ConnectPins((InputPin)pin, CN.outPins[0]);
                                                graph.ConnectPins(CN.inPins[0], (OutputPin)SelectedPin);
                                            }
                                        }
                                    }
                                }

                                SelectedPin = null;
                            }
                        } else {
                            SelectedPin = pin;
                        }
                    }
                } else {
                    if (SelectedPin != null) {
                        SelectedPin = null;
                        window.toolTipView = null;
                    }
                }
            }

        }
        #endregion

        public void CutNode(object o) {

        }

        public void CopyNode(object o) {

        }

        public void DupeNode(object o) {

        }

        public void PromoteVariable(object o) {
            if (!o.GetType().Equals(typeof(NodePin))) return;
            NodePin pin = (NodePin)o;
            if(window.varCreateView != null) {
                window.varCreateView = new VariableCreateView(pin.Center, pin);
            }
        }
    }
}
