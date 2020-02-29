using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Item item;
    // Start is called before the first frame update
    public void FillSlot(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    // Update is called once per frame
    public void ClearSlot()
    {
        item = null;
        icon.enabled = false;
        
        
        
        removeButton.interactable = false;
    }
    public void Remove()
    {
        objectLists.instance.Remove(item);
    }
}
