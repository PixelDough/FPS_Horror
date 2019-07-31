using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolume : MonoBehaviour
{
    public GameObject targetObject;
    public string targetScriptName;
    public bool toggleWhenInside = true;

    public bool isTriggered = false;

    private void FixedUpdate()
    {
        if (targetObject == null || !targetObject.activeSelf)
        {
            print(name + ": " + targetObject.name + " is null or inactive.");
            isTriggered = (toggleWhenInside ? false : true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetObject != null)
        {
            if (other.gameObject == targetObject)
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

        if (targetScriptName != null)
        {
            if (other.GetComponent(targetScriptName))
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
            if (other.gameObject == targetObject)
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

        if (targetScriptName != null)
        {
            if (other.GetComponent(targetScriptName))
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
