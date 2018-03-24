using System;
using System.Collections.Generic;

namespace ScriptEditor.Graph
{
	public class ChoiceNode : ControlNode {
        public List<string> choiceTexts;
        public bool hideDisabledChoices;
        public bool timed;
        public float timerLength;
        public int defaultChoice;

		public ChoiceNode ()
		{

		}

        public override void Construct() {
            // set information
            name = "Choice";
            description = "Splits execution based on an on-screen decision.";
            // Create pins
			multiplePins = true;
            inPins.Add(new EventInputPin(this));
            inPins.Add(new ValueInputPin(this, VarType.Bool));
            inPins.Add(new ValueInputPin(this, VarType.Bool));
            inPins[1].Name = "Condition 2";
            inPins[1].Default = true;
            inPins[2].Name = "Condition 3";
            inPins[2].Default = true;

            outPins.Add(new EventOutputPin(this));
            outPins.Add(new EventOutputPin(this));
            outPins.Add(new EventOutputPin(this));
            outPins[0].Name = "Default";
            outPins[0].Description = "What happens if no choice is available or selected.";
            outPins[1].Name = "Choice 2";
            outPins[2].Name = "Choice 3";
            nodeType = NodeType.Dialog;
        }

        // called everyframe
        public override void Execute()
        {
            finished = true;
        }

    }
}

