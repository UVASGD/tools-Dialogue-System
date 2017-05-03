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

        protected static Vector2 baseBox = new Vector2 ();
        protected static Vector2 margin = new Vector2(10, 10);

        public virtual void Construct() {
            base.SetName("Start");
            outPins.Add(new OutputPin(this, PinType.Logic));
            Resize();
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Event;
        }
        
        /// <summary>
        /// Resize the body of node. Necessary when number of pins changes or the width of text to be displayed changes.
        /// </summary>
        protected override void Resize() {
            body = new Rect(0, 0, 170, 80);
            baseBox = body.size - margin;

            // reposition output node
            float y = body.height / 2f;
            float x = body.width - NodePin.margin.x - NodePin.pinSize.x;
            outPins[0].bounds.position = new Vector2(x, y);
        }
    }
    
    /// <summary>
    /// Node that begins partial logic execution of a script.
    /// </summary>
    public class SubStartNode : StartNode {
        public override void Construct() {
            base.SetName("Sub Start");
            outPins.Add(new OutputPin(this, PinType.Logic));
            Resize();
        }
    }


}
