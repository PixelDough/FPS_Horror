using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public TriggerVolume triggerVolume;

    bool isGoingUp = false;
    float speed = 0f;
    float speedMax = 0.05f;

    FirstPersonCharacterController rider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //isGoingUp = false;
        if (triggerVolume.isTriggered)
        {
            isGoingUp = true;
        }

        

        if (isGoingUp)
        {
            //FindObjectOfType<FirstPersonCharacterController>().transform.SetParent(transform);
            GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(0, speed, 0));

            speed = Mathf.Min(speed + 0.001f, speedMax);
        }
        
    }
    
}
