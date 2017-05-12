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
        protected const float Left = 10, Header = 10;
        private Vector2 Center {
            get { return new Vector2(baseBox.x / 2f + Left, baseBox.y / 2f + Header); }
        }

        public virtual void Construct() {
            base.SetName("Start");
            outPins.Add(new OutputPin(this, VarType.Exec));
            Resize();
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Event;
            description = "Begins root script execution";
        }

        //public override void Lookup(bool compileTime) {
            
        //}

        /// <summary>
        /// Resize the body of node. Necessary when number of pins changes or the width of text to be displayed changes.
        /// </summary>
        protected override void Resize() {
            body = new Rect(0, 0, Width, 77);
            outPins[0].bounds.position = new Vector2(body.width - NodePin.margin.x -
                NodePin.pinSize.x, 30);
        }
    }
    
    /// <summary>
    /// Node that begins partial logic execution of a script.
    /// </summary>
    public class SubStartNode : StartNode {
        public override void Construct() {
            base.SetName("Sub Start");
            outPins.Add(new OutputPin(this, VarType.Exec));
            Resize();
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Event;
            description = "Begins script execution";
        }
    }


}
