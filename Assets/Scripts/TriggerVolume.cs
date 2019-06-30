using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolume : MonoBehaviour
{
    public GameObject targetObject;
    public bool toggleWhenInside = true;

    public bool isTriggered = false;

    private void Update()
    {
        if (targetObject == null || !targetObject.activeInHierarchy)
        {
            isTriggered = (toggleWhenInside ? false : true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetObject != null)
        {
            if (other.name == targetObject.name)
            {
                if (toggleWhenInside)
                {
                    isTriggered = true;
                }
                else
                {
                    isTriggered = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetObject != null)
        {
            if (other.name == targetObject.name)
            {
                if (!toggleWhenInside)
                {
                    isTriggered = true;
                }
                else
                {
                    isTriggered = false;
                }
            }
        }
    }
}
