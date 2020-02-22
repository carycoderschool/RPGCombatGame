using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class attackScript : MonoBehaviour
{
    public objectLists lists;
    public List<BaseStats> stats = new List<BaseStats>();
    GameObject thatTarget;
    GameObject target;
    BaseStats attacker;
    float damage;
    int currentActor = 0;
    string atk = "normal";
    public delegate void SpecialDelegate(string name);
    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < lists.enemies.Length; i++)
        {
            stats.Add(lists.enemies[i].GetComponent<BaseStats>());
        }
        for (int i = 0; i < lists.chars.Length; i++)
        {
            stats.Add(lists.chars[i].GetComponent<BaseStats>());
        }
        TurnOrder();
    }
    void Reset()
    {
        // TurnOrder();  
    }

    // Update is called once per frame
    void Update()
    {



    }
    
    public void enemyChoose(UnityAction specAtk)
    {
        if (atk == "normal")
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
        } else if (atk == "special")
        {
            for (int i = 0; i < lists.enemies.Length; i++)
            {
                lists.buttons[i].GetComponentInChildren<Text>().text = lists.enemies[i].name;
                lists.buttons[i].GetComponent<Button>().interactable = true;
                GameObject enemy = lists.enemies[i];
                lists.buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                lists.buttons[i].GetComponent<Button>().onClick.AddListener(specAtk);
                lists.buttons[i + 1].GetComponentInChildren<Text>().text = "";
                lists.buttons[i + 1].GetComponent<Button>().interactable = false;
            }
        }
        
        lists.buttons[3].GetComponentInChildren<Text>().text = "Back";
        lists.buttons[3].GetComponent<Button>().interactable = true;
        lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[3].GetComponent<Button>().onClick.AddListener(Back);

    }

    void TurnOrder()
    {
        List<BaseStats> sortedStats = stats.OrderBy(s => s.speed).ToList();
       
        sortedStats.Reverse();
       
        BaseStats currentCharTurn = sortedStats[currentActor];
        // Take that turn
        
        if (currentCharTurn.gameObject.tag == "Player" && currentCharTurn.turn == false)
        {
            
            currentCharTurn.turn = true;
            attacker = currentCharTurn;
            playerTurn();
        }
        else if (currentCharTurn.gameObject.tag == "enemy" && currentCharTurn.turn == false)
        {
            
            currentCharTurn.turn = true;
            int ran = Random.Range(0, lists.chars.Length);
            target = lists.chars[ran];
            attacker = currentCharTurn;
            StartCoroutine(enemyAttack(target));
            
        }
        currentActor++;
        if (currentActor > sortedStats.Count - 1)
        {
            foreach (BaseStats stat in sortedStats)
            {
                stat.turn = false;
            }
            currentActor = 0;
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
            lists.buttons[0].GetComponent<Button>().onClick.AddListener(() => enemyChoose(null));
            lists.buttons[0].GetComponent<Button>().Select();
            lists.buttons[1].GetComponentInChildren<Text>().text = "Items";
            lists.buttons[2].GetComponentInChildren<Text>().text = "Special Attacks";
            lists.buttons[3].GetComponentInChildren<Text>().text = "Run";

        }
       
        IEnumerator enemyAttack(GameObject target)
        {
            yield return new WaitForSeconds(2f);


            damage = attacker.GetComponent<BaseStats>().attack - target.GetComponent<BaseStats>().def;
            target.GetComponent<BaseStats>().HP -= damage;
            yield return new WaitForSeconds(2f);
            TurnOrder();
        }
        void Back()
        {

        }

        void Attack(GameObject enemy)
        {


        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = false;

        }
            damage = attacker.attack - enemy.GetComponent<BaseStats>().def;
            enemy.GetComponent<BaseStats>().HP -= damage;
            TurnOrder();


        }
        public void specialAttackChoose()
    {
        BaseStats targetHit = target.GetComponent<BaseStats>();
        atk = "Special";
        lists.buttons[0].GetComponentInChildren<Text>().text = attacker.name1;
        UnityAction atk1;
        atk1 += attacker.SpecialAttack1(attacker.name1 /*, "None", "None", attacker, 2, 3*/));
        lists.buttons[0].GetComponent<Button>().onClick.AddListener(() => enemyChoose(atk1));
        if (attacker.name2 != "")
        {

        }
    }


} 
