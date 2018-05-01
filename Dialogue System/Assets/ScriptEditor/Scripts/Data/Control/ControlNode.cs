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
    public class ControlNode : NodeBase {
        
        /// <summary> Defines which PinTypes can be cast to  which other PinTypes </summary>
        public static Dictionary<string, List<string>> castables;
        public static List<ControlType> dialogControls;
        
        public enum ControlType {
            Branch, ForLoop, Sequence, Print, PlaySound, Dialogue, Delay,
            Music, Quest, Choice, Cast, SetDialogueScript, SetSubStart,
            Custom
        }

        public virtual void Construct() { }

        /// <summary> If choice node, adds a choice to the control </summary>
        public override void AddInputPin() {
            if (multiplePins && valInPins.Count < 8) {
                switch (this.GetType().ToString()) {
                    case "ChoiceNode":
                        valInPins.Add(new ValueInputPin(this, VarType.Bool, true));
                        valInPins[valInPins.Count - 1].Name = "Condition " + (valInPins.Count - 1);

                        execOutPins.Add(new ExecOutputPin(this));
                        execOutPins[execOutPins.Count - 1].Name = "Choice " + (execOutPins.Count - 1);
                        break;
                    case "SequenceNode":
                        execOutPins.Add(new ExecOutputPin(this));
                        execOutPins[execOutPins.Count - 1].Name = "Then " + (valInPins.Count - 2);

                        break;
                }
                Resize();
            }
        }

        /// <summary> If choice node, removes last choice from the control.
        /// minimum of 3 value inputs </summary>
        public override void RemovePin() {
            if(multiplePins && valInPins.Count > 2) {
                ValueInputPin iP = valInPins[valInPins.Count - 1];
                valInPins.Remove(iP);
                RemoveConnection(iP);
                ExecOutputPin oP = execOutPins[execOutPins.Count - 1];
                execOutPins.Remove(oP);
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
            VarType.Vector3.ToString(), VarType.Color.ToString()}));
            castables.Add(VarType.Vector3.ToString(), new List<string>(new string[] {
                VarType.Color.ToString()}));
            ;
        }
    }
}
