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
            inPins.Add(new EventInputPin(this));
            inPins.Add(new ValueInputPin(this, VarType.String));
            inPins[1].Name = "Text";
            inPins[1].Default = "Hello World";

            outPins.Add(new EventOutputPin(this));
        }

        public override void Execute()
        {
            finished = true;
        }
    }
}
