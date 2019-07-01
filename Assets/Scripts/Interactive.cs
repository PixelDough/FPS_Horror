using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour, IInteractable
{
    public enum Verbs
    {
        NONE,
        GET,
        OPEN,
        USE
    }
    public enum Items
    {
        NONE,
        FLASHLIGHT,
        TAPE,
        KEYCARD
    }
    

    public Verbs verb = Verbs.NONE;
    public Items item = Items.NONE;
    public string itemName = "???";

    public AudioClip pickupSound;

    private UIGameManager uiGame;
    private AudioManager audioManager;


    // Start is called before the first frame update
    public virtual void Start()
    {
        uiGame = FindObjectOfType<UIGameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    virtual public void LookAt()
    {
        //uiGame = FindObjectOfType<UIGameManager>();
        uiGame.SetInteractText(verb + " " + itemName);
    }

    virtual public void Interact()
    {
        print(verb + " " + itemName);

        switch (verb)
        {
            case Verbs.GET:
                GetItem();
                break;
        }
    }

    public Items GetItem()
    {
        this.gameObject.SetActive(false);
        audioManager.PlaySound(pickupSound);
        return item;
    }
}
