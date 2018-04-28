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

            DialogueNode node = (DialogueNode)target;
            EditorStyles.textField.wordWrap = true;

            EditorGUILayout.LabelField(node.name, EditorStyles.boldLabel);
            //scroll = EditorGUILayout.BeginScrollView(scroll);
            string oldText = node.text;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("text"));

            if (node.text != oldText) {
                node.header = null;
            }
            //node.text = tmp;
            //EditorGUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("showName"));
            if (node.showName)
                EditorGUILayout.PropertyField(serializedObject.FindProperty("nickname"));
            GUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("canSkip"));
            if (node.canSkip) {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("autoSkip"));
                if (node.autoSkip)
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("autoSkipDelay"));
                GUILayout.EndHorizontal();
            } else {
                node.autoSkip = false;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("hideOnExit"));
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
