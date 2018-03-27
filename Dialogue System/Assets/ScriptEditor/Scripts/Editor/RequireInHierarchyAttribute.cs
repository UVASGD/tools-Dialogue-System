using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

namespace ScriptEditor {
    [CustomPropertyDrawer(typeof(RequiredInHierarchyAttribute))]
    public class RequireInHierarchyAttribute : PropertyDrawer {

        static UnityEngine.Object FindRequiredComponent(RequiredInHierarchyAttribute reqAttr) {
            return GameObject.FindObjectOfType(reqAttr.requiredType);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            RequiredInHierarchyAttribute reqAttr = attribute as RequiredInHierarchyAttribute;
            if (!FindRequiredComponent(reqAttr)) {
                position.height = EditorGUIUtility.singleLineHeight * 2.0f;
                EditorGUI.HelpBox(position, string.Format("Can't find a {0} component in the scene, but it is required.", 
                    reqAttr.requiredType.Name), MessageType.Error);
                position.y += EditorGUIUtility.singleLineHeight * 2.0f + 2;
                position.height = EditorGUIUtility.singleLineHeight;
            }
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (!FindRequiredComponent(attribute as RequiredInHierarchyAttribute)) {
                return EditorGUIUtility.singleLineHeight * 3.0f ;
            } return base.GetPropertyHeight(property, label);

        }
    }
}
