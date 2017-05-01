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
        Vector2 pan;

        public WorkView() : base(getTitle()) {

            backgroundTexture = getTexture();
            if(backgroundTexture!=null)
                backgroundTexture.wrapMode = TextureWrapMode.Repeat;
        }

        private void ProcessContextMenu(Event e) {
        }

        // TODO
        private void NewNode(object obj) {

        }

        private void ContextCallback(object obj) {

        }

        #region Override
        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);
            ProcessEvents(e);
            if (graph != null) {
                title = graph.name;
            } else {
                title = getTitle();
            }


            GUI.DrawTextureWithTexCoords(body, backgroundTexture, new Rect(0, 0, body.width / backgroundTexture.width,
                body.height / backgroundTexture.height));
            GUI.Box(body, title, skin.GetStyle("WorkViewBackground"));
            if (graph!=null) graph.DrawGraph(e, body);

        }

        Texture2D getTexture() {
            string s = EditorGUIUtility.isProSkin ? "workViewBackgroundDark" : "workViewBackgroundLight";
            return Resources.Load<Texture2D>("Textures/Editor/" + s);
        }

        private static string getTitle() {
            return "Work View";
        }

        public override void ProcessEvents(Event e) {
            base.ProcessEvents(e);

            //if (e.button == 1 && e.type == EventType.MouseDrag) {
            //    panX += Event.current.delta.x;
            //    panY += Event.current.delta.y;
            //}

            if (e.type == EventType.ScrollWheel) {
                 window.zoomScale = StaticMethods.Clamp(window.zoomScale + e.delta.y, 1.0f / 25.0f, 2.0f);
            }

            if (e.type == EventType.ContextClick) {
                Vector2 mousePos = e.mousePosition;
                GenericMenu menu = new GenericMenu();
                if (graph != null) {
                    //Debug.Log("HasNodeS: \"" + graph.nodes + "'");
                    NodeBase node = insideNode(mousePos);
                    if (node != null) {
                        Debug.Log("Right Clicked a node");
                        //test if connected to pin
                        NodePin pin = node.InsidePin(mousePos);
                        if (pin != null) {
                            //node connection menu
                            bool isOutput = pin.GetType().Equals(typeof(OutputPin));

                            //break all connections
                            //menu.AddItem(new GUIContent("Break All Connections"), false, NodeBase.RemoveAllPins, pin);

                            //break individualConections
                            if (isOutput && pin.isConnected) {
                                //foreach (InputPin n in pin.node.inPins) {
                                    menu.AddItem(new GUIContent("Break Connection to " + pin.ConName()), false, node.RemovePin, "pin obj?");
                                //}
                            } else {
                                //foreach (OutputPin n in pin.node.outPins) {
                                    menu.AddItem(new GUIContent("Break Connection to " + pin.ConName()), false, node.RemovePin, "pin obj?");
                                //}

                            }

                            //add separator
                            menu.AddSeparator("");
                        }

                        // cut, copy, duplicate, delete
                        if (node.GetType().Equals(typeof(StartNode))) {
                            menu.AddItem(new GUIContent("Cut Node"), false, CutNode, node);
                            menu.AddItem(new GUIContent("Copy Node"), false, CopyNode, node);
                            menu.AddItem(new GUIContent("Duplicate Node"), false, DupeNode, node);
                            menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode, node);
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
        }

        private NodeBase insideNode(Vector2 mousePos) {
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

        public void DeleteNode(object o) {

        }
    }
}
