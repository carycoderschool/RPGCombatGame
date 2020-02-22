﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class battleSystem : MonoBehaviour
{
    public objectLists lists;
    public List<baseStats> stats = new List<baseStats>();
    GameObject thatTarget;
    GameObject target;
    baseStats attacker;
    float damage;
    int currentActor = 0;
    public string atk = "normal";
    public delegate void SpecialDelegate(string name);
    
    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < lists.enemies.Length; i++)
        {
            stats.Add(lists.enemies[i].GetComponent<baseStats>());
        }
        for (int i = 0; i < lists.chars.Length; i++)
        {
            stats.Add(lists.chars[i].GetComponent<baseStats>());
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
    
    public void enemyChoose()
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
                lists.buttons[i].GetComponent<Button>().onClick.AddListener(() => specialAttackChoose(enemy));
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
        List<baseStats> sortedStats = stats.OrderBy(s => s.speed).ToList();
       
        sortedStats.Reverse();
       
        baseStats currentCharTurn = sortedStats[currentActor];
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
            foreach (baseStats stat in sortedStats)
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
            lists.buttons[0].GetComponent<Button>().onClick.AddListener(enemyChoose);
            lists.buttons[0].GetComponent<Button>().Select();
            lists.buttons[1].GetComponentInChildren<Text>().text = "Items";
            lists.buttons[2].GetComponentInChildren<Text>().text = "Special Attacks";
        lists.buttons[2].GetComponent<Button>().onClick.AddListener(spec);
        lists.buttons[3].GetComponentInChildren<Text>().text = "Run";

        }
       
        IEnumerator enemyAttack(GameObject target)
        {
            yield return new WaitForSeconds(2f);


            damage = attacker.GetComponent<baseStats>().attack - target.GetComponent<baseStats>().def;
            target.GetComponent<baseStats>().HP -= damage;
            yield return new WaitForSeconds(2f);
            TurnOrder();
        }
        void Back()
        {

        }
    void spec()
    {
        atk = "special";
        enemyChoose();
    }

        void Attack(GameObject enemy)
        {


        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = false;

        }
            damage = attacker.attack - enemy.GetComponent<baseStats>().def;
            enemy.GetComponent<baseStats>().HP -= damage;
            TurnOrder();


        }
        public void specialAttackChoose(GameObject enemy)
    {
         
        
        lists.buttons[0].GetComponentInChildren<Text>().text = attacker.name1;

        lists.buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
        
        lists.buttons[0].GetComponent<Button>().onClick.AddListener(() => attacker.SpecialAttack1(attacker.name1, "None", "None", attacker, enemy.GetComponent<baseStats>(), 2, 3));
        if (attacker.name2 != "")
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = attacker.name2;
            lists.buttons[1].GetComponent<Button>().interactable = true;
            lists.buttons[1].GetComponent<Button>().onClick.RemoveAllListeners();
            lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => attacker.SpecialAttack2(attacker.name2, "None", "None", attacker, enemy.GetComponent<baseStats>(), 2, 3));
        } else
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = "";
            lists.buttons[1].GetComponent<Button>().interactable = false;
        }
        if (attacker.name3 != "")
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = attacker.name2;
            lists.buttons[2].GetComponent<Button>().interactable = true;
            lists.buttons[2].GetComponent<Button>().onClick.RemoveAllListeners();
            lists.buttons[2].GetComponent<Button>().onClick.AddListener(() => attacker.SpecialAttack3(attacker.name2, "None", "None", attacker, enemy.GetComponent<baseStats>(), 2, 3));
        } else
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = "";
            lists.buttons[2].GetComponent<Button>().interactable = false;
        }

    }


} 
