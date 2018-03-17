using System;
using UnityEngine;

namespace ScriptEditor.Graph
{
    public class DialogueNode : ControlNode
    {
        public string text;
        public string nickname;
        public bool showName = true;
        public bool autoSkip = false;
        public double autoSkipDelay;
        public bool canSkip = true;
        public bool hideOnExit;
        public Sprite icon;

        public DialogueNode()
        {
        }

        public override void Construct()
        {
            // set information
            name = "Show Dialogue";
            description = "Displays a conversation dialogue to a dialogue box based on the conversant.";
            // Create pins
            inPins.Add(new InputPin(this, VarType.Exec));
            inPins.Add(new InputPin(this, VarType.Actor));
            inPins[1].Name = "Speaker";
            inPins[1].Description = "The Actor who speaks the text. Can be left empty";
            inPins.Add(new InputPin(this, VarType.Bool));
            inPins[2].Name = "Focus on";
            inPins[2].Description = "Whether or not the main camera should focus on the conversant";
            inPins[2].Default = true;
            inPins.Add(new InputPin(this, VarType.Float));
            inPins[3].Name = "Speed";
            inPins[3].Description = "How fast the text is displayed";
            inPins[3].Default = 3f;
            inPins.Add(new InputPin(this, VarType.Object));
            inPins[4].Name = "User Data";

            outPins.Add(new OutputPin(this, VarType.Exec));
            nodeType = NodeType.Dialog;
        }

        public override void Execute()
        {

        }
    }
}

