using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectLists : MonoBehaviour
{
    public GameObject[] chars;
    public GameObject[] items;
    public GameObject[] enemies;
    public GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        chars = GameObject.FindGameObjectsWithTag("Player");
        items = GameObject.FindGameObjectsWithTag("item");
        buttons = GameObject.FindGameObjectsWithTag("button");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
