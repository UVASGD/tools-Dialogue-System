using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private List<Actor> collidingActors;
    [SerializeField] Actor currentNPC {
        get { return collidingActors.Count>0?
                collidingActors[collidingActors.Count-1]:
                null; }
    }

    // Use this for initialization
    void Start () {
        collidingActors = new List<Actor>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Interact")) {
            Debug.Log("Eat me");
            if(currentNPC!=null)
                currentNPC.ActivateScript();
        }
	}

    private void OnTriggerEnter(Collider collider) {
        Actor actor = getActor(collider);
        if (actor != null)
            collidingActors.Add(actor);
    }

    private void OnTriggerExit(Collider collider) {
        Actor actor = getActor(collider);
        if (actor != null)
            collidingActors.Remove(actor);
    }

    private Actor getActor(Collider collider) {
        GameObject go = collider.gameObject;
        return go.GetComponent<Actor>();
    }
}
