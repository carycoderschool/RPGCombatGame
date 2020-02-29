using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    Item item;
    // Start is called before the first frame update
    public void FillSlot(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    // Update is called once per frame
    public void ClearSlot()
    {
        item = null;
        icon = null;
        icon.enabled = false;
    }
}
