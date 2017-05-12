using UnityEngine;
using System.Collections;

namespace ScriptEditor.Graph {
    public class EndNode : NodeBase {

        public virtual void Construct() {
            base.SetName("End");
            inPins.Add(new InputPin(this, VarType.Exec));
            Resize();
        }

        protected override void Resize() {
            body = new Rect(0, 0, Width, 77);
            inPins[0].bounds.position = new Vector2(NodePin.margin.x, 30);
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Event;
            description = "Finalizes script execution";
        }

        public override void Execute() {
            base.Execute();
        }
    }
}
