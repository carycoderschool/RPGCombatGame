using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{
    objectLists battleInventory;
    GlobalManager worldInventory;
    public RectTransform itemsParent;
    public InventorySlot[] slots;
    Scene currentScene;
    // Start is called before the first frame update
    void Awake()
    {
        itemsParent = GameObject.FindGameObjectWithTag("inventorySlots").GetComponent<RectTransform>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "BattleScene")
        {
            battleInventory = objectLists.instance;
            //battleInventory.onItemChangedCallback = null;
            battleInventory.onItemChangedCallback = UpdateUI;
        } else if (currentScene.name == "OverworldScene")
        {
            worldInventory = GlobalManager.instance;
            //worldInventory.onItemChangedCallback = null;
            worldInventory.onItemChangedCallback = UpdateUI;
        }
    }
    // Update is called once per frame
    void UpdateUI()
    {
        
        if (currentScene.name == "BattleScene")
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < battleInventory.items.Count)
                {
                    slots[i].FillSlot(battleInventory.items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }

            }
        } else if (currentScene.name == "OverworldScene")
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < worldInventory.inventory.Count)
                {
                    slots[i].FillSlot(worldInventory.inventory[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }

            }
        }
        
    }
}
