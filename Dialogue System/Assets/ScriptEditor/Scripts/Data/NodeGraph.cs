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
        [HideInInspector]public NodeBase ConnectionNode;
        [HideInInspector]public bool wantsConnection;
        [HideInInspector]public bool showProperties;
        public string path { get { return AssetDatabase.GetAssetPath(this); } }

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

        }
#if UNITY_EDITOR
        public void DrawGraph(Event e, Rect viewRect) {
            if (nodes.Any()) {
                ProcessEvents(e, viewRect);
                
                foreach (NodeBase n in nodes) {
                    n.DrawNode(e, viewRect);
                }
            }
        }

        private void ProcessEvents(Event e, Rect viewRect) {

        }

        private void DrawConnectionToMouse(Vector2 pos) {

        }

        public void DeletNode(NodeBase node) {

        }

        private void DeselectAllNodes() {

        }
#endif

    }
}
