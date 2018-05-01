using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptEditor.Graph
{
    public class BranchNode : ControlNode
    {
        public BranchNode()
        {
        }

        public override void Construct()
        {
            // set information
            name = "Branch";
            description = "Splits execution based on the value of the condition.";

            // Create pins
            execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.Bool, true));
            valInPins[0].Name = "Condition";

            execOutPins.Add(new ExecOutputPin(this));
            execOutPins[0].Name = "True";
            execOutPins.Add(new ExecOutputPin(this));
            execOutPins[1].Name = "False";
        }

        // called everyframe
        public override void Execute()
        {
            finished = true;
        }

        public override NodeBase GetNextNode()
        {
            // condition is based on default value or a lookup value
            bool condition = valInPins[0].GetBool();

            NodeBase output = null;
            OutputPin pin = execOutPins[condition ? 0 : 1];
            if (pin.IsConnected)
                output = pin.ConnectedInput.parentNode;

            return output;
        }
    }
}
