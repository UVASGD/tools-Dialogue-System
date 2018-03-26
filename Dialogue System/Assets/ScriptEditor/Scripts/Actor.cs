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

    public int ID { get { return gameObject.GetInstanceID(); } }

    public Actor() {
            
    }

    public void Start() {
        dc = FindObjectOfType<DialogueController>();
        if (Script != null) {
           // create runtime instance if necessary
           // Unity might already do this, but if not, do the thing pls
        }
    }

    public void ActivateScript() {
        if(Script!=null) dc.StartDialogue(Script);
    }

}
