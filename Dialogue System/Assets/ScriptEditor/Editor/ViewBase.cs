using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using ScriptEditor.Graph;

namespace ScriptEditor.EditorScripts {
    [Serializable]
    public class ViewBase  {

        //public string name;
        public NodeType type { get { return nodeType; } }
        public Vector2 center { get { return new Vector2(body.x+body.width/2f, body.y+body.height/2f); } }

        protected NodeType nodeType;
        protected Rect body;
        protected string title;
        protected GUISkin skin;
        protected NodeGraph graph;
        protected ScriptEditorWindow window { get { return EditorWindow.GetWindow<ScriptEditorWindow>(); } }

        public ViewBase(string title) {
            this.title = title;
            GetEditorSkin();
        }

        public ViewBase(string title, Rect body): this(title) {
            this.body = body;
        }

        public virtual void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            if (skin == null) {
                GetEditorSkin();
                return;
            }

            body = new Rect(editorRect.x * PercentRect.x,
                            editorRect.y * PercentRect.y,
                            editorRect.width * PercentRect.width,
                            editorRect.height * PercentRect.height);
            this.graph = graph;
        }

        public virtual void ProcessEvents(Event e) {

        }

        protected void GetEditorSkin() {
            string skinName = EditorGUIUtility.isProSkin ? "NodeEditorDark" : "NodeEditorLight";
            skin = Resources.Load<GUISkin>("GUI Skins/Editor/" + skinName);
            //foreach (UnityEngine.Object o in Resources.FindObjectsOfTypeAll(typeof(GUISkin))) {
            //    //Debug.Log("Skin: " + o);
            //    if (o.name.Equals(skinName)) {
            //        skin = (GUISkin)o;
            //        break;
            //    }
            //}
        }
    }
}
