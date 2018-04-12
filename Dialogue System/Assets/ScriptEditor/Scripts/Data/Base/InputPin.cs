using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor.Graph {
    /// <summary> Input pin. If disconnected, a constant can be provided </summary>
    [Serializable]
    public class InputPin : NodePin {
        [SerializeField] protected PinValue val;

        public virtual OutputPin ConnectedOutput { get; set; }
        public override bool IsConnected { get { return ConnectedOutput != null; } }

        public object Value {
            get { return val.Value; }
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
            return "(IN) Node: " + parentNode.STName + " | " + varType + "\n" + parentNode.description;
        }
    }

    /// <summary>
    /// connections to EventInputPins are not looked up during execution, only when
    /// the node graph is built in the script editor
    /// </summary>
    [Serializable]
    public class ExecInputPin : InputPin {
        public ExecInputPin(NodeBase n) : base(n, VarType.Exec) { }

        [SerializeField] private string cOutput = null;

        public override OutputPin ConnectedOutput {
            get { return this.parentNode.parentGraph.OutputFromID(cOutput); }
            set { cOutput = this.parentNode.parentGraph.IDFromOutput(value); }
        }
    }

    /// <summary>
    /// connecteded outputs are necessary for easily finding the previous node within the graph
    /// </summary>
    [Serializable]
    public class ValueInputPin : InputPin {
        public ValueInputPin(NodeBase n, VarType t, object val) : base(n, t, val) { }
        public ValueInputPin(NodeBase n, VarType t) : base(n, t) { }

        [SerializeField] private ValueOutputPin cOutput = null;
        [SerializeField] private int totalConnections = 0;

        public override OutputPin ConnectedOutput {
            get { return cOutput; }
            set {
                totalConnections += value == null ? -1 : 1;
                cOutput = (ValueOutputPin) value;
            }
        }
    }
}