using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    public Animator handleAnimator;

    private bool m_IsOpen = false;
    private Quaternion m_InitialDoorRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_InitialDoorRotation = transform.rotation; 
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            handleAnimator.Play("Locked");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            handleAnimator.Play("Unlocked");
            m_IsOpen = !m_IsOpen;
        }

        if (m_IsOpen)
        {
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, m_InitialDoorRotation.eulerAngles + new Vector3(0, -90f, 0), 0.05f);
        }
        else
        {
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, m_InitialDoorRotation.eulerAngles, 0.05f);
        }

    }
}
