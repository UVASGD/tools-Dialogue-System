using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ScriptEditor.Graph;
using UnityEditor;

namespace ScriptEditor.EditorScripts {
    public class WorkView : ViewBase{

        Texture2D backgroundTexture;
        NodePin SelectedPin;
        public Vector2 pan;

        public WorkView() : base(GetTitle()) {
            backgroundTexture = getTexture();
            if(backgroundTexture!=null)
                backgroundTexture.wrapMode = TextureWrapMode.Repeat;
        }

        #region Override
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
                graph.UpdateGraph(e);
                graph.DrawGraph(e, body);
            }

            if (SelectedPin != null) {
                DrawConnectionToMouse(e.mousePosition);
            }
        }

        /// <summary>
        /// Draw a bezier curve from selected node to the mouse
        /// </summary>
        /// <param name="end"> location of the mouse</param>
        private void DrawConnectionToMouse(Vector2 end) {
            Vector3 start = SelectedPin.Center;
            Vector2 startTangent, endTangent;

            float offset = Mathf.Abs(start.x - end.x) / 1.75f;
            offset *= (end.x < start.x) ? -1 : 1;
            startTangent = new Vector2(start.x + offset, start.y);
            endTangent = new Vector2(end.x - offset, end.y);

            Handles.BeginGUI();
            {
                Handles.color = Color.white;
                Handles.DrawBezier(start, end, startTangent, endTangent, SelectedPin.Color, null, 2);
            }
            Handles.EndGUI();
        }

        Texture2D getTexture() {
            string s = EditorGUIUtility.isProSkin ? "workViewBackgroundDark" : "workViewBackgroundLight";
            return Resources.Load<Texture2D>("Textures/Editor/" + s);
        }

        private static string GetTitle() {
            return "Work View";
        }

        public override void ProcessEvents(Event e) {
            base.ProcessEvents(e);
            Vector2 mousePos = e.mousePosition;
            NodeBase node = InsideNode(mousePos);
            NodePin pin = null;
            if(node!=null) pin = node.InsidePin(mousePos);

            // pan the view
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
            {
                if (node != null) {
                    if (pin != null) {
                        if (SelectedPin != null) {
                            if (!SelectedPin.GetType().Equals(pin.GetType()) && pin.varType != SelectedPin.varType) {
                                if (ControlNode.castables.ContainsKey(SelectedPin.varType.ToString())) {
                                    if (ControlNode.castables[SelectedPin.varType.ToString()].Contains(pin.varType.ToString())) {
                                        Debug.Log("Casting tooltip");
                                        GUI.Box(new Rect(mousePos, new Vector2(100, 35)),
                                           ("Cast from " + Enum.GetName(typeof(PinType),SelectedPin.varType)
                                            + " to " + Enum.GetName(typeof(PinType), pin.varType)));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // right click a thing
            if (e.type == EventType.ContextClick) {
                GenericMenu menu = new GenericMenu();
                if (graph != null) {
                    if (node != null) {
                        if (pin != null) {
                            //Debug.Log("Right Clicked a PIN");
                            // pin connection menu
                            bool isOutput = pin.GetType().Equals(typeof(OutputPin));

                            //break all connections
                            if (pin.isConnected) {
                                menu.AddItem(new GUIContent("Break Connection"), false, NodeBase.RemoveConnection, pin);

                                //break individual Conections
                                //if (isOutput) {
                                //    foreach (InputPin n in pin.node.inPins) {
                                //        menu.AddItem(new GUIContent("Break Connection to " + pin.ConName()), false, node.RemovePin, "pin obj?");
                                //    }
                                //} else {
                                //    foreach (OutputPin n in pin.node.outPins) {
                                //        menu.AddItem(new GUIContent("Break Connection to " + pin.ConName()), false, node.RemovePin, "pin obj?");
                                //    }
                                //}

                                //add separator
                                menu.AddSeparator("");
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
                    } else if(window.nodeCreateView== null) {
                        // add node creation window
                        //Debug.Log("Right Clicked nowhere");
                        window.nodeCreateView = new NodeCreateView(mousePos);
                    } else {
                        //Debug.Log("NCV: " + new Rect(window.nodeCreateView.mouseLoc, window.nodeCreateView.size));
                    }
                }
            }

            // hit escape
            if (e.keyCode == KeyCode.Escape) {
                if (SelectedPin != null) SelectedPin = null;
            }

            // left click a pin
            if(e.button == 0 && e.type == EventType.MouseDown) {
                if (node != null) {
                    if (pin != null) {
                        if (SelectedPin != null) {
                            bool isInput = SelectedPin.GetType().Equals(typeof(InputPin));
                            // must connect inputs to outputs
                            if (!SelectedPin.GetType().Equals(pin.GetType())) {
                                if (pin.varType == SelectedPin.varType) {
                                    if (pin.node != SelectedPin.node) {
                                        SelectedPin.isConnected = true;
                                        pin.isConnected = true;
                                        if (isInput) {
                                            ((InputPin)SelectedPin).ConnectedOutput = ((OutputPin)pin);
                                            //((OutputPin)pin).ConnectedInputID = SelectedPin.node.inPins.IndexOf((InputPin)pin);
                                            ((OutputPin)pin).ConnectedInput = (InputPin)SelectedPin;
                                        } else {
                                            ((InputPin)pin).ConnectedOutput = ((OutputPin)SelectedPin);
                                            //((OutputPin)SelectedPin).ConnectedInputID = pin.node.inPins.IndexOf((InputPin)pin);
                                            ((OutputPin)SelectedPin).ConnectedInput = (InputPin)pin;
                                        }
                                    }
                                } else {
                                    // if vartype of selectedPin can be cast to clicked pin,
                                    if (ControlNode.castables.ContainsKey(SelectedPin.varType.ToString())) {
                                        if (ControlNode.castables[SelectedPin.varType.ToString()].Contains(pin.varType.ToString())) {
                                            //Debug.Log("sPin can be cast to Pin");
                                            // create a cast node
                                            ControlNode CN;
                                            if (isInput) {
                                                CN = (ControlNode)NodeUtilities.CreateNode(graph, pin.varType,
                                                    SelectedPin.varType, e.mousePosition);
                                                //CN.outPins[0].ConnectedInputID = SelectedPin.node.inPins.IndexOf((InputPin)SelectedPin);
                                                ((InputPin)pin).ConnectedOutput = CN.outPins[0];
                                                CN.inPins[0].ConnectedOutput = (OutputPin)pin;
                                                //((OutputPin)pin).ConnectedInputID = 0;
                                                ((OutputPin)pin).ConnectedInput = CN.inPins[0];
                                            } else {
                                                CN = (ControlNode)NodeUtilities.CreateNode(graph, SelectedPin.varType, 
                                                    pin.varType, e.mousePosition);
                                                //CN.outPins[0].ConnectedInputID = pin.node.inPins.IndexOf((InputPin)pin);
                                                CN.outPins[0].ConnectedInput = (InputPin)pin;
                                                ((InputPin)pin).ConnectedOutput = CN.outPins[0];
                                                CN.inPins[0].ConnectedOutput = (OutputPin)SelectedPin;
                                                //((OutputPin)SelectedPin).ConnectedInputID = 0;
                                                ((OutputPin)SelectedPin).ConnectedInput = CN.inPins[0];
                                            }
                                            CN.inPins[0].isConnected = true;
                                            CN.outPins[0].isConnected = true;
                                            SelectedPin.isConnected = true;
                                            pin.isConnected = true;
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
                    }
                }
            }

        }

        private NodeBase InsideNode(Vector2 mousePos) {
            if (graph == null) return null;
            foreach(NodeBase node in graph.nodes) {
                if (node == null) continue;
                if (node.Contains(mousePos)) return node;
            }
            return null;
        }
        #endregion

        public void CutNode(object o) {

        }

        public void CopyNode(object o) {

        }

        public void DupeNode(object o) {

        }
    }
}
