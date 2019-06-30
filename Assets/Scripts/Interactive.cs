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
        TAPE,
        KEYCARD
    }
    

    public Verbs verb = Verbs.NONE;
    public Items item = Items.NONE;
    public string itemName = "???";

    private UIGameManager uiGame;


    // Start is called before the first frame update
    public virtual void Start()
    {
        uiGame = FindObjectOfType<UIGameManager>();
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
        //Destroy(this.gameObject, 0.01f);
        return item;
    }
}
