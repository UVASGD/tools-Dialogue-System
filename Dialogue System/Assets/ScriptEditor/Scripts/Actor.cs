using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ScriptEditor;
using ScriptEditor.Graph;


[DisallowMultipleComponent]
public class Actor : MonoBehaviour {
    public String Name;
    public NodeGraph Script;
    // public String Condition;

    private DialogueController dc;

    [SerializeField] int ID {
        get { return gameObject.GetInstanceID(); }
    }

    public Actor() {
            
    }

    public void Start() {
        dc = FindObjectOfType<DialogueController>();
        if (Script != null) {
            Script = ScriptableObject.CreateInstance<NodeGraph>();
        }
    }

    public void ActivateScript() {
        Debug.Log("I be ate: " +Script);
        if(Script!=null) dc.StartDialogue(Script);
    }

}
