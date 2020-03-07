using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Item item;
    public baseStats attacker;
    public Color color;
    public battleSystem battle;
    // Start is called before the first frame update
    public void FillSlot(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        
        color = icon.color;
        color.a = 255;
        icon.color = color;
        removeButton.interactable = true;
    }

    // Update is called once per frame
    public void ClearSlot()
    {
        item = null;
        icon.enabled = false;
        color = icon.color;
        color.a = 0;
        icon.color = color;
        removeButton.interactable = false;
    }
    public void Remove()
    {
        objectLists.instance.Remove(item, battle);
    }
    public void UseItem()
    {
        if (item != null)
        {
            item.Use(attacker);
            Remove();
        }
    }
}
