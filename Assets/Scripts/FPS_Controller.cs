using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Controller : MonoBehaviour
{
    public float walkSpeed = 4.0f;
    public float runSpeed = 7.0f;
    public float sensitivity = 2f;
    public Transform headJoint;
    public Camera cam;
    public Light flashlight;

    public Transform target;

    float moveFB;
    float moveLR;

    float rotX;
    float rotY;

    void Start()
    {
        // Turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;

        //player = GetComponent<CharacterController>();

        // TODO Freeze Rigidbody's X and Z rotation from code.
    }

    void FixedUpdate()
    {

        if (!target)
        {
            headJoint.transform.Rotate(0, Mathf.Lerp(headJoint.transform.rotation.y, rotX, 0.5f), 0);

            cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(rotY, 0, 0), 0.5f);
        }
        else
        {
            headJoint.transform.LookAt(target);
            cam.transform.LookAt(target);
        }

        flashlight.transform.rotation = Quaternion.Lerp(flashlight.transform.rotation, cam.transform.rotation, 0.25f);

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


        //if (Input.GetKeyDown("escape"))
        //{
        //    // Turn on the cursor
        //    Cursor.lockState = CursorLockMode.None;
        //}
    }
}
