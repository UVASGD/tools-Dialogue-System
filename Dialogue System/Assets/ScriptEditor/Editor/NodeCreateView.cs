using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptEditor.Graph;
using UnityEngine;
using UnityEditor;

namespace ScriptEditor.EditorScripts {
    public class NodeCreateView : ViewBase{

        public Vector2 mouseLoc, size;
        private Vector2 scrollPos;
        Array ops, cTypes, fTypes;

        bool showMath, showControls, showVar;

        public NodeCreateView(Vector2 mousePos):base("Node View") {
            mouseLoc = mousePos;
            // to DO
            size = new Vector2(200, 100);
            scrollPos = new Vector2();

            ops = Enum.GetValues(typeof(MathNode.OpType));
            cTypes = Enum.GetValues(typeof(ControlNode.ControlType));
            fTypes = Enum.GetValues(typeof(FunctionNode.FunctionType));
        }

        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);

            // reposition to fit on screen
            float dy = window.position.height - (body.height + body.y);
            if (dy < 0) body.y += dy;
            float dx = window.position.width - (body.width + body.x);
            if (dx < 0) body.x += dx;

            ProcessEvents(e);

            Vector2 margin = new Vector2(6, 12);
            size = new Vector2(200, 100);

            //Rect viewRect = new Rect(0, 0, body.width, body.height);
            //Rect ctrlRect = new Rect(x,y, ctrlWidth, ctrlHeight);
            GUI.Box(body, "", skin.GetStyle("NodeCreationBackground"));
            GUILayout.BeginArea(new Rect(body.x+margin.x, body.y+margin.y,
                body.width - 2*margin.x, body.height - 2*margin.y));
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
            EditorGUILayout.BeginVertical();
            {
                if(showMath = EditorGUILayout.Foldout( showMath, "Math")) {
                    foreach (MathNode.OpType op in ops) {
                        size.y += 10;
                        if (GUILayout.Button(op.ToString(), skin.GetStyle("NodeCreationButton"))) {
                            // create math FunctionNode
                            Destroy();

                            MathNode mn = new MathNode();
                            mn.Initialize(); mn.Construct(op, PinType.Float, 2);
                            NodeCreatePopup.Init(NodeType.Math, op, mouseLoc, mn.multiplePins?-1:2);
                        }
                    }
                } if (showControls = EditorGUILayout.Foldout(showControls, "Functions")) {
                    
                    foreach (ControlNode.ControlType cT in cTypes){
                        if(size.y<400) size.y += 10;
                        if (cT == ControlNode.ControlType.Custom) GUI.enabled = false;
                        else if (GUILayout.Button(cT.ToString(), skin.GetStyle("NodeCreationButton"))) {
                            Destroy();
                            NodeCreatePopup.Init(NodeType.Control, cT, mouseLoc);

                        }
                        if (cT == ControlNode.ControlType.Custom) GUI.enabled = true;
                    }
                    foreach (FunctionNode.FunctionType cT in fTypes) {
                        if (size.y < 400) size.y += 10;
                        if (GUILayout.Button(cT.ToString(), skin.GetStyle("NodeCreationButton"))) {
                            Destroy();
                            NodeCreatePopup.Init(NodeType.Function, cT, mouseLoc);
                        }
                        
                    }
                } if (showVar = EditorGUILayout.Foldout( showVar, "Variable")) {
                    
                    // add var from history

                    // or add new var (select Type)
                }

            } EditorGUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        public void Destroy() {
            window.nodeCreateView = null;
        }

        public override void ProcessEvents(Event e) {
            base.ProcessEvents(e);

            if (e.type == EventType.ContextClick || e.type == EventType.MouseDown) {
                //Debug.Log("NCV clicked");
                if (!body.Contains(e.mousePosition)){
                    // destroy window
                    Destroy();
                }

            }
        }

    }
}
