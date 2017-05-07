using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ScriptEditor.Graph {
    public class NodeGraph : ScriptableObject{
        public string Name = "New Script";
        public List<NodeBase> nodes;
        [HideInInspector]public NodeBase SelectedNode;
        //[HideInInspector]public NodeBase ConnectionNode;
        //[HideInInspector] public NodePin SelectedPin;
        [HideInInspector]public bool wantsConnection;
        [HideInInspector]public bool showProperties;
        public string Path { get { return AssetDatabase.GetAssetPath(this); } }

        private string ScriptPath;

        public void OnEnable() {
            if (nodes == null) {
            }
        }

        public void Initialize() {
            if (nodes.Any())
                foreach(var n in nodes) {
                    n.Initialize();
                }
        }

        public void UpdateGraph(Event e) {
            //if (nodes.Any()) {
            //    foreach (NodeBase node in nodes)
            //        node.UpdateNode(e);
            //}
        }

#if UNITY_EDITOR
        public void DrawGraph(Event e, Rect viewRect) {
            if (nodes.Any()) {
                ProcessEvents(e, viewRect);
                
                foreach (NodeBase n in nodes)
                    n.DrawConnections();
                foreach (NodeBase n in nodes) 
                    n.DrawNode(e, viewRect);

            }
        }

        private void ProcessEvents(Event e, Rect viewRect) {
            if (viewRect.Contains(e.mousePosition)) {
                if (e.button == 0) {
                    if (e.type == EventType.MouseDown) {
                        DeselectAllNodes();
                        bool setNode = false;
                        foreach(var node in nodes) {
                            if (node.Contains(e.mousePosition)) {
                                if (node.InsidePin(e.mousePosition)== null) {
                                    SelectedNode = node;
                                    node.isSelected = true;
                                    setNode = true;
                                    break;
                                }
                            }
                        }

                        if (!setNode) DeselectAllNodes();
                        else
                            BringToFront(SelectedNode);
                        if (wantsConnection) {

                        }
                    }
                }
            }
            if (e.keyCode == KeyCode.Delete && SelectedNode != null) {
                DeleteNode(SelectedNode);
                SelectedNode = null;
            }
        }

        public void DeleteNode(object n) {
            // remove node connections
            NodeBase node = (NodeBase)n;
            foreach (InputPin ip in node.inPins) {
                ip.ConnectedOutput.isConnected = false;
                ip.ConnectedOutput = null;
            } foreach(OutputPin op in node.outPins) {
                op.ConnectedInput.isConnected = false;
                op.ConnectedInput = null;
            }

            nodes.Remove((NodeBase)node);

        }

        /// <summary>
        /// Brings the selected node to the front in draw order
        /// </summary>
        /// <param name="node"></param>
        private void BringToFront(NodeBase node) {
            nodes.Remove(node);
            nodes.Add(node);
        }

        private void DeselectAllNodes() {
            foreach(var node in nodes) {
                node.isSelected = false;
            }
            SelectedNode = null;
        }
#endif

    }
}
