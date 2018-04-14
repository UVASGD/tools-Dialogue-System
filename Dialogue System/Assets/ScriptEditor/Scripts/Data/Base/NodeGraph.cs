using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ScriptEditor.Graph {
    public class NodeGraph : ScriptableObject, ISerializationCallbackReceiver {
        public string Name = "New Script";
        public List<NodeBase> nodes;
        public List<StartNode> starts;
        /// <summary> the graph must becompiled before the game is run. 
        /// variable is invalidated whenever unsaved changes are made or compile returns error</summary>
        public bool compiled = true;
        [HideInInspector]public NodeBase SelectedNode;
        [HideInInspector]public bool wantsConnection;
        [HideInInspector]public bool showProperties;

        public string Path { get { return AssetDatabase.GetAssetPath(this); } }
        public bool ContainsVariable (string varName) { return localVariables.getVariable(varName) != null; }
        
        private VariableDictionary localVariables;
        private int subStartIndex = 0;
        private bool errFound = false;

        public bool hasErrors { get { return errFound; } }
        public StartNode CurrentSubStart{
            get { return starts[subStartIndex]; }
        }

        public void SetSubStartIndex(int index){
            subStartIndex = index;
        }

        public void Initialize() {
            localVariables = new VariableDictionary();
            starts = new List<StartNode>();
            if (nodes.Any())
                foreach(var n in nodes) {
                    n.Initialize();
                }
        }

        public void AddNode(NodeBase n) {
            nodes.Add(n);
            ResetCompiledStatus();
            if (n is StartNode) {
                starts.Add(n as StartNode);
            }
        }

        public void ConnectPins(InputPin ip, OutputPin op) {
            ResetCompiledStatus();

            // limit pin connections per type
            // executions can only have one output, but multiple inputs
            if (op.IsConnected && op.varType==VarType.Exec) {
               op.ConnectedInput.ConnectedOutput = null;
            }

            if (ip.IsConnected && ip.varType != VarType.Exec) {
                ip.ConnectedOutput.ConnectedInput = null;
            }

            ip.ConnectedOutput = op;
            //op.ConnectedInputID = ip.node.inPins.IndexOf(ip);
            op.ConnectedInput = ip;
        }

        // these functions are necessary (but not used) since not all pins can be serialized
        // TODO: indices are not immutable, so this string may not be valid
        public InputPin InputFromID(string ID) {
            if (String.IsNullOrEmpty(ID)) return null;
            string [] dat = ID.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            return nodes[int.Parse(dat[0])].InPins.ToList()[int.Parse(dat[1])];
        }

        public string IDFromInput(InputPin ip) {
            if(ip == null) return null;
            return nodes.IndexOf(ip.parentNode)+":"+ip.parentNode.InPins.ToList().IndexOf(ip);
        }

        public OutputPin OutputFromID(string ID) {
            if (String.IsNullOrEmpty(ID)) return null;
            string[] dat = ID.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            //Debug.LogWarning("PDOP_ID: " + ID/*+"\nN: "+nodes[x]+"\nOP: " +(nodes[x]!=null?nodes[x].OutPins.ToString():"FAILURE")*/);
            int x = int.Parse(dat[0]), y = int.Parse(dat[1]);

            return nodes[x].OutPins.ToList()[y];
        }

        public string IDFromOutput(OutputPin op) {
            if (op == null) return null;
            return nodes.IndexOf(op.parentNode) + ":" + op.parentNode.OutPins.ToList().IndexOf(op);
        }



        public Stack<NodeBase> lookupStack, compileStack;
        public bool foundEnd;
        public void Compile() {
            compileStack = new Stack<NodeBase>();
            lookupStack = new Stack<NodeBase>();
            // clear errors from previous compile
            foreach (NodeBase node in nodes) {
                node.errors = new List<NodeError>();
                //node.errors.Add(new NodeError(NodeError.ErrorType.NotConnected));
                node.compiled = false;
            }

            // start compile each execution path
            foreach(NodeBase node in nodes) {
                StartNode start = node as StartNode;
                if (start != null) {
                    foundEnd = false;
                    start.Compile();
                    if (!foundEnd)
                        start.errors.Add(new NodeError(NodeError.ErrorType.NoEnd));
                }
            }

            // check if compiling node found any errors
            foreach(NodeBase node in nodes)
                if (node.errors.Any()) {
                    //if (node.errors.Count == 1 && node.errors[0] ==
                    //    new NodeError(NodeError.ErrorType.NotConnected))
                    //    continue; // ignore isolated nodes 
                    errFound = true;
                    break;
                }

            if(!errFound) {
                compiled = true;
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        public void OnEnable()
        {
            Debug.Log("NodeGraph.OnEnable");
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            Debug.Log("NodeGraph.OnAfterDeserialize: nodes: " + Misc.ListToStringIDs(nodes) + ", starts: " + Misc.ListToStringIDs(starts));
            foreach (NodeBase node in nodes)
                foreach (NodePin pin in node.AllPins)
                    pin.OnAfterGraphDeserialize();
        }

#if UNITY_EDITOR
        /// <summary>
        /// clear all errors from recent compilation
        /// </summary>
        public void ResetCompiledStatus() {
            if (!compiled) return;
            compiled = false;
            //foreach (NodeBase n in nodes)
            //    n.errors.Clear();
        }

        private static float errMargin = 20f;
        public void DrawGraph(Event e, Rect viewRect) {
            if (nodes.Any()) {
                ProcessEvents(e, viewRect);

                Texture2D texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, Color.Lerp(Color.red, Color.black, .25f));
                texture.Apply();
                GUI.skin.box.normal.background = texture;
                GUI.skin.box.normal.textColor = Color.white;
                foreach (NodeBase n in nodes)
                    n.DrawConnections();
                foreach (NodeBase n in nodes)
                    n.DrawNode(e, viewRect);
                foreach (NodeBase n in nodes) {
                    Rect b = n.GetBody();
                    Vector2 start = b.position;
                    start.y += b.height;
                    int i = 0;
                    foreach (NodeError err in n.errors) {
                        string s = "#ERR " + err.Title;
                        Vector2 size = GUI.skin.box.CalcSize(new GUIContent(s));
                        GUI.Box(new Rect(start.x, start.y+errMargin*i-size.y/2f, 
                            size.x,
                            errMargin), s);
                        i++;
                    }
                }
            }
        }

        public void CreateVariable(Variable variable) {
            localVariables.addVariable(variable);
        }

        private bool shift = false;
        /// <summary> handle user input </summary>
        private void ProcessEvents(Event e, Rect viewRect) {

            // hold shift
            if (e.keyCode == KeyCode.LeftShift || e.keyCode == KeyCode.RightShift) {
                shift = (e.type == EventType.KeyDown);
            }

            if (viewRect.Contains(e.mousePosition)) {
                if (e.button == 0) {
                    if (e.type == EventType.MouseDown) {
                        bool setNode = false;
                        if (!shift) {
                            DeselectAllNodes();
                            foreach (var node in nodes) {
                                if (node.Contains(e.mousePosition)) {
                                    if (node.InsidePin(e.mousePosition) == null) {
                                        Selection.activeObject = node;
                                        SelectedNode = node;
                                        node.isSelected = true;
                                        setNode = true;
                                        break;
                                    }
                                }
                            }
                        } else {
                            Debug.Log("Pls ckae");
                            foreach (var node in nodes)
                                if (node.Contains(e.mousePosition)) {
                                    List<UnityEngine.Object> objs = new List<UnityEngine.Object>(Selection.objects);
                                    objs.Add(node);
                                    node.isSelected = true;
                                    Selection.objects = objs.ToArray();
                                }
                        }

                        if (!setNode) DeselectAllNodes();
                        else BringToFront(SelectedNode);
                    }
                }
            }
        }

        /// <summary>
        /// checks if the mouse position is inside any node
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        public NodeBase InsideNode(Vector2 mousePos) {
            foreach (NodeBase node in nodes) {
                if (node == null) continue;
                if (node.Contains(mousePos)) return node;
            }
            return null;
        }

        public void DeleteNode(object n) {
            // remove node connections
            NodeBase node = (NodeBase)n;
            if (node as StartNode != null || node as EndNode != null)
                return;

            foreach (NodePin pin in node.AllPins)
                NodeBase.RemoveConnection(pin);
            nodes.Remove(node);
            DestroyImmediate(node, true);
            ResetCompiledStatus();
        }

        /// <summary>  Brings the selected node to the front in draw order </summary>
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
            Selection.activeObject = null;
        }
#endif

    }
}
