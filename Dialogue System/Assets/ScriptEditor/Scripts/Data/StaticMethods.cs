using UnityEngine;
using System.Collections;

namespace ScriptEditor.Graph {
    public class StaticMethods {
        public static float Clamp(float value, float min = 0, float max = 1) {
            return value < min ? min : value > max ? max : value;
        }
        public static int Clamp(int value, int min = 0, int max = 1) {
            return value < min ? min : value > max ? max : value;
        }
    }
}
