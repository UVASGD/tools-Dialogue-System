using ScriptEditor.Graph;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptEditor.EditorScripts.Inspector {

    static class PropertiesInspector {
        public static bool defaultGUI = false;

        //public static void showTooltips(Type t) {
        //    foreach(FieldInfo f in t.GetFields()) {
        //        TooltipAttribute ta = GetTooltip(f, true);
        //    }
        //}

        public static TooltipAttribute GetTooltip(FieldInfo field, bool inherit) {
            TooltipAttribute[] attributes = field.GetCustomAttributes(typeof(TooltipAttribute), inherit) as TooltipAttribute[];
            return attributes.Length > 0 ? attributes[0] : null;
        }
    }

#region NodeInspect

    [CustomEditor(typeof(MathNode))]
    [CanEditMultipleObjects]
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
    [CanEditMultipleObjects]
    public class DialogueNodeE : Editor {

        Vector2 scroll;
        SerializedProperty serProp;

        public void OnEnable() {
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            if (PropertiesInspector.defaultGUI) {
                base.OnInspectorGUI();
                return;
            }

            serProp = serializedObject.GetIterator();
            serProp.NextVisible(true);

            DialogueNode node = (DialogueNode)target;
            EditorStyles.textField.wordWrap = true;
            TooltipAttribute tooltip;

            EditorGUILayout.LabelField("Text Body", node.name, EditorStyles.boldLabel);
            //scroll = EditorGUILayout.BeginScrollView(scroll);
            tooltip = PropertiesInspector.GetTooltip(typeof(DialogueNode).GetField("text"), false);
            string tmp = EditorGUILayout.TextField(new GUIContent("", tooltip.tooltip), node.text);

            if (tmp != node.text)
                node.header = null;
            node.text = tmp;
            //EditorGUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            node.showName = EditorGUILayout.Toggle("Show Name", node.showName);
            if (node.showName)
                node.nickname = EditorGUILayout.TextField("Nickname", node.nickname);
            GUILayout.EndHorizontal();

            node.canSkip = EditorGUILayout.Toggle("Can Skip", node.canSkip);
            if (node.canSkip) {
                GUILayout.BeginHorizontal();
                node.autoSkip = EditorGUILayout.Toggle("Auto Skip", node.autoSkip);
                if (node.autoSkip)
                    node.autoSkipDelay = EditorGUILayout.FloatField("Auto Skip Delay", 
                        node.autoSkipDelay);
                GUILayout.EndHorizontal();
            } else {
                node.autoSkip = false;
            }

            node.hideOnExit = EditorGUILayout.Toggle("Hide On Exit", node.hideOnExit);
            //PropertiesInspector.showTooltips(typeof(DialogueNode));
            serializedObject.ApplyModifiedProperties();
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
