using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Demo.EditorScripts{
	[CustomEditor(typeof(MyClass))]
	[CanEditMultipleObjects]
	public class MyClassE : Editor {
		SerializedProperty serProp;
		SerializedProperty uniqueProp;
		GUIContent gcVolIcon;
		
		void OnEnable(){
			uniqueProp = serializedObject.FindProperty("uniqueness
			gcVolIcon = new GUIContent((Texture2D) AssetDatabase.LoadAssetAtPath("Assets/Editor/vol_icon.png",
			typeof(Texture2D)), "Master Volume");
		}
		
		// Loops through all exposed, Serialized fields for the target class
		public void override void OnInspectorGUI(){
			serializedObject.GetIterator();
			serProp.NextVisibile(true);
			
			do{
				if(serProp.name == "masterVolume"){
					EditorGuiLayout.Slider(serProp, 0, 1f, gcVolIcon);
				} else if(!serProp.hasMultipleDifferentValues)
					EditorGuiLayout.PropertyField(serProp);
			} while(serProp.NextVisibile(false));
			
			uniqueProp.intValue = Mathf.Max(0, uniqueProp.intValue);
			serializedObject.ApplyModifiedProperties();
		}
	}
	
	public class MyClass : Monobehavior {
		public int uniqueness;
		public float masterVolume;
		
		[Serializable] private Vector2 MotionSrc; 
	}
}