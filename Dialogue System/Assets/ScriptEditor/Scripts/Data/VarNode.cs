using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptEditor.Graph {
    public class VarNode : NodeBase {

        string varName;

        /// <summary>
        /// Construct a node from a promoted variable
        /// </summary>
        /// <param name="variableType"></param>
        public void Construct(Variable variable, bool isInput) {
            
            if (isInput) {
                description = "Sets the value of a variable";
                inPins.Add(new InputPin(this, variable.varType));
                inPins[0].Description = "";
                inPins[0].Default = null;
            } else {
                description = "Gets the value of a variable";
                outPins.Add(new OutputPin(this, variable.varType));
                outPins[0].Description = "";
            }
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Variable;
            //body = bounce baby bounce;
        }

#if UNITY_EDITOR
        public override void DrawNode(Event e, Rect viewRect) {
            base.DrawNode(e, viewRect);


        }
#endif
    

    }
}
