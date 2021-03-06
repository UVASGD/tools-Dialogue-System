﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor.Graph
{
    public class DialogueNode : ControlNode
    {

        [TextArea(10,20), Tooltip("The source text of the dialogue. Can be delimited to show data contained within variables")]
        public string text;
        [Tooltip("The text to replace the header part of the dialogue")]
        public string nickname;
        [Tooltip("Setting to hide/show the name/nickname of the speaker")]
        public bool showName = true;
        [Tooltip("whether or not to automatically skip to the next page when all text from the current page has been displayed")]
        public bool autoSkip = false;
        [Tooltip("Controls the delay between showing each page of dialogue")]
        public float autoSkipDelay = 6f;
        [Tooltip("Local setting that limits whether or not the player can skip the gradual display of the text")]
        public bool canSkip = true;
        [Tooltip("Whether or not to hide the dialogue box upon exiting the node")]
        public bool hideOnExit;
        [Tooltip("Reference to a local Sprite/Texture to show in the dialogue canvas")]
        public Sprite icon;


        [HideInInspector] public string header;
        private Vector2 Index;
        private Transform previousFocus, camera;
        private List<String> pages;
        private AudioSource src;
        private GameObject TextAudio;

        /// <summary> speed of text display in character per frame </summary>
        private float speed;
        private float skipTime;
        /// <summary> if the dialog is waiting for user input </summary>
        private bool pleaseContinue;
        private float MaxCharLength {
            get {
                if (pages!= null) return pages[(int)Index.x].Length;
                return -1;
            }
        }

        public DialogueNode(){

        }

        public override void Construct()
        {
            // set information
            name = "Show Dialogue";
            description = "Displays a conversation dialogue to a dialogue box based on the conversant.";
            
            // Create pins
            execInPins.Add(new ExecInputPin(this));
            valInPins.Add(new ValueInputPin(this, VarType.Actor));
            valInPins[0].Name = "Speaker";
            valInPins[0].Description = "The Actor who speaks the text. Can be left empty";
            valInPins.Add(new ValueInputPin(this, VarType.Bool, true));
            valInPins[1].Name = "Focus on";
            valInPins[1].Description = "Whether or not the main camera should focus on the conversant";
            valInPins.Add(new ValueInputPin(this, VarType.Float, 1f));
            valInPins[2].Name = "Speed";
            valInPins[2].Description = "How fast the text is displayed";
            //valInPins[2].Default = 1f;
            valInPins.Add(new ValueInputPin(this, VarType.Object));
            valInPins[3].Name = "User Data";

            execOutPins.Add(new ExecOutputPin(this));
            nodeType = NodeType.Dialog;
        }
        
        /// <summary>
        /// continue or skip display of dialogue
        /// </summary>
        public void SoftSkip() {
            if (!setupCompleted) return;
            if (Index.y < MaxCharLength && canSkip) {
                pleaseContinue = true;
                Index.y = MaxCharLength;
            } else {
                NextPage();
            }
        }

        public override void Execute(){
            if (!setupCompleted) Setup();

            if (!pleaseContinue) {
                Debug.Log("Outputting Text: "+Index);
                dc.outputTextbox.text = pages[(int)Index.x].Substring(0, (int)Index.y);
                
                Index.y += speed;
                src.Play();

                if(Index.y > MaxCharLength) {
                    pleaseContinue = true;
                    // show continue icon 
                    if (dc.continueIndicator)
                        dc.continueIndicator.gameObject.SetActive(true);
                }
            } else if (pleaseContinue && autoSkip) {
                Debug.Log("Auto skipping");
                skipTime += Time.deltaTime;
                if (skipTime >= autoSkipDelay) {
                    skipTime = 0;
                    NextPage();
                }
            }

        }

        /// <summary>
        /// Continue on to the next page of text
        /// </summary>
        private void NextPage() {
            pleaseContinue = false;
            Index = new Vector2(Index.x + 1, 0);
            if (dc.continueIndicator)
                dc.continueIndicator.gameObject.SetActive(false); Index.x++;
            if (Index.x > pages.Count - 1)
                Finalization();
        }

        /// <summary>
        /// Ensure that everything is setup properly for the dialogue window
        /// </summary>
        protected override void Setup() {
            dc = GameObject.FindObjectOfType<DialogueController>();
            TextAudio = new GameObject("Text Audio");
            TextAudio.transform.parent = dc.transform;
            src = Misc.CopyComponent<AudioSource>(dc.GetAudioSrc(), TextAudio);
            src.clip = dc.TextSound;

            pages = new List<string>();
            pages.AddRange(text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));

            string o = "";
            foreach (string s in pages) o += s + "\n";
            Debug.Log(o);

            Index = Vector2.zero;
            Actor actor = null;

            if (valInPins[0].IsConnected) {
                actor = valInPins[0].GetActor();
            }
            
            if (dc.outputHeader) {
                if (showName) {
                    // set the header
                    if (nickname != null) {
                        dc.outputHeader.text = nickname;
                    } else if (actor != null) {
                        dc.outputHeader.text = actor.Name;
                    } else {
                        dc.outputHeader.text = "";
                    }

                    dc.outputHeader.gameObject.SetActive(true);
                } else {
                    // hide name if
                    dc.outputHeader.gameObject.SetActive(false);
                }
            }

            // set the speed from % to character per second (or frame?)
            speed = valInPins[2].GetInt();
            if (speed <= 0) speed = 5;

            // focus on speaker
            if(actor != null) {
                // change this to not be controlled by var?
                if (valInPins[1].IsConnected) {
                    if (valInPins[1].GetBool()) {
                        Camera tmp = Camera.main;
                        if (tmp != null) {
                            camera = tmp.transform;
                            previousFocus = camera.parent;
                            camera.parent = actor.transform;
                        }
                    }
                }
            }

            setupCompleted = true;
        }

        protected override void Finalization() {
            // reset camera
            if (previousFocus != null) {
                camera.parent = previousFocus;
                previousFocus = null;
            }

            Destroy(TextAudio);
            TextAudio = null;

            finished = true;
        }
    }
}

