using System;
using UnityEngine;

namespace ScriptEditor.Graph
{
    public class PrintNode : ControlNode
    {
        public Color color;
        public bool printToConsole = true;
        public bool printToScreen = false;

        public PrintNode()
        {
        }

        public override void Construct()
        {
            //set information
            name = "Print Text";
            description = "Prints text to a console";
            // Create Pins
            execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.String));
            valInPins[1].Name = "Text";
            valInPins[1].Default = "Hello World";

            execOutPins.Add(new ExecOutputPin(this));
        }

        public override void Execute()
        {
            finished = true;
        }
    }
}
