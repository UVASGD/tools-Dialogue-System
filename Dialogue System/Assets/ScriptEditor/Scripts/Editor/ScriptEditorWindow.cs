using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using ScriptEditor.Graph;
using System.IO;
using ScriptEditor.EditorScripts.Inspector;

namespace ScriptEditor.EditorScripts {
    public class ScriptEditorWindow : EditorWindow {

        public static ScriptEditorWindow instance;
        public WorkView workView;
        public HeaderView headerView;
        public StatusView statusView;
        public NodeCreateView nodeCreateView;
        public VariableCreateView varCreateView;
        public NodeToolTipView toolTipView;
        public NodeGraph graph = null;

        const float VIEW_HORIZONTAL_PERCENTAGE = 0.75f;
        const float VIEW_VERTICAL_PERCENTAGE = 0.75f;

        [MenuItem("Dope Dialogue/Script Editor")]
        private static void OpenScriptEditor() {
            ScriptEditorWindow winso = (ScriptEditorWindow)EditorWindow.GetWindow(typeof(ScriptEditorWindow));
            PropertiesInspector.window = winso;
            Initialize();
        }

        public static void Initialize() {
            instance = GetWindow<ScriptEditorWindow>();
            instance.titleContent = new GUIContent("Script Editor");
        }
        Vector2 pan;

        void OnEnable() {
        }


        #region GUI
        public void Update() {
            Event e = Event.current;
        }

        //Vector3 vanishingPoint = new Vector2(0, 19.12f); public float zoomScale = 1;
        
        void OnGUI() {
            if (workView == null || headerView == null || statusView == null) {
                CreateViews();
                return;
            }

            Event e = Event.current;
            //Debug.Log(e);
            Rect headerBox = new Rect(0, 0, position.width, 17.5f);Matrix4x4 oldMatrix = GUI.matrix;

            //Scale  gui matrix
            //Matrix4x4 Translation = Matrix4x4.TRS(vanishingPoint, Quaternion.identity, Vector3.one);
            //Matrix4x4 Scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
            //GUI.matrix = Translation * Scale * Translation.inverse;
            workView.DrawView(new Rect(0, headerBox.height, position.width, position.height - 2 * headerBox.height),
                            new Rect(/*zoomScale, zoomScale,  1/zoomScale, 1/zoomScale*/ 1, 1, 1, 1),
                            e, graph);
            //reset the matrix
            //GUI.matrix = oldMatrix;

            // Just for testing (unscaled controls at the bottom)
            //GUILayout.FlexibleSpace();
            //vanishingPoint = EditorGUILayout.Vector2Field("vanishing point", vanishingPoint);

            //***Not highest priority
            headerView.DrawView(headerBox,
                            new Rect(0, 0, 1, 1),
                            e, graph);

            //***Not highest priority
            statusView.DrawView(new Rect(0, position.height - headerBox.height, position.width, headerBox.height),
                            new Rect(0, 1, 1, 1),
                            e, graph);
            if (nodeCreateView != null)
                nodeCreateView.DrawView(new Rect(nodeCreateView.mouseLoc, nodeCreateView.size),
                    new Rect(1, 1, 1, 1), e, graph);
            if (varCreateView != null)
                varCreateView.DrawView(new Rect(varCreateView.mouseLoc, VariableCreateView.DefaultSize),
                    new Rect(1,1,1,1), e, graph);
            if (toolTipView != null)
                toolTipView.DrawView(new Rect(e.mousePosition+new Vector2(20,20), toolTipView.size),
                    new Rect(1,1,1,1), e, graph);

            ProcessEvents(e);
            if (workView.SelectedPin != null || nodeCreateView!=null ||
                 (e.type == EventType.MouseDrag && e.button == 2)) Repaint();
        }

        static void CreateViews() {
            if (instance != null) {
                instance.workView = new WorkView();
                instance.headerView = new HeaderView();
                instance.statusView = new StatusView();
            } else {
                instance = GetWindow<ScriptEditorWindow>();
                instance.titleContent = new GUIContent("Script Editor");
                instance.workView = new WorkView();
                instance.headerView = new HeaderView();
                instance.statusView = new StatusView();
            }
        }

        void DrawNodeWindow(int id) {
            GUI.DragWindow();
        }
        #endregion

        void ProcessEvents(Event e) {

        }

        #region data
        void OnSelectionChange() {
            if (Selection.activeObject != null) {
                if (Selection.activeObject is NodeGraph) {
                    graph = (NodeGraph)Selection.activeObject;
                    Repaint();
                }
            }
            
        }

        //[UnityEditor.Callbacks.OnOpenAsset(1)]
        //public static bool OnOpenAsset(int instanceID, int line) {
        //    if (Selection.activeObject as NodeGraph != null) {
        //        OpenScriptEditor(Selection.activeObject as NodeGraph);
        //        return true; //catch open file
        //    }

        //    return false; // let unity open the file
        //}

        #endregion

        
    }

    /// <summary> Small popup window that allows the user to choose the variable type of the node
    /// they wish to create </summary>
    public class NodeCreatePopup : EditorWindow {
        public Vector2 mouseLoc;

        private static NodeCreatePopup Instance;
        private NodeType nodeType;
        private object subType;
        int nodeCount;
        private static VarType inType, outType;
        private Vector2 selectedIndex;
        private NodePin pinToAttach;

        private NodeGraph GraphObj {
            get {
                ScriptEditorWindow window = GetWindow<ScriptEditorWindow>();
                if (window != null) return window.graph;
                return null;
            }
        }
        List<string> posInp;


        private static void SetNCP() {
            ScriptEditorWindow window = GetWindow<ScriptEditorWindow>();
            if (window != null) window.workView.NCPopup = Instance;
        }

        private static void RemoveNCP() {
            ScriptEditorWindow window = GetWindow<ScriptEditorWindow>();
            if (window != null) {
                window.workView.NCPopup = null;
                window.workView.SelectedPin = null;
            }
        }

        public static void Init(NodeType nT, object sT, NodePin pin, Vector2 pos, int count=2) {
            Instance = GetWindow<NodeCreatePopup>(true);
            Instance.maxSize = new Vector2(200, 100);
            Instance.minSize = Instance.maxSize;
            Instance.nodeType = nT;
            Instance.subType = sT;
            Instance.mouseLoc = pos;
            Instance.pinToAttach = pin;
            Instance.posInp = new List<string>();
            Instance.nodeCount = count;
            SetNCP();

            switch (nT) {
                case NodeType.Function:
                    FunctionNode.FunctionType f = (FunctionNode.FunctionType)sT;
                    Instance.titleContent = new GUIContent("Create " + (f).ToString() + " Node");
                    Instance.posInp = FunctionNode.validCombos[f];
                    break;
                case NodeType.Math:
                    MathNode.OpType t = (MathNode.OpType)sT;
                    Instance.titleContent = new GUIContent("Create "+ (t).ToString() + " Node");
                    Instance.posInp = MathNode.validCombos[t];
                    Instance.nodeCount = 2;
                    break;
                case NodeType.Control:
                    //Instance.titleContent = new GUIContent("Create " + ((ControlNode.ControlType)sT).ToString() + " Node");
                    Instance.titleContent = new GUIContent("Create Cast Node");
                    Instance.posInp = new List<string>(ControlNode.castables.Keys);
                    break;
                case NodeType.Fetch:
                    Instance.titleContent = new GUIContent("Fetch Variable");
                    // input from list of variables in database
                    break;
            }

            if(Instance.nodeCount<0 || Instance.posInp.Count == 1) {
                Instance.CreateNode();
            }
        }

        public void OnGUI() {
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(20);
                switch (nodeType) {
                    case NodeType.Math:
                    case NodeType.Fetch:
                    case NodeType.Function:
                        EditorGUILayout.LabelField("Type: ", EditorStyles.boldLabel, GUILayout.Width(80));
                        selectedIndex.x = EditorGUILayout.Popup((int)selectedIndex.x, posInp.ToArray(), GUILayout.Width(80));

                        if (nodeCount > 0) {
                            GUILayout.EndHorizontal();
                            GUILayout.Space(6);
                            GUILayout.BeginHorizontal();
                            GUILayout.Space(20);
                            EditorGUILayout.LabelField("Num Pins:", EditorStyles.boldLabel, GUILayout.Width(80));
                            nodeCount = StaticMethods.Clamp(EditorGUILayout.IntField(nodeCount, GUILayout.Width(80)), 2, 16);
                        }
                        break;
                    case NodeType.Control:
                        EditorGUILayout.LabelField("From: ", EditorStyles.boldLabel/*, GUILayout.Width(80)*/);
                        selectedIndex.x = EditorGUILayout.Popup((int)selectedIndex.x, posInp.ToArray());
                        EditorGUILayout.LabelField("To: ", EditorStyles.boldLabel/*, GUILayout.Width(80)*/);
                        selectedIndex.y = EditorGUILayout.Popup((int)selectedIndex.y, 
                            ControlNode.castables[posInp[(int)selectedIndex.x]].ToArray());
                        break;
                }
                GUILayout.Space(20);

            }
            GUILayout.EndHorizontal();
            GUILayout.Space(6);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(20);
                if (GUILayout.Button("Create")) {
                    CreateNode();
                }
                GUILayout.Space(10);
                if (GUILayout.Button("Cancel")) {
                    Instance.Close();
                }
                GUILayout.Space(20);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            Repaint();

        }

        public new void Close() {
            base.Close();
            RemoveNCP();
        }

        /// <summary>
        /// Create the node using NodeUtilities
        /// </summary>
        public void CreateNode() {
            NodeBase node = null;
            switch (nodeType) {
                case NodeType.Math:
                    VarType pT = (VarType)Enum.Parse(typeof(VarType), posInp[(int)selectedIndex.x]);
                    //Debug.Log(GraphObj + "," + subType + ", " + pT);
                    node = NodeUtilities.CreateNode(GraphObj, NodeType.Math, 
                        subType, pT, mouseLoc, nodeCount);
                    break;
                case NodeType.Fetch:

                    break;
                case NodeType.Control:
                    if ((ControlNode.ControlType)subType != ControlNode.ControlType.Cast)
                        node = NodeUtilities.CreateNode(GraphObj, nodeType, subType, mouseLoc);
                    else node = NodeUtilities.CreateNode(GraphObj,
                        (VarType)Enum.Parse(typeof(VarType), posInp[(int)selectedIndex.x]),
                        (VarType)Enum.Parse(typeof(VarType), ControlNode.castables[posInp[(int)selectedIndex.x]][(int)selectedIndex.y]),
                        mouseLoc);
                    break;
            }
            
            // establish connection between selected pin and first input/output of matching type
            // e.g. float to float
            if (pinToAttach != null) {
                if (!pinToAttach.isInput) {
                    foreach (InputPin ip in node.InPins)
                        if (ip.varType == pinToAttach.varType) {
                            GraphObj.ConnectPins(ip, (OutputPin)pinToAttach);
                            break;
                        }
                } else {
                    foreach (OutputPin op in node.OutPins)
                        if (op.varType == pinToAttach.varType) {
                            GraphObj.ConnectPins((InputPin)pinToAttach, op);
                            break;
                        }
                }
            }
            Instance.Close();
        }
    }

    /// <summary>
    /// small popup window that allows the user to choose where to save the new script
    /// </summary>
    public class GraphCreatePopup :EditorWindow {
        private static GraphCreatePopup Instance;
        const string DEFAULT_NAME = "New Script";
        private string Path;

        void OnEnable() {
            string src = Application.dataPath + "/ScriptEditor/Resources/Database/";
            Path = (Directory.Exists(src) ? src : Application.dataPath+"/") + DEFAULT_NAME;
        }


        public static void Init() {
            Instance = GetWindow<GraphCreatePopup>(true);
            Instance.titleContent = new GUIContent("Script Name");
            Instance.maxSize = new Vector2(450, 80);
            Instance.minSize = Instance.maxSize;
        }

        public void OnGUI() {
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(20);
                EditorGUILayout.LabelField("New Script", EditorStyles.boldLabel, GUILayout.Width(80));
                Path = EditorGUILayout.TextField(Path);
                if (!Path.EndsWith(".asset")) Path += ".asset";
                if (GUILayout.Button("...", GUILayout.Width(35))) {
                    Path = EditorUtility.SaveFilePanel("Create New Script", DirFrom(Path), DEFAULT_NAME, "asset");
                }
                GUILayout.Space(20);

            } GUILayout.EndHorizontal();
            GUILayout.Space(6);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(20);
                if (GUILayout.Button("Create")) {
                    if(!String.IsNullOrEmpty(Path)) {
                        if (Path.Contains(Application.dataPath)) {
                            NewScript(Selection.activeGameObject);
                            Instance.Close();
                        } else {
                            EditorUtility.DisplayDialog("Invalid Path", "Script must be located in Assets folder!", "OK");
                        }
                    } else {
                        EditorUtility.DisplayDialog("Invalid Path", "Enter a valid path for script.", "OK");
                    }
                }
                GUILayout.Space(10);
                if (GUILayout.Button("Cancel")) {
                    Instance.Close();
                }
                GUILayout.Space(20);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            Repaint();

        }
        
        private void NewScript(GameObject obj) {
            string graphName;
            string key = Path.Contains("/") ? "/" : "\\";
            int kI = Path.Contains(key) ? Path.LastIndexOf(key) + 1 : 0, aI = Path.IndexOf(".asset");
            if (aI > 0) {
                graphName = Path.Substring(kI, aI - kI);
                Path = Path.Replace(graphName, "").Replace(".asset", "");
                NodeUtilities.CreateNodeGraph(graphName, Path.Substring(Path.IndexOf("Assets/")));

                if (obj != null) {
                    // attach script to object??
                }
            }
        }

        private string DirFrom(string s) {
            string graphName;
            string key = s.Contains("/") ? "/" : "\\";
            int kI = s.Contains(key) ? s.LastIndexOf(key) + 1 : 0, aI = s.IndexOf(".asset");
            if (aI > 0) {
                graphName = s.Substring(kI, aI - kI);
                return s.Replace(graphName, "").Replace(".asset", "");

            } else return null;
        }
    }
}
