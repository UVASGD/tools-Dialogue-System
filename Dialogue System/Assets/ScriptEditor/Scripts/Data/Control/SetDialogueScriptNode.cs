﻿using System;
using UnityEngine;

namespace ScriptEditor.Graph
{
    public class SetDialogueScriptNode : ControlNode
    {
        public NodeGraph newScript;

        public SetDialogueScriptNode()
        {
        }

        public override void Construct()
        {
            // set information
            name = "Set Dialogue Script";
            description = "Sets the indicated dialogue script attached to the given actor.";
            // Create pins
            inPins.Add(new InputPin(this, VarType.Exec));
            inPins.Add(new InputPin(this, VarType.Actor));
            inPins[1].Name = "Actor";
            inPins[1].Description = "The Actor whose script will be changed";

            outPins.Add(new OutputPin(this, VarType.Exec));
        }

        public override void Execute()
        {

        }
    }
}
