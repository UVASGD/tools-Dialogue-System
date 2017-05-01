using ScriptEditor.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ScriptEditor.EditorScripts {

#region NodeInspect

    [CustomEditor(typeof(FetchNode))]
    public class FetchNodeE : Editor {
        void OnGUI() {
            FetchNode node = (FetchNode)target;
            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
            object retVal;
            switch (node.VarType) {
                case PinType.Actor:
                    retVal = EditorGUILayout.ObjectField("Actor: ", (Actor)node.OutVal,
                        typeof(Actor), false);
                    break;
                //case ConnectType.Bool:
                //    EditorGUILayout.DoubleField("Value: ", (Actor)node.outVal,
                //        typeof(Actor), false);
                //    break;
                case PinType.Float:
                    retVal = EditorGUILayout.FloatField("Value: ", (float)node.OutVal);
                    break;
                case PinType.Integer:
                    retVal = EditorGUILayout.IntField("Value: ", (int)node.OutVal);
                    break;
                //case ConnectType.Object:
                //    retVal = EditorGUILayout.ObjectField("Value: ", node.outVal,
                //        node.outVal.GetType(), false);
                //    break;
                case PinType.Vector2:
                    retVal = EditorGUILayout.Vector2Field("Value: ", (Vector2)node.OutVal);
                    break;
                case PinType.Vector3:
                    retVal = EditorGUILayout.Vector2Field("Value: ", (Vector3)node.OutVal);
                    break;
                case PinType.Vector4:
                    retVal = EditorGUILayout.Vector2Field("Value: ", (Vector4)node.OutVal);
                    break;
            }
        }
    }

    [CustomEditor(typeof(MathNode))]
    public class FunctionNodeE : Editor {
        void OnGUI() {
            MathNode node = (MathNode)target;
            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
        }
    }

    [CustomEditor(typeof(StartNode))]
    public class StartNodeE : Editor {
        void OnGUI() {
            StartNode node = (StartNode)target;
            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
        }
    }

    [CustomEditor(typeof(ControlNode))]
    public class ControlNodeE : Editor {
        void OnGUI() {
            ControlNode node = (ControlNode)target;
            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
        }
    }
#endregion

    [CustomEditor(typeof(NodeGraph))]
    public class NodeGraphE : Editor {
        void OnGUI() {
            NodeGraph graph = (NodeGraph)target;
            EditorGUILayout.LabelField(graph.Name, EditorStyles.boldLabel);
        }
    }
}
