using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDeath : MonoBehaviour
{
    public baseStats enemy;
    objectLists list;
    public battleSystem b;
    public bool multi;
    // Start is called before the first frame update
    void Start()
    {
        list = objectLists.instance;
    }

    // Update is called once per frame
    public void Update()
    {
       
    }
    public void Die()
    {
        GetComponent<Animator>().SetBool("dead", true);
    }
    public void AttackSlash()
    {
        if (multi == false)
        {
            Vector3 babb = b.battleTarget.transform.position;
            GameObject sslash = Instantiate(enemy.slash);
            sslash.transform.position = b.battleTarget.transform.position;
        }
        else if (multi == true)
        {
            Debug.Log("p");
            foreach (GameObject hero in b.charStats)
            {
                Vector3 babb = hero.transform.position;
                GameObject sslash = Instantiate(enemy.slash);
                sslash.transform.position = hero.transform.position;
            }
            multi = false;
        }
    }
}
