using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScriptEditor.Graph {

    /// <summary>
    /// Node that begins the logic execution of a script. Cannot be removed from script.
    /// </summary>
    public class StartNode : NodeBase {
        public virtual void Construct() {
            base.Construct("Start");
            outPins.Add(new OutputPin(this, PinType.Logic));
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Event;
            body = new Rect(0, 0, 170, 80);
        }

#if UNITY_EDITOR
        public override void DrawNode(Event e, Rect viewRect) {
            base.DrawNode(e, viewRect);
            
            DrawPins();
        }
#endif
    }
    
    /// <summary>
    /// Node that begins partial logic execution of a script.
    /// </summary>
    public class SubStartNode : StartNode {
        public override void Construct() {
            base.Construct("Sub Start");
            outPins.Add(new OutputPin(this, PinType.Logic));
        }

#if UNITY_EDITOR
        public override void DrawNode(Event e, Rect viewRect) {
            base.DrawNode(e, viewRect);

            GUI.Box(body, "", skin.GetStyle("NodeEventBackground"));
        }
#endif
    }


}
