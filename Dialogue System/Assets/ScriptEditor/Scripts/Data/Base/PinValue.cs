using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScriptEditor.Graph
{
    [Serializable]
    public struct PinValue
    {
        [SerializeField] private bool boolVal;
        [SerializeField] private int intVal;
        [SerializeField] private float floatVal;
        [SerializeField] private string stringVal;
        [SerializeField] private Vector2 vector2Val;
        [SerializeField] private Vector3 vector3Val;
        [SerializeField] private Color colorVal;
        [SerializeField] private Actor actorVal;

        public VarType Type;

        public object Value {
            get {
                switch (Type) {
                    case VarType.Bool: return boolVal;
                    case VarType.Integer:  return intVal;
                    case VarType.Float: return floatVal;
                    case VarType.String: return stringVal;
                    case VarType.Vector2:  return vector2Val;
                    case VarType.Vector3:  return vector3Val;
                    case VarType.Color: return colorVal;
                    case VarType.Actor: return actorVal;
                    default: return null;
                }
            }
            set {
                switch (Type) {
                    case VarType.Bool: boolVal = (bool)value; break;
                    case VarType.Integer:  intVal = (int)value; break;
                    case VarType.Float: floatVal = (float)value; break;
                    case VarType.String: stringVal = (string)value; break;
                    case VarType.Vector2: vector2Val = (Vector2)value; break;
                    case VarType.Vector3: vector3Val = (Vector3)value; break;
                    case VarType.Color: colorVal = (Color)value; break;
                    case VarType.Actor: actorVal = (Actor)value; break;
                    default:  break;
                }
            }
        }

        public PinValue(VarType type, object value = null){
            Type = type;
            boolVal = false;
            intVal = 0;
            floatVal = 0f;
            stringVal = "";
            vector2Val = new Vector2();
            vector3Val = new Vector3();
            colorVal = Color.white;
            actorVal = null;
            if (value != null)
                Value = value;
        }
    }
}
