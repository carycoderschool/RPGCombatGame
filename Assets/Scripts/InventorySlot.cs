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
    public bool used = false;
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
    public IEnumerator UseItem()
    {
        if (item != null)
        {
            item.Use(attacker);
            battle.battleText.text = battle.attacker.gameObject.name + " uses the " + item.name;
            battle.itemsParent.gameObject.GetComponent<Image>().enabled = false;
            foreach (InventorySlot slot in battle.slots)
            {
                slot.gameObject.GetComponentInChildren<Button>().interactable = false;
                slot.removeButton.interactable = false;
                slot.removeButton.gameObject.GetComponent<Image>().enabled = false;

                slot.gameObject.GetComponentInChildren<Image>().enabled = false;

            }
            foreach (InventorySlot slot in battle.slots)
            {
                slot.icon.enabled = false;
                slot.color.a = 0;
            }
            foreach (GameObject button in battle.lists.buttons)
            {
                button.GetComponent<Button>().interactable = false;

            }
            used = true;
            Remove();
            yield return new WaitForSeconds(2f);
            battle.TurnOrder();
        }
    }
    public void UseItemm()
    {
        StartCoroutine(UseItem());
    }
}
