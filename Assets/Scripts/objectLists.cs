using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class objectLists : MonoBehaviour
{
    public List<GameObject> chars = new List<GameObject>();
    public List<Item> items = new List<Item>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> buttons = new List<GameObject>();
    public static objectLists instance;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    //singleton
  
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("messed up");
        }
        instance = this;
        foreach (GameObject charac in GameObject.FindGameObjectsWithTag("Player"))
        {
            //if (charac.GetComponent<baseStats>().character != null)
            //{
                chars.Add(charac);
            //}

        }
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            //if (enemy.GetComponent<baseStats>().character != null)
            //{
                enemies.Add(enemy);
            //}

        }
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("button"))
        {

            buttons.Add(button);
        }
        chars = chars.OrderBy(c => c.GetComponent<baseStats>().importance).ToList();
        enemies = enemies.OrderBy(c => c.GetComponent<baseStats>().importance).ToList();
        for (int i = 0; i < GlobalManager.instance.encounter.Count; i++)
        {
            enemies[i].GetComponent<baseStats>().character = GlobalManager.instance.encounter[i];
        }
        for (int i = 0; i < GlobalManager.instance.currentParty.Count; i++)
        {
            chars[i].GetComponent<baseStats>().character = GlobalManager.instance.currentParty[i].state;
        }
       
    }
    private void Start()
    {
       
        for (int i = 0; i < GlobalManager.instance.inventory.Count; i++)
        {
            AddItem(GlobalManager.instance.inventory[i]);
        }
    }
    // Update is called once per frame
    public void AddItem(Item item)
    {
        items.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    public void Remove(Item item, battleSystem battle)
    {
        
            items.Remove(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            
        
        
    }
}
