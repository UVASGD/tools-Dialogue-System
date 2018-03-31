using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptEditor.Demo {
    [RequireComponent(typeof(DialogueController))]
    public class DialogueInput : MonoBehaviour {

        DialogueController dc;
        private float buttonCooldown = 0;

        private const float DEFAULT_COOLDOWN = 0.75f;

        // Use this for initialization
        void Start() {
            dc = GetComponent<DialogueController>();
        }

        // Update is called once per frame
        void Update() {
            if (dc.canTakeInput) {
                if (buttonCooldown > 0) buttonCooldown -= Time.deltaTime;
                if (Input.GetButton("Interact") && buttonCooldown <=0) {
                    dc.ContinueDialogue();
                    buttonCooldown = DEFAULT_COOLDOWN;
                }
            }
        }
    }
}