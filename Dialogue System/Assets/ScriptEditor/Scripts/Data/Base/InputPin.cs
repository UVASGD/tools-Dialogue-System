using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor.Graph {
    /// <summary> Input pin. If disconnected, a constant can be provided </summary>
    [Serializable]
    public class InputPin : NodePin {
        public virtual OutputPin ConnectedOutput { get; set; }

        [SerializeField] private object defaultVal;
        /// <summary> Default value of the input when pin is constructed </summary>
        public object Default {
            get { return defaultVal; }
            set {
                if (value != null) {
                    switch (varType) {
                        case VarType.Bool: defaultVal = (bool)value; break;
                        case VarType.Float: defaultVal = (float)value; break;
                        case VarType.Integer: defaultVal = (int)value; break;
                        case VarType.String: defaultVal = (string)value; break;
                        case VarType.Vector2: defaultVal = (Vector2)value; break;
                        case VarType.Vector3: defaultVal = (Vector3)value; break;
                        case VarType.Vector4: defaultVal = (Vector4)value; break;
                        case VarType.Color: defaultVal = (Color)value; break;
                    }
                    val = defaultVal;
                }
            }
        }

        public InputPin(NodeBase n, object val) : base(n, val) {
            Default = val;
        }
        public InputPin(NodeBase n, VarType t) : base(n, t) { }

        public override string ConName() {
            return ConnectedOutput.node.name;
        }
        public override string ToString() {
            return "(IN) Node: " + node.STName + " | " + varType + "\n" + node.description;
        }
    }

    /// <summary>
    /// connections to EventInputPins are not looked up during execution, only when
    /// the node graph is built in the script editor
    /// </summary>
    [Serializable]
    public class EventInputPin : InputPin {
        public EventInputPin(NodeBase n, object val) : base(n, val) { }
        public EventInputPin(NodeBase n) : base(n, VarType.Exec) { }

        private string cOutput = null;

        public override OutputPin ConnectedOutput {
            get { return this.node.parentGraph.OutputFromID(cOutput); }
            set { cOutput = this.node.parentGraph.IDFromOutput(value); }
        }
    }

    /// <summary>
    /// connecteded outputs are necessary for easily finding the previous node within the graph
    /// </summary>
    [Serializable]
    public class ValueInputPin : InputPin {
        public ValueInputPin(NodeBase n, object val) : base(n, val) { }
        public ValueInputPin(NodeBase n, VarType t) : base(n, t) { }

        private OutputPin cOutput = null;

        public override OutputPin ConnectedOutput {
            get { return cOutput; }
            set { cOutput = value; }
        }
    }
}