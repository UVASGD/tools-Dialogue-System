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
    /// Node that executes a base function
    /// </summary>
    public class ControlNode : NodeBase {

        private ControlType conType;
        /// <summary> Defines which PinTypes can be cast to other which other PinTypes </summary>
        public static Dictionary<string, List<string>> castables;

        public enum ControlType {
            Set, Branch, ForLoop, Cast,  Log, PlaySound, Text, Wait,

            Custom,
            
        }

        /// <summary>
        /// Constructs the pins for a Cast Node
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public void Construct(PinType input, PinType output) {
            base.SetName("Cast");
            conType = ControlType.Cast;
            name = "Cast Node";
            description = "Cast " + input.ToString() + " to " + output.ToString() + ".";
            inPins.Add(new InputPin(this, input));
            outPins.Add(new OutputPin(this, output));
            Resize();

        }

        /// <summary>
        /// Constructs the pins for a generic Control Node
        /// </summary>
        /// <param name="type"></param>
        public void Construct(ControlType type) {
            base.SetName(type.ToString());
            conType = type;
            nodeType = NodeType.Control;

            // create pins
            switch (conType) {
                case ControlType.Branch:
                    inPins.Add(new InputPin(this, PinType.Logic));
                    inPins.Add(new InputPin(this, PinType.Bool));

                    outPins.Add(new OutputPin(this, PinType.Logic));
                    outPins.Add(new OutputPin(this, PinType.Logic));
                    break;
                case ControlType.PlaySound:
                case ControlType.Log:
                    inPins.Add(new InputPin(this, PinType.Logic));
                    inPins.Add(new InputPin(this, PinType.String));
                    break;
                case ControlType.Text:
                    inPins.Add(new InputPin(this, PinType.Logic));
                    inPins.Add(new InputPin(this, PinType.String));

                    outPins.Add(new OutputPin(this, PinType.Logic));
                    nodeType = NodeType.Dialog;
                    break;
            }
            Resize();
        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Control;
            body = new Rect(0, 0, 150, 35*MaxNodes);
        }

#if UNITY_EDITOR
        public override void DrawNode(Event e, Rect viewRect) {
            base.DrawNode(e, viewRect);
            
            DrawPins();
        }
#endif

        /// <summary>
        /// Define castable dictionary
        /// </summary>
        static ControlNode() {
            castables = new Dictionary<string, List<string>>();

            castables.Add(PinType.Float.ToString(), new List<string>(new string[] {
            PinType.Integer.ToString(), PinType.Bool.ToString()}));
            castables.Add(PinType.Integer.ToString(), new List<string>(new string[] {
            PinType.Float.ToString(), PinType.Bool.ToString()}));
            castables.Add(PinType.String.ToString(), new List<string>(new string[] {
            PinType.Float.ToString(), PinType.Integer.ToString(), PinType.Bool.ToString()}));
            castables.Add(PinType.Vector2.ToString(), new List<string>(new string[] {
            PinType.Vector3.ToString(), PinType.Vector4.ToString()}));
            castables.Add(PinType.Vector3.ToString(), new List<string>(new string[] {
                PinType.Vector4.ToString()}));
            ;
        }
    }
}
