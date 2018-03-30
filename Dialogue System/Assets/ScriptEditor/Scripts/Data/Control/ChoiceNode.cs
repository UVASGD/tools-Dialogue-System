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

            choiceTexts = new List<string>();
            choiceTexts.Add("Default Choice");
            for(int i=1; i<inPins.Count; i++)
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
            for (int i = 1; i<inPins.Count; i++) {
                // only add the choice if the output has been connected
                if(outPins[i].isConnected)
                    dc.AddChoice(choiceTexts[i], (bool)inPins[i].Value);
            }

            choice = -1;
            waitTime = 0;
        }

        protected override void Finalization() {
            base.Finalization();
            dc.ResetChoiceList();
        }

        public override NodeBase GetNextNode() {
            return outPins[choice].ConnectedInput.node;
        }
    }
}

