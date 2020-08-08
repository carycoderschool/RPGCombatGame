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
        if (chestItem.keyItem == false)
        {
            if (invent.inventory.Count < 15)
            {
                invent.AddItem(chestItem);
            }
        } else if (chestItem.keyItem == true)
        {
            if (invent.keyInventory.Count < 15)
            {
                invent.keyInventory.Add(chestItem);
            }
        }
        
        TextGenerator.StartText(gameObject);
        gameObject.GetComponent<SpriteRenderer>().sprite = openChest;
        
    }
}
