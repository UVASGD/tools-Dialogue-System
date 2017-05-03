using UnityEngine;
using System.Collections;

namespace ScriptEditor.Graph {
    public class StaticMethods {
        /// <summary>
        /// clamps the value to the given range. If negative possible, range will also be negated (e.g, [3,5]=>[-5,-3:3,5])
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="negativePossible"></param>
        /// <returns></returns>
        public static float Clamp(float value, float min = 0, float max = 1, bool negativePossible=false) {
            if( negativePossible ? (value<0) : false) {
                float tmp = min;
                min = -max; max = -tmp;
            }
            
            return value < min ? min : value > max ? max : value;
        }
        public static int Clamp(int value, int min = 0, int max = 1, bool negativePossible = false) {
            if (negativePossible ? (value < 0) : false) {
                int tmp = min;
                min = -max; max = -tmp;
            }
            return value < min ? min : value > max ? max : value;
        }
    }
}
