using ScriptEditor;
using ScriptEditor.Graph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public NodeGraph Script;
    public bool triggered;
    public bool retriggerable;

    [RequiredInHierarchy(typeof(DialogueController))]
    private DialogueController dc;

	// Use this for initialization
	void Start () {
        dc = FindObjectOfType<DialogueController>();
	}

    private void OnTriggerEnter(Collider collider) {
        if (!retriggerable && triggered) return;
        GameObject go = collider.gameObject;
        if (go.CompareTag("Player")) {
            dc.StartDialogue(Script);
            triggered = true;
        }   
    }
}
