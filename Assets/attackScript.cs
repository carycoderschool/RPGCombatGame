using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class attackScript : MonoBehaviour
{
    public objectLists lists;
    
    GameObject thatTarget;
    GameObject target;
    characterStats attacker;
     float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
        attacker = gameObject.GetComponent<characterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        List<characterStats> stats = new List<characterStats>();

        List<characterStats> sortedStats = stats.OrderBy(s => s.speed).ToList();
    }
    public void enemyChoose()
    {
        for (int i = 0; i < lists.enemies.Length; i++)
        {
            lists.buttons[i].GetComponentInChildren<Text>().text = lists.enemies[i].name;
            lists.buttons[i].GetComponent<Button>().interactable = true;
            GameObject enemy = lists.enemies[i];
            lists.buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            lists.buttons[i].GetComponent<Button>().onClick.AddListener(() => Attack(enemy));
            lists.buttons[i + 1].GetComponentInChildren<Text>().text = "";
            lists.buttons[i + 1].GetComponent<Button>().interactable = false;
        }
        lists.buttons[3].GetComponentInChildren<Text>().text = "Back";
        lists.buttons[3].GetComponent<Button>().interactable = true;
        lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[3].GetComponent<Button>().onClick.AddListener(Back);

    }

    void TurnOrder()
    {
        GameObject speedestEnemy = null;
        GameObject speedestChar= null;
        float mostSpeedE = 0;
        float mostSpeedC = 0;
        
        
        
    }
    void playerTurn()
    {
        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
       
    }
    void Attack(GameObject enemy)
    {

       
        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = false;
            
        }
        damage = attacker.attack - enemy.GetComponent<enemyStats>().def;
        enemy.GetComponent<enemyStats>().HP -= damage;
        
         
    }
    IEnumerator enemyTurn(GameObject player)
    {
        yield return new WaitForSeconds(2f);
        
        int ran = Random.Range(0, lists.chars.Length);
        thatTarget = lists.chars[ran];
        damage = target.GetComponent<enemyStats>().attack - attacker.def;
        attacker.HP -= damage;
        yield return new WaitForSeconds(2f);
        
    }
    void Back()
    {
       
    }
    
   
}
