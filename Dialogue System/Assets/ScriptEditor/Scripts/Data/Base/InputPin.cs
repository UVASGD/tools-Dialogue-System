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
        
        
        public int GetInt() { return IsConnected ? (int)ConnectedOutput.parentNode.Result() : val.GetInt(); }
        public bool GetBool() { return IsConnected ? (bool)ConnectedOutput.parentNode.Result() : val.GetBool(); }
        public float GetFloat() { return IsConnected ? (float)ConnectedOutput.parentNode.Result() : val.GetFloat(); }
        public string GetString() { return IsConnected ? (string)ConnectedOutput.parentNode.Result() : val.GetString(); }
        public Vector2 GetVector2() { return IsConnected ? (Vector2)ConnectedOutput.parentNode.Result() : val.GetVector2(); }
        public Vector3 GetVector3() { return IsConnected ? (Vector3)ConnectedOutput.parentNode.Result() : val.GetVector3(); }
        public Color GetColor() { return IsConnected ? (Color)ConnectedOutput.parentNode.Result() : val.GetColor(); }
        public Actor GetActor() { return IsConnected ? (Actor)ConnectedOutput.parentNode.Result() : val.getActor(); }

        public void SetInt(int val) { if (this.val.Type != VarType.Integer) return; this.val.SetInt(val); }
        public void SetBool(bool val) { if (this.val.Type != VarType.Bool) return; this.val.SetBool(val); }
        public void SetFloat(float val) { if (this.val.Type != VarType.Integer) return; this.val.SetFloat(val); }
        public void SetString(string val) { if (this.val.Type != VarType.String) return; this.val.SetString(val); }
        public void SetVector2(Vector2 val) { if (this.val.Type != VarType.Vector2) return; this.val.SetVector2(val); }
        public void SetVector3(Vector3 val) { if (this.val.Type != VarType.Vector3) return; this.val.SetVector3(val); }
        public void SetColor(Color val) { if (this.val.Type != VarType.Color) return; this.val.SetColor(val); }
        public void SetActor(Actor val) { if (this.val.Type != VarType.Actor) return; this.val.SetActor(val); }

        /// <summary> Default value of the input when pin is constructed </summary>
        //public object Default {
        //    get { return Value; }
        //    set { Value = value; }
        //}

        public InputPin(NodeBase n, VarType t, object val) : this(n, t) {
            switch (t) {
                case VarType.Integer: SetInt((int)val); break;
                case VarType.Bool: SetBool((bool)val); break;
                case VarType.Float: SetFloat((float)val); break;
                case VarType.String: SetString((string)val); break;
                case VarType.Vector2: SetVector2((Vector2)val); break;
                case VarType.Vector3: SetVector3((Vector3)val); break;
                case VarType.Color: SetColor((Color)val); break;
                case VarType.Actor: SetActor((Actor)val); break;
            }
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

        public override void OnAfterGraphDeserialize(){
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
