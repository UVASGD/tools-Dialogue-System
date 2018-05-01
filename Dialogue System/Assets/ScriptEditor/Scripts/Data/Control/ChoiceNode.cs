using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor.Graph
{
	public class ChoiceNode : ControlNode {

        public List<string> choiceTexts;
        [Tooltip("If true, choices whose Condition is false will not be shown. Otherwise, these choices will be “greyed out”")]
        public bool hideDisabledChoices;
        [Tooltip("Whether or not to limit how much time a player has to decide")]
        public bool timed;
        [Tooltip("The amount of time in seconds to allow the player to make a decision before automatically choosing the default choice")]
        public float timerLength = 20f;
        [Tooltip("The default choice that is chosen when the Timer Length reaches 0 seconds. The default index is 0")]
        public int defaultChoice;
        [Tooltip("How to orient the choices when displaying")]
        public ChoiceOrientation choiceOrientation = ChoiceOrientation.VerticalFromBottom;

        private int choice;
        private float waitTime;

        public float TotalTime { get { return waitTime; } }

        public enum ChoiceOrientation {
            Radial, VerticalFromTop, VerticalFromBottom,
            HorizontalFromLeft, HorizontalFromRight
        }

		public ChoiceNode (){ }

        public override void Construct() {
            // set information
            name = "Choice";
            description = "Splits execution based on an on-screen decision.";

            // Create pins
			multiplePins = true;
            execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.Bool, true));
            valInPins[0].Name = "Condition 2";
            valInPins.Add(new ValueInputPin(this, VarType.Bool, true));
            valInPins[1].Name = "Condition 3";

            execOutPins.Add(new ExecOutputPin(this));
            execOutPins[0].Name = "Default Choice";
            execOutPins[0].Description = "What happens if no choice is available or selected.";
            execOutPins.Add(new ExecOutputPin(this));
            execOutPins[1].Name = "Choice 2";
            execOutPins.Add(new ExecOutputPin(this));
            execOutPins[2].Name = "Choice 3";
            nodeType = NodeType.Dialog;

            choiceTexts = new List<string>();
            choiceTexts.Add("Default Choice");
            for(int i=1; i< execOutPins.Count; i++)
                choiceTexts.Add("Choice " + (i+1));
        }

        // called everyframe
        public override void Execute(){
            if(!setupCompleted) Setup();

            // time the user!!!
            if (timed) {
               waitTime += Time.deltaTime;
                if(waitTime > timerLength) {
                    // select default choice
                    choice = 0;
                }
            }

            if (choice > -1) Finalization();
        }

        /// <summary>
        /// apply choice index
        /// </summary>
        /// <param name="c"></param>
        public void Click(int c) {
            choice = c;
        }

        protected override void Setup() {
            dc = GameObject.FindObjectOfType<DialogueController>();
            dc.AddChoice(choiceTexts[0], true);
            for (int i = 1; i<valInPins.Count; i++) {
                // only add the choice if the output has been connected
                if(execOutPins[i-1].IsConnected)
                    dc.AddChoice(choiceTexts[i], valInPins[i].GetBool());
            }

            choice = -1;
            waitTime = 0;
        }

        protected override void Finalization() {
            base.Finalization();
            dc.ResetChoiceList();
        }

        public override NodeBase GetNextNode() {
            return execOutPins[choice].ConnectedInput.parentNode;
        }
    }
}

