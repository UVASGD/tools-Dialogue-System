using ScriptEditor.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ScriptEditor.EditorScripts {
    public class HeaderView : ViewBase {

        public HeaderView() : base(GetTitle()) {

        }

        #region Override
        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);
            ProcessEvents(e);
            if (graph != null) {
                title = graph.name;
            } else {
                title = GetTitle();
            }

            try {
                GUI.Box(body, "", skin.GetStyle("HeaderViewBackground"));
                GUILayout.BeginArea(body);
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("New Script",
                        skin.GetStyle("HeaderButton"),
                        GUILayout.Width(80),
                        GUILayout.Height(body.height))) {
                        GraphCreatePopup.Init();
                    }
                    if (graph != null)
                        if (GUILayout.Button("Delete Script",
                            skin.GetStyle("HeaderButton"),
                            GUILayout.Width(80),
                            GUILayout.Height(body.height))) {
                            NodeUtilities.DeleteScript(graph);
                        }
                    GUILayout.EndHorizontal();
                }  GUILayout.EndArea();
            } catch { }
        }

        public override void ProcessEvents(Event e) {
            base.ProcessEvents(e);
        }


        private static string GetTitle() {
            return "Header";
        }
        #endregion
    }
}
