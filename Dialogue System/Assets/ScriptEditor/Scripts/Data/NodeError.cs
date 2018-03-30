
namespace ScriptEditor.Graph {

    /// <summary> information class containing possible compile errors </summary>
    public class NodeError {

        /// <summary> name of the error </summary>
        public string Title {
            get {
                switch (type) {
                    case ErrorType.DependencyCycle:
                        return "Dependency Cycle";
                    case ErrorType.InfiniteLoop:
                        return "Infinite Execution Loop";
                    case ErrorType.NoEnd:
                        return "Start Without End";
                    case ErrorType.NoDefault:
                        return "Missing Default";
                    case ErrorType.ConversionError:
                        return "Conversion Error";
                    //case ErrorType.NotConnected:
                    //    return "Not Connected";
                    default:
                        return "NodeError";
                }
            }
        }
        /// <summary> debug message </summary>
        public string Message {
            get {
                switch (type) {
                    case ErrorType.DependencyCycle:
                        return "Values have been connected in a loop, " +
                            "causing a cycle that cannot be evaluated";
                    case ErrorType.ConversionError:
                        return "Conversion between incompatible types is not " +
                            "possible"; // um, duh?
                    case ErrorType.InfiniteLoop:
                        return "Execution may be caught in an infinite loop!";
                    case ErrorType.NoEnd:
                        return "Script must end somewhere!";
                    case ErrorType.NoDefault:
                        return "Node's default path must be connected!";
                    //case ErrorType.NotConnected:
                    //    return "Node is not connected to anything. Will be ignored.";
                    default:
                        return "Some unknown compile error occured";
                }
            }
        }
        private ErrorType type;

        /// <summary> type of possible compile error </summary>
        public enum ErrorType {
            None, DependencyCycle, ConversionError, InfiniteLoop, NoEnd,
            //NotConnected,
            NoDefault
        }

        public NodeError(ErrorType type) {
            this.type = type;
        }

        public override bool Equals(object obj) {
            NodeError NE = obj as NodeError;
            return (NE == null) ? base.Equals(obj):NE.type == type;
        }
    }
}