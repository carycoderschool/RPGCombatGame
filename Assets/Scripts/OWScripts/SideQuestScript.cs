using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideQuestScript : MonoBehaviour
{
    public bool complete;
    public bool done;
    public Item reward;
    public Item requiredItem;
    Item currentItem;
    public int num;
    float size;
    public List<string> QuestLines = new List<string>();
    public List<string> AfterLines = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Check()
    {
       foreach (Item keyItem in GlobalManager.instance.keyInventory)
        {
            if (keyItem.name == requiredItem.name)
            {
                complete = true;
                GlobalManager.instance.keyInventory.Remove(keyItem);
                return;
            } else
            {
                complete = false;
            }
        }  
    }
}
