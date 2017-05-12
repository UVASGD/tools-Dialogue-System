using ScriptEditor.Graph;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScriptEditor.EditorScripts.Inspector {

    static class PropertiesInspector {
        public static bool defaultGUI = true;
    }

#region NodeInspect

[CustomEditor(typeof(FetchNode))]
    public class FetchNodeE : Editor {
        public override void OnInspectorGUI() {
            if (PropertiesInspector.defaultGUI) {
                base.OnInspectorGUI();
                return;
            }

            FetchNode node = (FetchNode)target;
            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
            object retVal=null;
            switch (node.VarType) {
                case VarType.Actor:
                    retVal = EditorGUILayout.ObjectField("Actor: ", (Actor)node.OutVal,
                        typeof(Actor), false);
                    break;
                //case ConnectType.Bool:
                //    EditorGUILayout.DoubleField("Value: ", (Actor)node.outVal,
                //        typeof(Actor), false);
                //    break;
                case VarType.Float:
                    retVal = EditorGUILayout.FloatField("Value: ", (float)node.OutVal);
                    break;
                case VarType.Integer:
                    retVal = EditorGUILayout.IntField("Value: ", (int)node.OutVal);
                    break;
                //case ConnectType.Object:
                //    retVal = EditorGUILayout.ObjectField("Value: ", node.outVal,
                //        node.outVal.GetType(), false);
                //    break;
                case VarType.Vector2:
                    retVal = EditorGUILayout.Vector2Field("Value: ", (Vector2)node.OutVal);
                    break;
                case VarType.Vector3:
                    retVal = EditorGUILayout.Vector2Field("Value: ", (Vector3)node.OutVal);
                    break;
                case VarType.Vector4:
                    retVal = EditorGUILayout.Vector2Field("Value: ", (Vector4)node.OutVal);
                    break;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value: ");
            EditorGUILayout.LabelField(retVal!=null? retVal.ToString():"null");
            EditorGUILayout.EndHorizontal();
        }
    }

    [CustomEditor(typeof(MathNode))]
    public class FunctionNodeE : Editor {
        public override void OnInspectorGUI() {
            if (PropertiesInspector.defaultGUI) {
                base.OnInspectorGUI();
                return;
            }

            MathNode node = (MathNode)target;
            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
        }
    }

    [CustomEditor(typeof(StartNode))]
    public class StartNodeE : Editor {
        public override void OnInspectorGUI() {
            if (PropertiesInspector.defaultGUI) {
                base.OnInspectorGUI();
                return;
            }

            StartNode node = (StartNode)target;
            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
        }
    }

    [CustomEditor(typeof(SubStartNode))]
    public class SubStartNodeE : Editor {
        public override void OnInspectorGUI() {
            if (PropertiesInspector.defaultGUI) {
                base.OnInspectorGUI();
                return;
            }

            SubStartNode node = (SubStartNode)target;
            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
        }
    }

    [CustomEditor(typeof(ControlNode))]
    public class ControlNodeE : Editor {
        public override void OnInspectorGUI() {
            if (PropertiesInspector.defaultGUI) {
                base.OnInspectorGUI();
                return;
            }

            ControlNode node = (ControlNode)target;
            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
        }
    }
#endregion

    [CustomEditor(typeof(NodeGraph))]
    public class NodeGraphE : Editor {
        public override void OnInspectorGUI() {
            NodeGraph graph = (NodeGraph)target;
            EditorGUILayout.LabelField(graph.Name, EditorStyles.boldLabel);
        }
    }
}
