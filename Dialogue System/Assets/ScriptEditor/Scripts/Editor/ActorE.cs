using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ScriptEditor.Graph;

namespace ScriptEditor.EditorScripts.Inspector {
    [CustomEditor(typeof(Actor))]
    public class ActorE : Editor {

        Actor a;
        public override void OnInspectorGUI() {
            bool isPlayer = a.gameObject.CompareTag("Player");
            if (isPlayer) {
                EditorGUILayout.LabelField("Player Actor");
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Name", GUILayout.Width(100));
            a.Name = GUILayout.TextField(a.Name);
            GUILayout.EndHorizontal();

            if (!isPlayer) {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Dialogue Script", GUILayout.Width(100));
                a.Script = (NodeGraph) EditorGUILayout.ObjectField(a.Script, typeof(NodeGraph), false);
                GUILayout.EndHorizontal();

                if(a.Script != null) {
                    if (a.Script.starts != null)
                        foreach (StartNode n in a.Script.starts)
                            EditorGUILayout.ObjectField("Start:", n, typeof(NodeBase), false);
                }
            }
        }

        private void OnEnable() {
            a = (Actor)target;
        }
    }
}
