using System;

namespace ScriptEditor.Graph{
	public class ControlNodeDialogue : ControlNode
	{
		public ControlNodeDialogue()
		{
		}

		public override void Execute() {

		}

		public override void Construct() {
			// set information
			name = "Show Dialogue";
            description = "Displays a conversation dialogue to a dialogue box based on the conversant.";
			// Create pins
			inPins.Add(new InputPin(this, VarType.Exec));
            inPins.Add(new InputPin(this, VarType.Actor));
            inPins.Add(new InputPin(this, VarType.Bool));
            inPins.Add(new InputPin(this, VarType.Float));
            inPins.Add(new InputPin(this, VarType.Object));
            inPins[1].Name = "Conversant";
            inPins[1].Description = "The Actor who speaks the text. Can be left empty";
            inPins[2].Name = "Focus on";
            inPins[2].Description = "Whether or not the main camera should focus on the conversant";
            inPins[2].Default = true;
            inPins[3].Name = "Speed";
            inPins[3].Description = "How fast the text is displayed";
            inPins[3].Default = 3f;
            inPins[4].Name = "User Data";

            outPins.Add(new OutputPin(this, VarType.Exec));
            nodeType = NodeType.Dialog;
			}
		}
	}

