using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitch : MonoBehaviour, IInteractable
{
    public GameObject lever;
    public bool isOn = false;
    public GameObject lights;

    private Vector3 m_InitialLeverRotation;

    private void Start()
    {
        m_InitialLeverRotation = lever.transform.rotation.eulerAngles;

        lights.SetActive(isOn);
    }

    private void Update()
    {
        float dir = (isOn == true) ? 1f : 0f;
        lever.transform.rotation = Quaternion.Lerp(lever.transform.rotation, Quaternion.Euler(m_InitialLeverRotation.x + (180f * dir), lever.transform.rotation.eulerAngles.y, lever.transform.rotation.eulerAngles.z), 0.2f);
        //print(m_InitialLeverRotation.eulerAngles.x + (180f * dir));
    }

    public void LookAt()
    {
        GetComponent<Interactive>().LookAt();
    }

    public void Interact()
    {
        print("Interacted with " + transform.gameObject.name);
        isOn = !isOn;
        lights.SetActive(isOn);
    }
}
