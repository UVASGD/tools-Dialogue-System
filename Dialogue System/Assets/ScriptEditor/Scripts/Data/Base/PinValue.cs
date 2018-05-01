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
            
            if(value!=null)
                switch (Type) {
                    case VarType.Bool: boolVal = (bool)value; break;
                    case VarType.Integer: intVal = (int)value; break;
                    case VarType.Float: floatVal = (float)value; break;
                    case VarType.String: stringVal = (string)value; break;
                    case VarType.Vector2: vector2Val = (Vector2)value; break;
                    case VarType.Vector3: vector3Val = (Vector3)value; break;
                    case VarType.Color: colorVal = (Color)value; break;
                    case VarType.Actor: actorVal = (Actor)value; break;
                    default: break;
                }
        }

        public int GetInt() { return intVal; }
        public bool GetBool() { return boolVal; }
        public float GetFloat() { return floatVal; }
        public string GetString() { return stringVal; }
        public Vector2 GetVector2() { return vector2Val; }
        public Vector3 GetVector3() { return vector3Val; }
        public Color GetColor() { return colorVal; }
        public Actor getActor() { return actorVal; }

        public void SetInt(int val) { intVal = val; }
        public void SetBool(bool val) { boolVal = val; }
        public void SetFloat(float val) { floatVal = val;  }
        public void SetString(string val) { stringVal = val;  }
        public void SetVector2(Vector2 val) { vector2Val = val;  }
        public void SetVector3(Vector3 val) { vector3Val = val; }
        public void SetColor(Color val) { colorVal = val; }
        public void SetActor(Actor val) { actorVal = val; }

    }
}
