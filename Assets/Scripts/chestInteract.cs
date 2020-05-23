using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestInteract : interactScript
{
    public Item chestItem;
    public Sprite openChest;
    public Sprite closedChest;
    

    
    public override void Interact()
    {
        GlobalManager invent;
        invent = GlobalManager.instance;
        interactable = true;
        player.GetComponent<playerMovement>().mode = "chestPause";
        if (invent.inventory.Count < 15)
        {
            invent.inventory.Add(chestItem);
        }
        
        gameObject.GetComponent<SpriteRenderer>().sprite = openChest;
        TextGenerator.StartText(gameObject);
    }
}
