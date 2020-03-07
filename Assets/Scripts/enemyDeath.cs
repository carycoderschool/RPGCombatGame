using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDeath : MonoBehaviour
{
    public baseStats enemy;
    objectLists list;
    public battleSystem b;
    // Start is called before the first frame update
    void Start()
    {
        list = objectLists.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.HP <= 0)
        {
            list.enemies.Remove(gameObject);
            
            Destroy(gameObject);
        }
    }
}
