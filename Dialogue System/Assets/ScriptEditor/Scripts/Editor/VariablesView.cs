using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptEditor.Graph;
using UnityEngine;
using UnityEditor;

namespace ScriptEditor.EditorScripts{
    public class VariablesView : ViewBase{
		bool showMath, showControls, showDialogues, showFunc;
        public string text = "";
        private object parent;
        private float delay;
        private GUIStyle style;
        public Vector2 mouseLoc, size;
        private Vector2 scrollPos;
        Array ops, cTypes, fTypes;
        NodePin pinToAttach;

        public VariablesView() : base("Variable Dictionary") {

        }
			

        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);

			// reposition to fit on screen
			float dy = window.position.height - (body.height + body.y);
			if (dy < 0) body.y += dy;
			float dx = window.position.width - (body.width + body.x);
			if (dx < 0) body.x += dx;

			Vector2 margin = new Vector2(6, 12);
			size = new Vector2(200, 100);


			try {
				GUI.Box(body, "", skin.GetStyle("NodeCreationBackground"));
				GUILayout.BeginArea(new Rect(body.x + margin.x, body.y + margin.y,
					body.width - 2 * margin.x, body.height - 2 * margin.y));
				{
					
				}

                GUILayout.EndArea();
                
			} catch {

			}
		} 
    }
}
