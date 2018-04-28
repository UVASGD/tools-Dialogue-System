using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor.Graph {
    /// <summary> Output connection. Must always have a value, even if disconnected </summary>
    [Serializable]
    public abstract class OutputPin : NodePin, ISerializationCallbackReceiver {
        //public int ConnectedInputID = -1;
        [SerializeField]
        private string cInput;
        [NonSerialized]
        public InputPin ConnectedInput;
        public override bool IsConnected { get { return ConnectedInput != null; } }

        public OutputPin(NodeBase n, VarType t) : base(n, t) { }

        public override string ConName() {
            return "???";
        }

        public override string ToString() {
            return "(OUT) Node: " + (parentNode == null ? "null" : parentNode.name) + " | " + varType;
        }

        public void OnBeforeSerialize()
        {
            cInput = this.parentNode.parentGraph.IDFromInput(ConnectedInput);
        }

        public void OnAfterDeserialize() { }

        public override void OnAfterGraphDeserialize()
        {
            ConnectedInput = this.parentNode.parentGraph.InputFromID(cInput);
        }
    }

    /// <summary>
    /// connections to ValueOutputPins are not looked up during execution, only when
    /// the node graph is built in the script editor
    /// </summary>
    [Serializable]
    public class ValueOutputPin : OutputPin {
        public ValueOutputPin(NodeBase n, VarType t) : base(n, t) { }
    }

    /// <summary>
    /// connecteded inputs are necessary for easily finding the node withing the graph
    /// </summary>
    [Serializable]
    public class ExecOutputPin : OutputPin {
        public ExecOutputPin(NodeBase n) : base(n, VarType.Exec) { }
    }
}
