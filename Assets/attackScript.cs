using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class attackScript : MonoBehaviour
{
    public objectLists lists;
    public List<characterStats> stats = new List<characterStats>();
    GameObject thatTarget;
    GameObject target;
    characterStats attacker;
     float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        for (int i = 0; i < lists.enemies.Length; i++)
        {
            stats.Add(lists.enemies[i].GetComponent<characterStats>());
        }
        for (int i = 0; i < lists.chars.Length; i++)
        {
            stats.Add(lists.chars[i].GetComponent<characterStats>());
        }
        TurnOrder();
    }
     void Reset()
    {
        TurnOrder();  
    }

    // Update is called once per frame
    void Update()
    {
        

        
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
        List<characterStats> sortedStats = stats.OrderBy(s => s.speed).ToList();
        for (int i = 0; i < sortedStats.Count; i++)
        {
            if (sortedStats[i].gameObject.tag == "Player" && sortedStats[i].turn == false)
            {
                
                sortedStats[i].turn = true;
                attacker = sortedStats[i];
                
                playerTurn();
                
                
            } else if (sortedStats[i].gameObject.tag == "enemy" && sortedStats[i].turn == false)
            {
                sortedStats[i].turn = true;
                int ran = Random.Range(0, lists.chars.Length);
                target = lists.chars[ran];
                attacker = sortedStats[i];
                StartCoroutine(enemyAttack(target));
                
            } else
            {
                foreach (characterStats stat in sortedStats)
                {
                    stat.turn = false;
                    
                    
                }
                Reset();
            }
            
            
        }
        
        
        
    }
    void playerTurn()
    {
        
       
        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
        lists.buttons[0].GetComponentInChildren<Text>().text = "Attack";
        lists.buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[0].GetComponent<Button>().onClick.AddListener(enemyChoose);
        lists.buttons[0].GetComponent<Button>().Select();
        lists.buttons[1].GetComponentInChildren<Text>().text = "Items";
        lists.buttons[2].GetComponentInChildren<Text>().text = "Special Attacks";
        lists.buttons[3].GetComponentInChildren<Text>().text = "Run";
       
    }
    void Attack(GameObject enemy)
    {

       
        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = false;
            
        }
        damage = attacker.attack - enemy.GetComponent<characterStats>().def;
        enemy.GetComponent<characterStats>().HP -= damage;
        TurnOrder();
        
         
    }
    IEnumerator enemyAttack(GameObject target)
    {
        yield return new WaitForSeconds(2f);
        
       
        damage = attacker.GetComponent<characterStats>().attack - target.GetComponent<characterStats>().def;
        target.GetComponent<characterStats>().HP -= damage;
        yield return new WaitForSeconds(2f);
        TurnOrder();
    }
    void Back()
    {
       
    }
    
   
}
