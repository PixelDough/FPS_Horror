using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyButton : Interactive
{
    public bool isOpen = false;
    public bool hasBeenPressed = false;

    public PowerSwitch powerSwitch;
    public AudioClip errorSound;

    private Animator animator;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }

    override public void LookAt()
    {
        base.LookAt();

    }

    override public void Interact()
    {
        base.Interact();

        if (isOpen)
        {
            animator.Play("ButtonPress", 0, 0f);
            if (powerSwitch.isOn)
            {
                hasBeenPressed = true;
            }
            else
            {
                AudioManager.PlaySound(errorSound);
            }
        }
        else
        {
            isOpen = true;
            animator.Play("ButtonOpen", 0, 0f);
        }

    }
}
