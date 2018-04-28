using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor.Graph {
    /// <summary> Input pin. If disconnected, a constant can be provided </summary>
    [Serializable]
    public class InputPin : NodePin, ISerializationCallbackReceiver {
        [SerializeField] protected PinValue val;

        [SerializeField]
        private string cOutput;
        [NonSerialized]
        public OutputPin ConnectedOutput;
        public override bool IsConnected { get { return ConnectedOutput != null; } }

        public object Value {
            get { Debug.Log("IP.VAL:"); return val.Value; }
            set { val.Value = value; }
        }

        /// <summary> Default value of the input when pin is constructed </summary>
        public object Default {
            get { return Value; }
            set { Value = value; }
        }

        public InputPin(NodeBase n, VarType t, object val) : this(n, t) {
            Value = val;
        }

        public InputPin(NodeBase n, VarType t) : base(n, t) {
            val = new PinValue(t);
        }

        public override string ConName() {
            return ConnectedOutput.parentNode.name;
        }

        public override string ToString() {
            return "(IN) Node: " + parentNode.name + " | " + varType + "\n" + parentNode.description;
        }

        public void OnBeforeSerialize()
        {
            cOutput = this.parentNode.parentGraph.IDFromOutput(ConnectedOutput);
        }

        public void OnAfterDeserialize() { }

        public override void OnAfterGraphDeserialize()
        {
            ConnectedOutput = this.parentNode.parentGraph.OutputFromID(cOutput);
        }
    }

    /// <summary>
    /// connections to EventInputPins are not looked up during execution, only when
    /// the node graph is built in the script editor
    /// </summary>
    [Serializable]
    public class ExecInputPin : InputPin {
        public ExecInputPin(NodeBase n) : base(n, VarType.Exec) { }
    }

    /// <summary>
    /// connecteded outputs are necessary for easily finding the previous node within the graph
    /// </summary>
    [Serializable]
    public class ValueInputPin : InputPin {
        public ValueInputPin(NodeBase n, VarType t, object val) : base(n, t, val) { }
        public ValueInputPin(NodeBase n, VarType t) : base(n, t) { }
    }
}
