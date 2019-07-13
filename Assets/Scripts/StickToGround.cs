using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToGround : MonoBehaviour
{
    public Transform transformToMove;

    Transform standingOn;
    Vector3 standingOnLast;
    

    private void FixedUpdate()
    {
        if (standingOn)
        {
            transformToMove.position += (standingOn.position - standingOnLast);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovingPlatform>())
        {
            standingOn = other.transform;
            standingOnLast = standingOn.position;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform == standingOn)
        {
            transformToMove.position += (other.transform.position - standingOnLast);

            standingOnLast = other.transform.position;
        }
    }
}
