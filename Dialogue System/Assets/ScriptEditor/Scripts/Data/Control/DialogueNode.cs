using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor.Graph
{
    public class DialogueNode : ControlNode
    {

        [Tooltip("The source text of the dialogue")]
        public string text;
        [Tooltip("The text to replace the header part of the dialogue")]
        public string nickname;
        public bool showName = true;
        public bool autoSkip = false;
        public double autoSkipDelay = 6f;
        public bool canSkip = true;
        public bool hideOnExit;
        public Sprite icon;

        private Vector2 Index;
        private Transform previousFocus, camera;
        private List<String> pages;
        private DialogueController dc;

        private float speed;

        public DialogueNode(){
        }

        public override void Construct()
        {
            // set information
            name = "Show Dialogue";
            description = "Displays a conversation dialogue to a dialogue box based on the conversant.";
            
            // Create pins
            inPins.Add(new EventInputPin(this));
            inPins.Add(new ValueInputPin(this, VarType.Actor));
            inPins[1].Name = "Speaker";
            inPins[1].Description = "The Actor who speaks the text. Can be left empty";
            inPins.Add(new ValueInputPin(this, VarType.Bool));
            inPins[2].Name = "Focus on";
            inPins[2].Description = "Whether or not the main camera should focus on the conversant";
            inPins[2].Default = true;
            inPins.Add(new ValueInputPin(this, VarType.Float));
            inPins[3].Name = "Speed";
            inPins[3].Description = "How fast the text is displayed";
            inPins[3].Default = 1f;
            inPins.Add(new ValueInputPin(this, VarType.Object));
            inPins[4].Name = "User Data";

            outPins.Add(new EventOutputPin(this));
            nodeType = NodeType.Dialog;
        }

        public override void Execute(){
            if (!setupCompleted) Setup(); 
            dc.outputTextbox.text = pages[(int)Index.x];

            Finalization();
        }

        /// <summary>
        /// Ensure that everything is setup properly for the dialogue window
        /// </summary>
        protected override void Setup() {
            dc = GameObject.FindObjectOfType<DialogueController>();
            pages = new List<string>();
            pages.AddRange(text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));
            Index = Vector2.zero;
            Actor actor = null;

            if (inPins[1].isConnected) {
                actor = (Actor)inPins[1].Value;
            }

            // set the header
            if (nickname != null) {
                dc.outputHeader.text = nickname;
            } else if (actor != null) {
                dc.outputHeader.text = actor.Name;
            } else {
                dc.outputHeader.text = "";
            }

            // set the speed from % to character per second (or frame?)
            speed = (int)inPins[3].Value;

            // focus on speaker
            if(actor != null) {
                // change this to not be controlled by var?
                if (inPins[2].isConnected) {
                    if ((bool)inPins[2].Value) {
                        GameObject tmp = GameObject.Find("Main Camera");
                        if (tmp != null) {
                            camera = tmp.transform;
                            previousFocus = camera.parent;
                            camera.parent = actor.transform;
                        }
                    }
                }
            }


        }

        protected override void Finalization() {
            // reset camera
            if (previousFocus != null) {
                camera.parent = previousFocus;
                previousFocus = null;
            }

            finished = true;
        }
    }
}

