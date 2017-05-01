using ScriptEditor.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ScriptEditor.EditorScripts {
    public class StatusView : ViewBase {
        
        public string statusTxt;

        public StatusView() : base(getTitle()) {
            resetStatus();
        }

        public void resetStatus() {
            if (graph != null) statusTxt = graph.path;
            else statusTxt = "No script loaded.";
            //statusTxt += "  " + graph;
        }
        private void ProcessContextMenu(Event e) {

        }

        private void ContextCallback(object obj) {

        }

        #region Override
        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);
            ProcessEvents(e);
            if (graph != null) {
                title = graph.name;
            } else {
                title = getTitle();
            }


            GUI.Box(body, "", skin.GetStyle("HeaderViewBackground"));
            GUILayout.BeginArea(body);
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent(statusTxt));
                GUILayout.EndHorizontal();
            } GUILayout.EndArea();
            if(Selection.activeGameObject == null) {
                resetStatus();
            } else {
                // show status for selected Node
            }
        }

        public override void ProcessEvents(Event e) {
            base.ProcessEvents(e);
        }


        private static string getTitle() {
            return "Status";
        }
        #endregion
    }
}
