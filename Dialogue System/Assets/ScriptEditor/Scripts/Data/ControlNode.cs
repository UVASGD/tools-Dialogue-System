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
        public static List<ControlType> dialogControls;

        public List<string> options;

        public enum ControlType {
            Set, Branch, ForLoop, Sequence, Print, PlaySound, Dialogue, Delay,
            Music, Quest, Choice, Cast,
            Custom
        }

        public ControlType SubType() { return conType; }

        /// <summary>
        /// Constructs the pins for a Cast Node
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public void Construct(VarType input, VarType output) {
            base.SetName("Cast");
            conType = ControlType.Cast;
            name = "Cast Node";
            description = "Casts " + input.ToString() + " to " + output.ToString() + ".";
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

            // set name and description of node
            switch (conType) {
                case ControlType.Branch:
                    name = "Branch";
                    description = "Splits execution based on the value of the condition.";
                    break;
                case ControlType.PlaySound:
                    name = "Play Sound at Location";
                    description = "";
                    break;
                case ControlType.Music:
                    name = "Play Music";
                    description = "Sets the bgm of the scene";
                    break;
                case ControlType.Print:
                    name = "Print Text";
                    description = "Prints text to a console";
                    break;
                case ControlType.Dialogue:
                    name = "Show Dialogue";
                    description = "Displays a conversation dialogue to a dialogue box based on the conversant.";
                    break;
                case ControlType.Choice:
                    name = "Choice";
                    description = "Splits execution based on an on-screen decision.";
                    break;
                case ControlType.Quest:
                    name = "Quest";
                    description = "Defines a quest and shit";
                    break;
                case ControlType.Set:
                    name = "Set";
                    description = "Does a thing... as to what, I'm not yet sure";
                    break;
                case ControlType.ForLoop:
                    name = "For Loop";
                    description = "Loops through execution based on index, from start to finish.";
                    break;
                case ControlType.Sequence:
                    name = "Sequence";
                    description = "Splits execution based on index";
                    break;
                case ControlType.Delay:
                    name = "Delay";
                    description = "Delays execution for a given number of seconds";
                    break;
            }

            // create pins
            switch (conType) {
                case ControlType.Delay:
                    inPins.Add(new InputPin(this, VarType.Exec));
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins[1].Name = "Duration";
                    inPins[1].Default = 1.5f;

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    break;
                case ControlType.Branch:
                    inPins.Add(new InputPin(this, VarType.Exec));
                    inPins.Add(new InputPin(this, VarType.Bool));
                    inPins[1].Name = "Condition";
                    inPins[1].Default = true;

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    outPins.Add(new OutputPin(this, VarType.Exec));
                    outPins[0].Name = "True";
                    outPins[1].Name = "False";
                    break;
                case ControlType.PlaySound:
                    inPins.Add(new InputPin(this, VarType.Exec));
                    inPins.Add(new InputPin(this, VarType.String));
                    inPins.Add(new InputPin(this, VarType.Bool));
                    inPins.Add(new InputPin(this, VarType.Vector3));
                    inPins[1].Name = "Path to Sound";
                    inPins[1].Description = "Path to what sound to play. (e.g. Assets/Resources/Sounds/Jump)";
                    inPins[1].Default = "Assets/";
                    inPins[2].Name = "Loop";
                    inPins[2].Default = false;
                    inPins[3].Name = "Location";

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    nodeType = NodeType.Dialog;
                    break;
                case ControlType.Music:
                    inPins.Add(new InputPin(this, VarType.Exec));
                    inPins.Add(new InputPin(this, VarType.String));
                    inPins.Add(new InputPin(this, VarType.Bool));
                    inPins.Add(new InputPin(this, VarType.Vector3));
                    inPins.Add(new InputPin(this, VarType.Float));
                    inPins[1].Name = "Path to Song";
                    inPins[1].Description = "Path to what song to play. (e.g. Assets/Resources/Music/Main Theme)";
                    inPins[1].Default = "Assets/";
                    inPins[2].Name = "Loop";
                    inPins[2].Default = false;
                    inPins[3].Name = "Location";
                    inPins[3].Description = "The location to play the song in World Coordinates";
                    inPins[4].Name = "Range";
                    inPins[4].Description = "The maximum distance the song can be heard";
                    inPins[4].Default = 100;

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    break;
                case ControlType.Print:
                    inPins.Add(new InputPin(this, VarType.Exec));
                    inPins.Add(new InputPin(this, VarType.String));
                    inPins.Add(new InputPin(this, VarType.Bool));
                    inPins.Add(new InputPin(this, VarType.Bool));
                    inPins[1].Name = "In String";
                    inPins[1].Default = "Hello World";
                    inPins[2].Name = "Print to Screen";
                    inPins[3].Name = "Print to Console";
                    inPins[3].Default = true;

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    break;
                case ControlType.Dialogue:
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
                    break;
                case ControlType.Choice:
                    multiplePins = true;
                    inPins.Add(new InputPin(this, VarType.Exec));
                    inPins.Add(new InputPin(this, VarType.Bool));
                    inPins.Add(new InputPin(this, VarType.Bool));
                    inPins[1].Name = "Condition 1";
                    inPins[1].Default = true;
                    inPins[2].Name = "Condition 2";
                    inPins[2].Default = true;

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    outPins.Add(new OutputPin(this, VarType.Exec));
                    outPins.Add(new OutputPin(this, VarType.Exec));
                    outPins[0].Name = "Default";
                    outPins[0].Description = "What happens if no choice is available or selected.";
                    outPins[1].Name = "Choice 1";
                    outPins[1].Name = "Choice 2";
                    nodeType = NodeType.Dialog;
                    break;
                case ControlType.Set:
                    inPins.Add(new InputPin(this, VarType.Exec));

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    break;
                case ControlType.ForLoop:
                    inPins.Add(new InputPin(this, VarType.Exec));
                    inPins.Add(new InputPin(this, VarType.Integer));
                    inPins.Add(new InputPin(this, VarType.Integer));
                    inPins[1].Name = "First";
                    inPins[1].Description = "Initial value of the index";
                    inPins[1].Default = 0;
                    inPins[2].Name = "Last";
                    inPins[2].Description = "Final value of the index";
                    inPins[2].Default = 0;

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    outPins.Add(new OutputPin(this, VarType.Integer));
                    outPins.Add(new OutputPin(this, VarType.Exec));
                    outPins[0].Name = "Loop Body";
                    outPins[0].Description = "The sequence of actions to execute during the loop";
                    outPins[1].Name = "Index";
                    outPins[1].Description = "The current location of the loop";
                    outPins[2].Name = "Completed";
                    outPins[2].Description = "The execution path when the loop has finished (i.e. Index>=Last)";
                    break;
                case ControlType.Sequence:
                    multiplePins = true;
                    inPins.Add(new InputPin(this, VarType.Exec));
                    inPins.Add(new InputPin(this, VarType.Integer));
                    inPins[1].Name = "Index";
                    inPins[1].Description = "The index at which to split execution";

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    outPins.Add(new OutputPin(this, VarType.Exec));
                    outPins[1].Name = "Then 1";
                    outPins[1].Name = "Then 2";
                    break;
                case ControlType.Quest:
                    inPins.Add(new InputPin(this, VarType.Exec));

                    outPins.Add(new OutputPin(this, VarType.Exec));
                    break;
            }
            Resize();
        }

        /// <summary> If choice node, adds a choice to the control </summary>
        public override void AddInputPin() {
            if (multiplePins && inPins.Count < 9) {
                switch (conType) {
                    case ControlType.Choice:
                        inPins.Add(new InputPin(this, VarType.Bool));
                        inPins[inPins.Count - 1].Name = "Condition " + (inPins.Count - 2);
                        inPins[inPins.Count - 1].Default = true;

                        outPins.Add(new OutputPin(this, VarType.Exec));
                        outPins[outPins.Count - 1].Name = "Choice " + (inPins.Count - 2);
                        break;
                    case ControlType.Sequence:
                        outPins.Add(new OutputPin(this, VarType.Exec));
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

        public override void Execute() {

        }

        public override void Initialize() {
            base.Initialize();
            nodeType = NodeType.Control;
            options = new List<string>();
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
