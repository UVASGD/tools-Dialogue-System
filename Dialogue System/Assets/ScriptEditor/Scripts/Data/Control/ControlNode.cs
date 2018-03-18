using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptEditor.Graph {

    /// <summary>
    /// Node that executes a basic function
    /// </summary>
    public abstract class ControlNode : NodeBase {
        
        /// <summary> Defines which PinTypes can be cast to  which other PinTypes </summary>
        public static Dictionary<string, List<string>> castables;
        public static List<ControlType> dialogControls;
        
        public enum ControlType {
            Branch, ForLoop, Sequence, Print, PlaySound, Dialogue, Delay,
            Music, Quest, Choice, Cast, SetDialogueScript, SetSubStart,
            Custom
        }

        public abstract void Construct();

        /// <summary> If choice node, adds a choice to the control </summary>
        public override void AddInputPin() {
            if (multiplePins && inPins.Count < 9) {
                switch (this.GetType().ToString()) {
                    case "ChoiceNode":
                        inPins.Add(new ValueInputPin(this, VarType.Bool));
                        inPins[inPins.Count - 1].Name = "Condition " + (inPins.Count - 1);
                        inPins[inPins.Count - 1].Default = true;

                        outPins.Add(new EventOutputPin(this));
                        outPins[outPins.Count - 1].Name = "Choice " + (inPins.Count - 1);
                        break;
                    case "SequenceNode":
                        outPins.Add(new EventOutputPin(this));
                        outPins[outPins.Count - 1].Name = "Then " + (inPins.Count - 2);

                        break;
                }
                Resize();
            }
        }

        /// <summary> If choice node, removes last choice from the control </summary>
        public override void RemovePin() {
            if(multiplePins&& inPins.Count > 3) {
                InputPin iP = inPins[inPins.Count - 1];
                inPins.Remove(iP);
                RemoveConnection(iP);
                OutputPin oP = outPins[outPins.Count - 1];
                outPins.Remove(oP);
                RemoveConnection(oP);
                Resize();
            }
        }

        // public override void Execute() {}

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Control;
            //options = new List<string>();
            body = new Rect(0, 0, 150, 35*MaxNodes);
        }

#if UNITY_EDITOR
        public override void DrawNode(Event e, Rect viewRect) {
            base.DrawNode(e, viewRect);
            
            DrawPins();
        }
#endif

        /// <summary>
        /// Define castable dictionary and dialogue controls
        /// </summary>
        static ControlNode() {
            dialogControls = new List<ControlType>();
            dialogControls.Add(ControlType.Dialogue);
            dialogControls.Add(ControlType.Choice);
            dialogControls.Add(ControlType.Quest);
            dialogControls.Add(ControlType.PlaySound);

            castables = new Dictionary<string, List<string>>();

            castables.Add(VarType.Float.ToString(), new List<string>(new string[] {
            VarType.Integer.ToString(), VarType.Bool.ToString()}));
            castables.Add(VarType.Integer.ToString(), new List<string>(new string[] {
            VarType.Float.ToString(), VarType.Bool.ToString()}));
            castables.Add(VarType.String.ToString(), new List<string>(new string[] {
            VarType.Float.ToString(), VarType.Integer.ToString(), VarType.Bool.ToString()}));
            castables.Add(VarType.Vector2.ToString(), new List<string>(new string[] {
            VarType.Vector3.ToString(), VarType.Vector4.ToString()}));
            castables.Add(VarType.Vector3.ToString(), new List<string>(new string[] {
                VarType.Vector4.ToString()}));
            ;
        }
    }
}
