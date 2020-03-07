using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    objectLists inventory;
    public Transform itemsParent;
    public InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = objectLists.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].FillSlot(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
                
            }
            
        }
    }
}
