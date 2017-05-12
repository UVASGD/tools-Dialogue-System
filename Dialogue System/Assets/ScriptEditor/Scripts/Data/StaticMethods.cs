using UnityEngine;
using System.Collections;

namespace ScriptEditor.Graph {
    /// <summary>
    /// Class containing static methods useful for any occasion
    /// </summary>
    public class StaticMethods {

        /// <summary>
        /// clamps the value to the given range. If negative possible, range will also be negated (e.g, [3,5]=>[-5,-3:3,5])
        /// </summary>
        public static float Clamp(float value, float min = 0, float max = 1, bool negativePossible=false) {
            if( negativePossible ? (value<0) : false) {
                float tmp = min;
                min = -max; max = -tmp;
            }
            
            return value < min ? min : value > max ? max : value;
        }

        /// <summary>
        /// clamps the value to the given range. If negative possible, range will also be negated (e.g, [3,5]=>[-5,-3:3,5])
        /// </summary>
        public static int Clamp(int value, int min = 0, int max = 1, bool negativePossible = false) {
            if (negativePossible ? (value < 0) : false) {
                int tmp = min;
                min = -max; max = -tmp;
            }
            return value < min ? min : value > max ? max : value;
        }
        
        /// <summary>
        /// snaps the value to a multiple of the interval;
        /// </summary>
        public static Vector2 SnapTo(Vector2 value, float interval) {
            return new Vector2(SnapTo(value.x, interval), SnapTo(value.y, interval));
        }
        
        /// <summary>
        /// snaps the value to a multiple of the interval;
        /// </summary>
        public static Vector3 SnapTo(Vector3 value, float interval) {
            return new Vector3(SnapTo(value.x, interval), 
                SnapTo(value.y, interval), SnapTo(value.z, interval));
        }

        /// <summary>
        /// snaps the value to a multiple of the interval;
        /// </summary>
        public static float SnapTo(float value, float interval) {
            return value - (value % interval);
        }
    }
}
