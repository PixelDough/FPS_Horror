using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    public TriggerVolume triggerVolume;
    public bool isPowered = false;
    public Animator doorAnimator;
    public CanvasGroup credits;

    bool isGoingUp = false;
    float speed = 0f;
    float speedMax = 0.05f;

    bool hasStarted = false;

    FirstPersonCharacterController rider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //isGoingUp = false;
        if (triggerVolume.isTriggered && !hasStarted && isPowered)
        {
            hasStarted = true;
            StartCoroutine(StartGoingUp());
        }

        

        if (isGoingUp)
        {
            //FindObjectOfType<FirstPersonCharacterController>().transform.SetParent(transform);
            GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(0, speed, 0));

            speed = Mathf.Min(speed + 0.001f, speedMax);
        }
        
    }


    IEnumerator StartGoingUp()
    {
        doorAnimator.Play("Close");
        FindObjectOfType<Monster_TV>().alive = false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.0f);
        isGoingUp = true;
        credits.alpha = 1.0f;
        
    }
    
}
