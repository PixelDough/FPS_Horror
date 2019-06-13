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
    void Start()
    {
        uiGame = FindObjectOfType<UIGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookAt()
    {
        uiGame.SetInteractText(verb + " " + itemName);
    }

    public void Interact()
    {
        print(verb + " " + itemName);
        
    }
}
