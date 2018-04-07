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

        [Tooltip("Whether or not starting the dialogue locks the player. Defaults to true.")]
        public bool LockPlayer = true;

        protected string indexName = "Default";
        protected static Vector2 baseBox = new Vector2 ();
        protected const float Left = 10, Header = 10;
        private Vector2 Center {
            get { return new Vector2(baseBox.x / 2f + Left, baseBox.y / 2f + Header); }
        }

        /// <summary> name of the substart </summary>
        public string IndexName {
            get { return indexName; }
            set { indexName = value; }
        }

        public virtual void Construct() {
            base.SetName("Start");
            outPins.Add(new ExecOutputPin(this));
            Resize();
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Event;
            description = "Begins root script execution";
        }

        public override void Execute() {
            DialogueController dc = GameObject.FindObjectOfType<DialogueController>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            dc.isPlayerLocked = LockPlayer;
            Finalization();
        }

        /// <summary>
        /// Resize the body of node. Necessary when number of pins changes or the width of text to be displayed changes.
        /// </summary>
        public override void Resize() {
            body = new Rect(0, 0, Width, 77);
            outPins[0].bounds.position = new Vector2(body.width - NodePin.margin.x -
                NodePin.pinSize.x, 30);
        }

        public override bool Equals(object other) {
            if (!(other is StartNode)) return false;
            return ((StartNode)other).indexName.Equals(indexName);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
    
    /// <summary>
    /// Node that begins partial logic execution of a script.
    /// </summary>
    public class SubStartNode : StartNode {
        public override void Construct() {
            base.SetName("Sub Start");
            outPins.Add(new ExecOutputPin(this));
            Resize();
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Event;
            description = "Begins script execution";
        }
    }


}
