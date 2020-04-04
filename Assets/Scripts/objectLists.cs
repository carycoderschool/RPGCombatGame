using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("messed up");
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject charac in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (charac.GetComponent<baseStats>().character != null)
            {
                chars.Add(charac);
            }
            
        }
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            if (enemy.GetComponent<baseStats>().character != null)
            {
                enemies.Add(enemy);
            }
            
        }
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("button"))
        {
            
            buttons.Add(button);
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
        
        battle.TurnOrder();
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
