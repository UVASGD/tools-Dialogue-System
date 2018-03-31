using ScriptEditor.Graph;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScriptEditor.EditorScripts.Inspector {

    static class PropertiesInspector {
        public static bool defaultGUI = false;
    }

#region NodeInspect

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

    [CustomEditor(typeof(DialogueNode))]
    public class DialogueNodeE : Editor {

        //Vector2 scroll;

        public override void OnInspectorGUI() {
            if (PropertiesInspector.defaultGUI) {
                base.OnInspectorGUI();
                return;
            }

            DialogueNode node = (DialogueNode)target;
            EditorGUILayout.LabelField("Text Body", node.name, EditorStyles.boldLabel);

            //scroll = EditorGUILayout.BeginScrollView(scroll);
            string tmp = EditorGUILayout.TextArea(node.text);
            if (!tmp.Equals(node.text))
                node.header = null;
            node.text = tmp;
            //EditorGUILayout.EndScrollView();

            node.showName = EditorGUILayout.Toggle("Show Name", node.showName);

            node.canSkip = EditorGUILayout.Toggle("Can Skip", node.canSkip);
            if (node.canSkip) {
                //GUILayout.BeginHorizontal();
                node.autoSkip = EditorGUILayout.Toggle("Auto Skip", node.autoSkip, GUILayout.Width(75));
                if (node.autoSkip)
                    node.autoSkipDelay = EditorGUILayout.FloatField("Auto Skip Delay", 
                        node.autoSkipDelay);
                //GUILayout.EndHorizontal();
            } else {
                node.autoSkip = false;
            }

            node.hideOnExit = EditorGUILayout.Toggle("Hode On Exit", node.hideOnExit);
        }
    }
#endregion

    [CustomEditor(typeof(NodeGraph))]
    public class NodeGraphE : Editor {
        public override void OnInspectorGUI() {
            if (PropertiesInspector.defaultGUI) {
                base.OnInspectorGUI();
                return;
            }

            NodeGraph graph = (NodeGraph)target;
            EditorGUILayout.LabelField(graph.Name, EditorStyles.boldLabel);
        }
    }
}
