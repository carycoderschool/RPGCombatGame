using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Item item;
    public baseStats attacker;
    public Color color;
    public battleSystem battle;
    public playerMovement manager;
    public bool used = false;
    Scene currentScene;
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
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
    }
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
        if (currentScene.name == "BattleScene")
        {
            objectLists.instance.Remove(item, battle);
        } else if (currentScene.name == "OverworldScene")
        {
            GlobalManager.instance.Remove(item);
        }
        
    }
    public IEnumerator UseItem()
    {
        if (item != null)
        {
            item.Use(attacker);
            if (currentScene.name == "BattleScene")
            {
                battle.battleText.text = battle.attacker.gameObject.name + " uses the " + item.name;
                battle.itemsParent.gameObject.GetComponent<Image>().enabled = false; foreach (GameObject button in battle.lists.buttons)
                {
                    button.GetComponent<Button>().interactable = false;
                }
                foreach (InventorySlot slot in battle.slots)
                {
                    slot.gameObject.GetComponentInChildren<Button>().interactable = false;
                    slot.removeButton.interactable = false;
                    slot.removeButton.gameObject.GetComponent<Image>().enabled = false;
                    slot.gameObject.GetComponentInChildren<Image>().enabled = false;
                    slot.icon.enabled = false;
                    slot.color.a = 0;
                }
                used = true;
                Remove();
                yield return new WaitForSeconds(2f);
                battle.TurnOrder();
            }
            else if (currentScene.name == "OverworldScene")
            {
                Color colorTextbox = manager.textBox.color;
                Color colorText = manager.textBox.gameObject.GetComponentInChildren<Text>().color;
                colorText.a = 255;
                colorTextbox.a = 225;
                manager.textBox.color = colorTextbox;
                manager.textBox.gameObject.GetComponentInChildren<Text>().color = colorText;
                manager.textBox.GetComponentInChildren<Text>().text = attacker.nameChar += " uses the " + item.name;
                foreach (Button button in manager.itemButtons)
                {
                    button.interactable = false;
                }
                foreach (InventorySlot slot in manager.slots)
                {
                    slot.gameObject.GetComponentInChildren<Button>().interactable = false;
                    slot.removeButton.interactable = false;
                }
                manager.backButton.interactable = false;
                manager.backButton.GetComponent<Image>().enabled = false;
                manager.backButton.GetComponentInChildren<Text>().enabled = false;
                Remove();
                yield return new WaitForSecondsRealtime(2f);
                manager.Items();
                colorText.a = 0;
                colorTextbox.a = 0;
                manager.textBox.color = colorTextbox;
                manager.textBox.gameObject.GetComponentInChildren<Text>().color = colorText;
                foreach(Button button in manager.itemButtons)
                {
                    button.GetComponent<Image>().enabled = false;
                    button.interactable = false;
                    button.GetComponentInChildren<Text>().enabled = false;
                }
            }
        }
    }
    public void UseItemm(baseStats target)
    {
        attacker = target;
        StartCoroutine(UseItem());
    }
}
