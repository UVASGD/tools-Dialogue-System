using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptEditor.Graph {
    public class BranchNode : ControlNode {


        public BranchNode() {


        }

        // called everyframe
        public override void Execute() {

        }

        public /*override*/ void Construct() {
            // set information
            name = "Branch";
            description = "Splits execution based on the value of the condition.";

            // Create pins
            inPins.Add(new InputPin(this, VarType.Exec));
            inPins.Add(new InputPin(this, VarType.Bool));
            inPins[1].Name = "Condition";
            inPins[1].Default = true;

            outPins.Add(new OutputPin(this, VarType.Exec));
            outPins.Add(new OutputPin(this, VarType.Exec));
            outPins[0].Name = "True";
            outPins[1].Name = "False";
        }

        public /*override*/ NodeBase GetNextNode() {
            // condition is based on default value or a lookup value
            bool condition = (bool)inPins[0].Value;

            NodeBase output = null;
            OutputPin pin = outPins[condition ? 0 : 1];
            if (pin.isConnected) 
                output = pin.ConnectedInput.node;
            
            return output;
        }
    }
}
