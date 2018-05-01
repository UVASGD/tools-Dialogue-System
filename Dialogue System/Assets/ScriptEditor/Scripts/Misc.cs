using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor {
    public static class Misc {

        /// <summary>
        /// Duplicate Component and add it to the destination GameObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static T CopyComponent<T>(T original, GameObject destination) where T : Component {
            System.Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields) {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy as T;
        }

        /// <summary>
        /// Shortens source text to fit within the bounds of the given view
        /// by separating text into words
        /// </summary>
        /// <param name="n"> source text</param>
        /// <param name="width"> width of the view that the text must fit within </param>
        /// <param name="style">Unity GUI style used to calculate width of text </param>
        /// <returns> shortend text appended with "..."</returns>
        public static string MinimalizeWidth(string n, float width, GUIStyle style) {
            if (String.IsNullOrEmpty(n)) return "";
            string[] words = n.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length <= 1) return MinimalizeWidthFull(n, width, style);

            string res = words[0];
            string s="";
            for(int i = 1; i < words.Length; i++) {
                s = res + " " + words[i];
                if (style.CalcSize(new GUIContent(s)).x > width)
                    break;
                res = s;
            }
            
            return MinimalizeWidthFull(res, width, style);
        }

        /// <summary>
        /// Shortens source text to fit within the bounds of the given view;
        /// ignores spaces
        /// </summary>
        /// <param name="n"> source text</param>
        /// <param name="width"> width of the view that the text must fit within </param>
        /// <param name="style">Unity GUI style used to calculate width of text </param>
        /// <returns> shortend text appended with "..."</returns>
        public static string MinimalizeWidthFull(string n, float width, GUIStyle style) {
            string res = "";
            string s; int i = 0;
            
            if (style.CalcSize(new GUIContent(n)).x <= width) 
                return n;

            do {
                s = res + n[i] + "...";
                float w = style.CalcSize(new GUIContent(s)).x;
                if (w > width)
                    break;
                res += n[i];
                i++;
            } while (i < n.Length);
            
            return res+"...";
        }

        public static string NullToString(object o)
        {
            return o != null ? o.ToString() : "null";
        }

        public static string ListToString<T>(List<T> lst)
        {
            if (lst == null) return "null";
            string s = "";
            foreach (T t in lst)
                s += NullToString(t) + ", ";
            return "{" + s + "}";
        }

        public static string ListToStringIDs<T>(List<T> lst) where T : UnityEngine.Object
        {
            if (lst == null) return "null";
            string s = "";
            foreach (T t in lst)
                s += t.GetInstanceID() + ", ";
            return "{" + s + "}";
        }
    }
}
