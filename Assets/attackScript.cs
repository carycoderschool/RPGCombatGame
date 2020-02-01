using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attackScript : MonoBehaviour
{
    public objectLists lists;
    
    GameObject playerT;
    GameObject enemy;
    characterStats player;
     float damage;
    // Start is called before the first frame update
    void Start()
    {
        enemy = lists.enemies[0];
        player = gameObject.GetComponent<characterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
     void Attack()
    {
        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = false;
            
        }
        damage = player.attack - enemy.GetComponent<enemyStats>().def;
        enemy.GetComponent<enemyStats>().HP -= damage;
        
        StartCoroutine(enemyTurn()); 
    }
    IEnumerator enemyTurn()
    {
        yield return new WaitForSeconds(2f);

        int ran = Random.Range(0, lists.chars.Length);
        playerT = lists.chars[ran];
        damage = enemy.GetComponent<enemyStats>().attack - player.def;
        player.HP -= damage;
        yield return new WaitForSeconds(2f);
        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }
    public void enemyChoose()
    {
        
    }
    
   
}
