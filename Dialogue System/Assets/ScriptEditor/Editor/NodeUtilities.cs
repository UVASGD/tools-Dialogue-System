using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using ScriptEditor.Graph;
using System.Collections.Generic;

namespace ScriptEditor.EditorScripts {
    public static class NodeUtilities {
#if UNITY_EDITOR

        /// <summary>
        /// Create Node Graph and save to assets
        /// </summary>
        /// <param name="graphName">name of graph</param>
        /// <param name="path">location graph will be saved</param>
        public static void CreateNodeGraph(string graphName, string path) {
            NodeGraph graph = ScriptableObject.CreateInstance<NodeGraph>();
            if (graph != null) {
                graph.Name = graphName;
                graph.nodes = new List<NodeBase>();
                graph.Initialize();

                AssetDatabase.CreateAsset(graph, path + graphName+".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                // graph mst conatain a start node

                ScriptEditorWindow sc = EditorWindow.GetWindow<ScriptEditorWindow>();
                if (sc != null) {
                    CreateNode(graph, NodeType.Event, false, new Vector2(sc.workView.center.x-170/4f, 
                        sc.workView.center.y-90/2f));
                    sc.graph = graph;
                }
            }
        }

        /// <summary> Unimpleented. Create graph from file. </summary>
        public static void LoadScript(string path) {
            NodeGraph graph;

            if (String.IsNullOrEmpty(path)) return;

            int appPatLen = Application.dataPath.Length;
            string finalPath = path.Substring(appPatLen - 6);
            graph = AssetDatabase.LoadAssetAtPath<NodeGraph>(finalPath);
            if(graph != null) {
                ScriptEditorWindow window = EditorWindow.GetWindow<ScriptEditorWindow>();
                if (window != null) {
                    window.graph = graph;
                }
            }
        }

        /// <summary> Delete passed graph on file </summary>
        /// <param name="graph"></param>
        public static void DeleteScript(NodeGraph graph) {
            if (EditorUtility.DisplayDialog("Delete Script", "Are you sure you want to delete the current script?", "Yes", "No")) {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(graph));
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// creates a new generic node in the passed graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="type"></param>
        /// <param name="var"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static NodeBase CreateNode(NodeGraph graph, NodeType type, object var, Vector2 pos) {
            NodeBase res = null;
            if (graph != null) {
                //Debug.Log("var: " + var);
                switch (type) {
                    case NodeType.Control:
                        res = ScriptableObject.CreateInstance<ControlNode>();
                        res.Initialize();
                        ((ControlNode)res).Construct((ControlNode.ControlType) var);
                        break;
                    case NodeType.Event:
                        if ((bool)var) res = ScriptableObject.CreateInstance <SubStartNode>();
                        else res = ScriptableObject.CreateInstance <StartNode>();
                        res.Initialize();
                        ((StartNode)res).Construct();
                        break;
                    case NodeType.Function:
                        break;
                }

                InitNode(res, graph, pos);
            }

            return res;
        }

        /// <summary>
        /// creates a new mathematical operator node
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="type"></param>
        /// <param name="pos"></param>
        /// <param name="inType"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static NodeBase CreateNode(NodeGraph graph, MathNode.OpType type, Vector2 pos, PinType inType, int count) {
            MathNode res = null;
            if (graph != null) {
                res = ScriptableObject.CreateInstance<MathNode>();
                res.Initialize();
                res.Construct(type, inType, count);
                InitNode(res, graph, pos);
            }

            return res;
        }

        /// <summary>
        /// create a new Casting node
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static NodeBase CreateNode(NodeGraph graph, PinType input, PinType output, Vector2 pos) {
            ControlNode res = null;

            if (graph != null) {
                res = ScriptableObject.CreateInstance<ControlNode>();
                res.Initialize();
                res.Construct(input, output);
                InitNode(res, graph, pos);
            }
            return res;
        }

        /// <summary>
        /// initializes certain things about the node ands saves it to asset database
        /// </summary>
        /// <param name="res"></param>
        /// <param name="graph"></param>
        /// <param name="pos"></param>
        private static void InitNode(NodeBase res, NodeGraph graph, Vector2 pos) {
            res.SetPos(pos);
            res.parentGraph = graph;
            graph.nodes.Add(res);
            AssetDatabase.AddObjectToAsset(res, graph);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

#endif

        public static void LoadScript(string graphName, string path) {

        }

        
    }
}
