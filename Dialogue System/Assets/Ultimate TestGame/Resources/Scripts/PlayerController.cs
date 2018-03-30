using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace ScriptEditor.Demo {
    public class PlayerController : MonoBehaviour {

        private List<Actor> collidingActors;
        [RequiredInHierarchy(typeof(DialogueController))]
        private DialogueController dc;
        private ThirdPersonCharacter Character; // A reference to the ThirdPersonCharacter on the object
        private Transform Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 CamForward;             // The current forward direction of the camera
        private Vector3 Move;
        private bool Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        private Actor currentNPC {
            get {
                return collidingActors.Count > 0 ?
                  collidingActors[collidingActors.Count - 1] :
                  null;
            }
        }

        // Use this for initialization
        void Start() {
            dc = FindObjectOfType<DialogueController>();
            collidingActors = new List<Actor>();

            // get the transform of the main camera
            if (Camera.main != null)  Cam = Camera.main.transform;
            else  Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
                
            Character = GetComponent<ThirdPersonCharacter>();
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetButtonDown("Interact")) {
                if (currentNPC != null)
                    currentNPC.ActivateScript();
            }

            if (!Jump) {
                Jump = CrossPlatformInputManager.GetButtonDown("Jump");
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


        #region Standard Asset Movement 
        private void FixedUpdate() {
            // read inputs
            float h = dc.isPlayerLocked ? 0 : CrossPlatformInputManager.GetAxis("Horizontal");
            float v = dc.isPlayerLocked ? 0 : CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = dc.isPlayerLocked ? false : Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (Cam != null) {
                // calculate camera relative direction to move:
                CamForward = Vector3.Scale(Cam.forward, new Vector3(1, 0, 1)).normalized;
                Move = v * CamForward + h * Cam.right;
            } else {
                // we use world-relative directions in the case of no main camera
                Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            Character.Move(Move, crouch, Jump);
            Jump = false;
        }
        #endregion
    }
}

