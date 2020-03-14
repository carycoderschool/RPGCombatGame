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
    public void Update()
    {
        if (enemy.HP <= 0)
        {
            


            list.enemies.Remove(gameObject);
            b.stats.Remove(gameObject.GetComponent<baseStats>());
            b.sortedStats.Remove(gameObject.GetComponent<baseStats>());
            
            Destroy(gameObject);
        }
    }
    public void AttackSlash()
    {
       Vector3  babb = b.battleTarget.transform.position;
        GameObject slash = Instantiate(b.slashPrefab);
        slash.transform.position = b.battleTarget.transform.position;
    }
}
