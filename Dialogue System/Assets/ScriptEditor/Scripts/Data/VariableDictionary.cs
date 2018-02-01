using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor.Graph {


    /// <summary>
    /// Containment class for variables
    /// </summary>
    [SerializeField]
    public class VariableDictionary {

        private Dictionary<string, Variable> variables;

        //public void addVariable(string varName, VarType varType) {
        //    addVariable(new Variable(varName, varType));
        //}

        public void addVariable(Variable var) {
            variables.Add(var.name, var);
        }

        public Variable getVariable(string varName)  {
            return variables.ContainsKey(varName) ? variables[varName] : null;
        }
    }

    /// <summary>
    /// self explanitory class; basically a structure for storing var data
    /// </summary>
    public class Variable {

        public string name = "NewVariable";
        public VarType varType = VarType.Object;
        public object value = null;
        public VarScope scope { get { return varScope; } }

        public enum VarScope { Global, Local }

        private VarScope varScope = VarScope.Global;

        public Variable(string name, VarType varType) : this(name, varType, null, VarScope.Global){}
        public Variable(string name, VarType varType, object initialValue, VarScope scope) {
            this.name = name;
            this.varType = varType;
            this.value = initialValue;
            this.varScope = scope;
        }

    }
}
