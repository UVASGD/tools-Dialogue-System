using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScriptEditor.Graph {
    public class FetchNode : NodeBase {

        public PinType VarType { get { return outPins[0].varType; } }
        public object OutVal { get { return outPins[0].Value; } }

        public void Construct(object variable) {
            base.SetName(GetVarName(variable));
            outPins.Add(new OutputPin(this, variable));
            Resize();
        }

        public override void Execute() {
            base.Execute();
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Fetch;
            body = new Rect(0, 0, 150, 35);
        }

        public override void UpdateNode(Event e) {
            base.UpdateNode(e);
        }

        public override void DrawNode(Event e, Rect viewRect) {
            base.DrawNode(e, viewRect);
            
            DrawPins();
        }

    }
}
