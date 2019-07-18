using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactive
{
    public bool isLocked = false;
    public Animator handleAnimator;
    public Mesh glowMesh;
    public Mesh glowMaterial;
    public Collider doorCollider;
    public AudioClip doorLockedSound;
    public AudioClip doorUnlockedSound;

    public int openDirection = -1;

    public bool m_IsOpen = false;
    private Quaternion m_InitialDoorRotation;
    private UIGameManager m_uiGame;

    public Items itemToUnlock;

    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        m_InitialDoorRotation = transform.rotation;
        m_uiGame = FindObjectOfType<UIGameManager>();
    }

    // Update is called once per frame
    override public void Update()
    {
        base.Update();
        

        float speed = 10f;

        if (m_IsOpen)
        {
            doorCollider.isTrigger = true;
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, m_InitialDoorRotation.eulerAngles + new Vector3(0, 90f, 0), speed * Time.deltaTime);
        }
        else
        {
            doorCollider.isTrigger = false;
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, m_InitialDoorRotation.eulerAngles, speed * Time.deltaTime);
        }
    }

    override public void LookAt()
    {
        base.LookAt();

        string verb = "OPEN";
        if (m_IsOpen)
            verb = "CLOSE";

           m_uiGame.SetInteractText(verb + " Door");
    }

    override public void Interact()
    {
        base.Interact();

        FirstPersonCharacterController player = FindObjectOfType<FirstPersonCharacterController>();

        if (itemToUnlock != Items.NONE)
        {
            isLocked = true;
            if (player.HasItem(itemToUnlock))
            {
                isLocked = false;
            }
        }

        print("Interacted with " + transform.gameObject.name);
        if (isLocked)
        {
            handleAnimator.Play("Locked");
            AudioManager.PlaySound(doorLockedSound);
        }
        else
        {
            handleAnimator.Play("Unlocked");
            AudioManager.PlaySound(doorUnlockedSound);
            m_IsOpen = !m_IsOpen;
        }
    }
}