using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectLists : MonoBehaviour
{
    public GameObject[] chars;
    public List<Item> items = new List<Item>();
    public GameObject[] enemies;
    public GameObject[] buttons;
    public static objectLists instance;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
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
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        chars = GameObject.FindGameObjectsWithTag("Player");
        
        buttons = GameObject.FindGameObjectsWithTag("button");
    }

    // Update is called once per frame
    public void AddItem(Item item)
    {
        items.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
