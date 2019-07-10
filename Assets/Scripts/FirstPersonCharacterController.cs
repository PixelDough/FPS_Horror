using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCharacterController : MonoBehaviour
{
    public float walkSpeed = 4.0f;
    public float runSpeed = 7.0f;
    public float sensitivity = 2f;
    [Range(2f, 4f)]
    public float interactReach = 3f;
    public GameObject headJoint;
    public Transform target;

    public Camera cam;
    public Light flashlight;
    public bool hasFlashlight = false;
    public AudioClip flashlightSoundOn;
    public AudioClip flashlightSoundOff;

    public List<Interactive.Items> items;
    

    float moveFB;
    float moveLR;

    float rotX;
    float rotY;

    float ySpeed = 0f;

    private UIGameManager m_uiGame;
    private AudioManager audioManager;

    private Transform standingOn;

    bool flashlightIsOn = true;

    private Interactive.Items item;

    void Start()
    {
        // Turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //cam.SetReplacementShader(shader, "RenderType");
        //player = GetComponent<CharacterController>();

        m_uiGame = FindObjectOfType<UIGameManager>();
        audioManager = FindObjectOfType<AudioManager>();

        // TODO Freeze Rigidbody's X and Z rotation from code.
    }

    private void FixedUpdate()
    {
        #region Movement
        float finalSpeed = walkSpeed;
        if (Input.GetButton("Run")) { finalSpeed = runSpeed; }

        moveFB = Input.GetAxis("Vertical") * finalSpeed * Time.deltaTime;
        moveLR = Input.GetAxis("Horizontal") * finalSpeed * Time.deltaTime;

        

        Vector3 movement = new Vector3(moveLR, 0, moveFB);

        
        var forward = headJoint.transform.forward;
        var right = headJoint.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        var desiredMoveDirection = forward * moveFB + right * moveLR;

        //var moveX = Mathf.Sin(Camera.main.transform.rotation.eulerAngles.y * (Mathf.PI/180));
        //var moveY = Mathf.Cos(Camera.main.transform.rotation.eulerAngles.y * (Mathf.PI / 180));
        //print(Camera.main.transform.rotation.eulerAngles.y);
        //var desiredMoveDirection = new Vector3(moveX, 0, moveY);
        //desiredMoveDirection.z *= moveLR;


        //this.GetComponent<Rigidbody>().MovePosition(transform.position + (desiredMoveDirection * Time.deltaTime));


        if (!this.GetComponent<CharacterController>())
        {
            this.GetComponent<Rigidbody>().MovePosition(transform.position + (desiredMoveDirection * Time.deltaTime));
        }
        else
        {
            ySpeed -= 100 * Time.deltaTime;
            desiredMoveDirection.y += ySpeed;
            this.GetComponent<CharacterController>().Move((desiredMoveDirection * Time.deltaTime));
            if (GetComponent<CharacterController>().isGrounded)
            {
                print("Grounded");
                ySpeed = 0f;
            }
            else
            {
                print("Not grounded");
                
            }
            
        }
        #endregion

        

    }

    private void LateUpdate()
    {
        #region Camera

        rotX = Input.GetAxis("Mouse X") * GameManager.Instance.lookSensitivity * Time.deltaTime;
        rotY -= Input.GetAxis("Mouse Y") * GameManager.Instance.lookSensitivity * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, -80f, 80f);

        if (!target)
        {
            headJoint.transform.Rotate(0, rotX, 0);

            cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(rotY, 0, 0), 0.5f);
        }
        else
        {
            headJoint.transform.LookAt(target);
            cam.transform.LookAt(target);
        }

        flashlight.transform.rotation = Quaternion.Lerp(flashlight.transform.rotation, cam.transform.rotation, 0.1f);

        #endregion
    }

    private void Update()
    {
        if (PauseManager.isPaused) { return; }

        #region Raycasting for Interaction

        // Raycasting for interaction
        m_uiGame.SetInteractText("");

        RaycastHit[] allHits;
        int layerMask = 1 << gameObject.layer;
        layerMask = LayerMask.NameToLayer("Interactable");
        layerMask = ~layerMask;

        allHits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, interactReach, layerMask, QueryTriggerInteraction.Collide);

        foreach (var hit in allHits)
        {
            if (hit.transform.gameObject.layer != layerMask)
            {
                var interacted = hit.transform.gameObject.GetComponent(typeof(Interactive)) as Interactive;
                if (interacted != null)
                {
                    interacted.LookAt();

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interacted.Interact();
                        if (interacted.verb == Interactive.Verbs.GET)
                        {
                            items.Add(interacted.GetItem());
                        }
                    }
                }
            }
        }
        
        #endregion


        //target = null;
        Collider[] overlap = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider c in overlap)
        {
            if (c.gameObject.GetComponent<Monster_TV>())
            {
                c.gameObject.GetComponent<Monster_TV>().alive = true;
                target = c.GetComponent<Monster_TV>().screenTransform;
            }
        }

        hasFlashlight = false;
        // Inventory check
        foreach (Interactive.Items i in items)
        {
            if(i == Interactive.Items.FLASHLIGHT)
            {
                hasFlashlight = true;
            }
        }

        if (hasFlashlight)
        {
            if (Input.GetKeyDown("f"))
            {
                // Toggle the flashlight
                flashlightIsOn = !flashlightIsOn;
                if (flashlightIsOn)
                {
                    AudioManager.PlaySound(flashlightSoundOn);
                }
                else
                {
                    AudioManager.PlaySound(flashlightSoundOff);
                }
            }
            flashlight.enabled = flashlightIsOn;
        }
        else
        {
            flashlight.enabled = false;
        }
    }


    public bool HasItem(Interactive.Items item)
    {

        foreach (Interactive.Items i in items)
        {
            if (i == item)
            {
                return true;
            }
        }

        return false;

    }
    

}
