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
                    
                    CreateNode(graph, NodeType.Event, false, new Vector2(sc.workView.center.x - 170 / 2f,
                        sc.workView.center.y - 90 / 2f));
                    CreateNode(graph, NodeType.Event, true, new Vector2(sc.workView.center.x + 170 / 2f,
                        sc.workView.center.y - 90 / 2f));
                    sc.graph = graph;
                }
            }
        }

        /// <summary> Unimplemented. Create graph from file. </summary>
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

        //public static void DeleteNode(NodeBase node) {
        //    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(node));
        //    //AssetDatabase.SaveAssets();
        //    //AssetDatabase.Refresh();
        //}

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
                switch (type) {
                    case NodeType.Control:
                        switch ((ControlNode.ControlType)var) {
                            case ControlNode.ControlType.Branch: res = ScriptableObject.CreateInstance<BranchNode>(); break;
                            case ControlNode.ControlType.Choice: res = ScriptableObject.CreateInstance<ChoiceNode>(); break;
                            case ControlNode.ControlType.Custom: res = ScriptableObject.CreateInstance<CustomNode>(); break;
                            case ControlNode.ControlType.Delay: res = ScriptableObject.CreateInstance<DelayNode>(); break;
                            case ControlNode.ControlType.Dialogue: res = ScriptableObject.CreateInstance<DialogueNode>(); break;
                            case ControlNode.ControlType.ForLoop: res = ScriptableObject.CreateInstance<ForLoopNode>(); break;
                            case ControlNode.ControlType.Music: res = ScriptableObject.CreateInstance<MusicNode>(); break;
                            case ControlNode.ControlType.PlaySound: res = ScriptableObject.CreateInstance<PlaySoundNode>(); break;
                            case ControlNode.ControlType.Print: res = ScriptableObject.CreateInstance<PrintNode>(); break;
                            case ControlNode.ControlType.Quest: res = ScriptableObject.CreateInstance<QuestNode>(); break;
                            case ControlNode.ControlType.Sequence: res = ScriptableObject.CreateInstance<SequenceNode>(); break;
                            case ControlNode.ControlType.SetDialogueScript: res = ScriptableObject.CreateInstance<SetDialogueScriptNode>(); break;
                            case ControlNode.ControlType.SetSubStart: res = ScriptableObject.CreateInstance<SetSubStartNode>(); break;
                        }

                        res.Initialize();
                        ((ControlNode)res).Construct();
                        break;
                    case NodeType.Event:
                        int i = 1;
                        if (var as string != null) {
                            res = ScriptableObject.CreateInstance<SubStartNode>();
                            i = 2;
                        } else {
                            if ((bool)var) {
                                res = ScriptableObject.CreateInstance<EndNode>();
                                i = 3;
                            } else res = ScriptableObject.CreateInstance<StartNode>();
                        }
                        res.Initialize();
                        switch(i){
                            case 1: ((StartNode)res).Construct(); break;
                            case 2: ((SubStartNode)res).Construct(); break;
                            case 3: ((EndNode)res).Construct(); break;
                        }
                        break;
                }
                
                InitNode(res, graph, pos);
            }

            return res;
        }

        /// <summary>
        /// creates a variable-container node
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="var"></param>
        /// <param name="isInput"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static VarNode CreateNode(NodeGraph graph, Variable var, bool isInput, Vector2 pos) {
            VarNode res = ScriptableObject.CreateInstance<VarNode>();
            res.Initialize();
            res.Construct(var, isInput);
            InitNode(res, graph, pos);

            return res;
        }

        /// <summary>
        /// creates a new node with a specified variable type
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="var"></param>
        /// <param name="pos"></param>
        /// <param name="inType"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static NodeBase CreateNode(NodeGraph graph, NodeType type, object var, VarType inType, Vector2 pos, int count) {
            NodeBase res = null;
            if (graph != null) {
                switch (type) {
                    case NodeType.Math:
                        res = ScriptableObject.CreateInstance<MathNode>();
                        res.Initialize();
                        ((MathNode)res).Construct((MathNode.OpType)var, inType, count);
                        break;
                    case NodeType.Function:
                        res = ScriptableObject.CreateInstance<FunctionNode>();
                        res.Initialize();
                        ((FunctionNode)res).Construct((FunctionNode.FunctionType)var, inType);
                        break;
                }
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
        public static NodeBase CreateNode(NodeGraph graph, VarType input, VarType output, Vector2 pos) {
            CastNode res = null;

            if (graph != null) {
                res = ScriptableObject.CreateInstance<CastNode>();
                res.Initialize();
                res.Construct(input, output);
                InitNode(res, graph, pos);
            }
            return res;
        }

        /// <summary>
        /// initializes common attributes of the node ands saves it to asset database
        /// </summary>
        /// <param name="res"></param>
        /// <param name="graph"></param>
        /// <param name="pos"></param>
        private static void InitNode(NodeBase res, NodeGraph graph, Vector2 pos) {
            res.Resize();
            res.SetPos(pos);
            res.parentGraph = graph;
            graph.AddNode(res);
            AssetDatabase.AddObjectToAsset(res, graph);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

#endif

        public static void LoadScript(string graphName, string path) {

        }

        
    }
}
