using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCharacterController : MonoBehaviour
{
    public float walkSpeed = 4.0f;
    public float runSpeed = 7.0f;
    public float sensitivity = 2f;
    public GameObject headJoint;
    public Transform target;

    public Camera cam;
    public Light flashlight;

    float moveFB;
    float moveLR;

    float rotX;
    float rotY;

    void Start()
    {
        // Turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;

        //cam.SetReplacementShader(shader, "RenderType");
        //player = GetComponent<CharacterController>();

        // TODO Freeze Rigidbody's X and Z rotation from code.
    }

    void Update()
    {
        float finalSpeed = walkSpeed;
        if (Input.GetButton("Run")) { finalSpeed = runSpeed; }

        moveFB = Input.GetAxis("Vertical") * finalSpeed;
        moveLR = Input.GetAxis("Horizontal") * finalSpeed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -80f, 80f);

        Vector3 movement = new Vector3(moveLR, 0, moveFB);

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

        flashlight.transform.rotation = Quaternion.Lerp(flashlight.transform.rotation, cam.transform.rotation, 0.25f);

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

        transform.Translate(desiredMoveDirection * Time.deltaTime);


        //target = null;
        Collider[] overlap = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider c in overlap)
        {
            if (c.gameObject.GetComponent<Monster_TV>())
            {
                c.gameObject.GetComponent<Monster_TV>().alive = true;
                target = c.transform;
            }
        }


        if (Input.GetKeyDown("escape"))
        {
            // Turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
