using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptEditor.Graph;
using UnityEngine;

namespace ScriptEditor.EditorScripts{
    class VariablesView : ViewBase{

        public VariablesView() : base("Variable Dictionary") {

        }

        public override void DrawView(Rect editorRect, Rect PercentRect, Event e, NodeGraph graph) {
            base.DrawView(editorRect, PercentRect, e, graph);
        }
    }
}
