using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitch : Interactive
{
    public GameObject lever;
    public bool isOn = false;
    public GameObject lights;

    private Vector3 m_InitialLeverRotation;

    public override void Start()
    {
        base.Start();
        m_InitialLeverRotation = lever.transform.rotation.eulerAngles;

        SetLights(isOn);
    }

    public override void Update()
    {
        base.Update();
        float dir = (isOn == true) ? 1f : 0f;
        lever.transform.rotation = Quaternion.Lerp(lever.transform.rotation, Quaternion.Euler(m_InitialLeverRotation.x + (180f * dir), lever.transform.rotation.eulerAngles.y, lever.transform.rotation.eulerAngles.z), 10f * Time.deltaTime);
        //print(m_InitialLeverRotation.eulerAngles.x + (180f * dir));
    }

    override public void LookAt()
    {
        base.LookAt();

        //<Interactive>().LookAt();
    }

    override public void Interact()
    {
        base.Interact();

        //print("Interacted with " + transform.gameObject.name);
        isOn = !isOn;
        //lights.SetActive(isOn);
        SetLights(isOn);
    }

    private void SetLights(bool isOn)
    {
        Light[] lightComponents = lights.GetComponentsInChildren<Light>(true);
        foreach (Light l in lightComponents)
        {
            l.gameObject.SetActive(isOn);
        }
    }
}
