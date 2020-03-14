using System.Linq;
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
    public GameObject battleTarget;
    baseStats attacker;
    float damage;
    int currentActor = 0;
    public string atk = "normal";
    public delegate void SpecialDelegate(string name);
    public Item itemPrefab;
    public InventorySlot[] slots;
    public Image[] slotSprites;
    public Transform itemsParent;
    public string currentAction;
    public Sprite BG;
    public Sprite buttonSpr;
    public List<baseStats> sortedStats;
    public GameObject slashPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slotSprites = itemsParent.GetComponentsInChildren<Image>();
        lists.AddItem(itemPrefab);
        lists.AddItem(itemPrefab);
        lists.AddItem(itemPrefab);
        itemsParent.gameObject.GetComponent<Image>().enabled = false;
        foreach (InventorySlot slot in slots)
        {
            slot.gameObject.GetComponentInChildren<Button>().interactable = false;
            slot.removeButton.interactable = false;
            slot.removeButton.gameObject.GetComponent<Image>().enabled = false;
            slot.icon.enabled = false;
            slot.color.a = 0;
            slot.gameObject.GetComponentInChildren<Image>().enabled = false;
        }

        for (int i = 0; i < lists.enemies.Count; i++)
        {
            stats.Add(lists.enemies[i].GetComponent<baseStats>());
        }
        for (int i = 0; i < lists.chars.Count; i++)
        {
            stats.Add(lists.chars[i].GetComponent<baseStats>());
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
    public void UseInventory()
    {
        currentAction = "items";
        itemsParent.gameObject.GetComponent<Image>().enabled = true;
        foreach (InventorySlot slot in slots)
        {
            slot.gameObject.GetComponentInChildren<Button>().interactable = true;
            
            if (slot.item != null)
            {
                slot.removeButton.interactable = true;
                slot.removeButton.gameObject.GetComponent<Image>().enabled = true;
            } else
            {
                slot.removeButton.interactable = false;
                slot.removeButton.gameObject.GetComponent<Image>().enabled = false;
            }
            slot.icon.enabled = true;
            slot.color.a = 225;
            slot.gameObject.GetComponentInChildren<Image>().enabled = true;
        }
        foreach (GameObject buttn in lists.buttons)
        {
            buttn.GetComponent<Button>().interactable = false;
        }
        lists.buttons[3].GetComponentInChildren<Text>().text = "Back";
        lists.buttons[3].GetComponent<Button>().interactable = true;
        lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[3].GetComponent<Button>().onClick.AddListener(Back);
    }
    public void enemyChoose(bool SA)
    {
        if (SA == true)
        {
            currentAction = "SAenemyChoose";
        } else
        {
            currentAction = "enemyChoose";
        }
        
        if (atk == "normal")
        {
            for (int i = 0; i < lists.enemies.Count; i++)
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
            for (int i = 0; i < lists.enemies.Count; i++)
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
        if (lists.enemies.Count < 2)
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = "";
            lists.buttons[2].GetComponent<Button>().interactable = false;
        }
        lists.buttons[3].GetComponentInChildren<Text>().text = "Back";
        lists.buttons[3].GetComponent<Button>().interactable = true;
        lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[3].GetComponent<Button>().onClick.AddListener(Back);

    }

    public void TurnOrder()
    {
        
        
        itemsParent.gameObject.GetComponent<Image>().enabled = false;
        foreach (InventorySlot slot in slots)
        {
            slot.gameObject.GetComponentInChildren<Button>().interactable = false;
            slot.removeButton.interactable = false;
            slot.removeButton.gameObject.GetComponent<Image>().enabled = false;
            
            slot.gameObject.GetComponentInChildren<Image>().enabled = false;
            
        }
        foreach (InventorySlot slot in slots)
        {
            slot.icon.enabled = false;
            slot.color.a = 0;
        }
            foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = false;

        }

        sortedStats = stats.OrderBy(s => s.speed).ToList();
       
        sortedStats.Reverse();
        int size = sortedStats.Count;
        baseStats currentCharTurn = sortedStats[currentActor];
        // Take that turn

       if (lists.enemies.Count < 1)
        {
            Debug.Log("win");
        }
        else if (currentCharTurn.gameObject.tag == "Player" && currentCharTurn.turn == false)
        {

            currentCharTurn.turn = true;
            attacker = currentCharTurn;
            foreach (InventorySlot slot in slots)
            {
                slot.attacker = attacker;

            }
            Debug.Log("Turn:" + attacker.name);
            playerTurn();
        }
        else if (currentCharTurn.gameObject.tag == "enemy" && currentCharTurn.turn == false)
        {

            currentCharTurn.turn = true;
            int ran = Random.Range(0, lists.chars.Count);
            battleTarget = lists.chars[ran];
            attacker = currentCharTurn;
            foreach (InventorySlot slot in slots)
            {
                slot.attacker = attacker;

            }
            Debug.Log("Turn:" + attacker.name);
            StartCoroutine(enemyAttack(battleTarget));

        }
       
        if (size > sortedStats.Count)
        {
            sortedStats = stats.OrderBy(s => s.speed).ToList();

            sortedStats.Reverse();
            foreach (baseStats stat in sortedStats)
            {
                stat.turn = false;
            }
            currentActor = 0;
            Reset();
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
        atk = "normal";
        itemsParent.gameObject.GetComponent<Image>().enabled = false;
        foreach (InventorySlot slot in slots)
        {
            slot.gameObject.GetComponentInChildren<Button>().interactable = false;
            slot.removeButton.interactable = false;
            slot.removeButton.gameObject.GetComponent<Image>().enabled = false;

            slot.gameObject.GetComponentInChildren<Image>().enabled = false;

        }

        foreach (GameObject button in lists.buttons)
            {
                button.GetComponent<Button>().interactable = true;
            }
            lists.buttons[0].GetComponentInChildren<Text>().text = "Attack";
            lists.buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
            lists.buttons[0].GetComponent<Button>().onClick.AddListener(() => enemyChoose(false));
            lists.buttons[0].GetComponent<Button>().Select();
        lists.buttons[1].GetComponentInChildren<Text>().text = "Items";
        lists.buttons[1].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[1].GetComponent<Button>().onClick.AddListener(UseInventory);
        lists.buttons[2].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[2].GetComponentInChildren<Text>().text = "Special Attacks";
        lists.buttons[2].GetComponent<Button>().onClick.AddListener(spec);
        lists.buttons[3].GetComponentInChildren<Text>().text = "Run";
        lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
    }
       
        IEnumerator enemyAttack(GameObject target)
        {
            yield return new WaitForSeconds(2f);
            if (attacker == null)
        {
            TurnOrder();
        }

            damage = attacker.GetComponent<baseStats>().attack - target.GetComponent<baseStats>().def;
            target.GetComponent<baseStats>().HP -= damage;
        attacker.gameObject.GetComponent<Animator>().SetBool("enemyAttacks", true);
            yield return new WaitForSeconds(1f);
        attacker.gameObject.GetComponent<Animator>().SetBool("enemyAttacks", false);
        TurnOrder();
        }
        void Back()
        {
        if (currentAction == "enemyChoose" || currentAction == "SAChoose" || currentAction == "items")
        {
            playerTurn();
        }
        else if (currentAction == "SAPhysical" || currentAction == "SAStatus" || currentAction == "SAenemyChoose")
        {
            specialTypeChoose();
        } 
        
            
        }
    void spec()
    {
        atk = "special";
        specialTypeChoose();
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
    void specialTypeChoose()
    {
        currentAction = "SAChoose";
        lists.buttons[0].GetComponentInChildren<Text>().text = "Physical";
        lists.buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[0].GetComponent<Button>().onClick.AddListener(() => enemyChoose(true));
        lists.buttons[0].GetComponent<Button>().interactable = true;
        lists.buttons[1].GetComponentInChildren<Text>().text = "Status";
        lists.buttons[1].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[1].GetComponent<Button>().onClick.AddListener(StatusSpecialAttackChoose);
        lists.buttons[1].GetComponent<Button>().interactable = true;
        lists.buttons[2].GetComponent<Button>().interactable = false;
        lists.buttons[3].GetComponentInChildren<Text>().text = "Back";
        lists.buttons[3].GetComponent<Button>().interactable = true;
        lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[3].GetComponent<Button>().onClick.AddListener(Back);
    }
    public void specialAttackChoose(GameObject enemy)
    {
        currentAction = "SAPhysical";
        
        lists.buttons[0].GetComponentInChildren<Text>().text = attacker.physicalName1;

        lists.buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
        
        lists.buttons[0].GetComponent<Button>().onClick.AddListener(() => attacker.SpecialAttack1(attacker.physicalName1, attacker, enemy.GetComponent<baseStats>()));
        lists.buttons[0].GetComponent<Button>().onClick.AddListener(TurnOrder);
        if (attacker.physicalName2 != "")
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = attacker.physicalName2;
            lists.buttons[1].GetComponent<Button>().interactable = true;
            lists.buttons[1].GetComponent<Button>().onClick.RemoveAllListeners();
            lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => attacker.SpecialAttack2(attacker.physicalName2, attacker, enemy.GetComponent<baseStats>()));
            lists.buttons[1].GetComponent<Button>().onClick.AddListener(TurnOrder);
        } else
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = "";
            lists.buttons[1].GetComponent<Button>().interactable = false;
        }
        if (attacker.physicalName3 != "")
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = attacker.physicalName3;
            lists.buttons[2].GetComponent<Button>().interactable = true;
            lists.buttons[2].GetComponent<Button>().onClick.RemoveAllListeners();
            lists.buttons[2].GetComponent<Button>().onClick.AddListener(() => attacker.SpecialAttack3(attacker.physicalName3, attacker, enemy.GetComponent<baseStats>()));
            lists.buttons[2].GetComponent<Button>().onClick.AddListener(TurnOrder);
        } else
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = "";
            lists.buttons[2].GetComponent<Button>().interactable = false;
        }
        
        
    }
    public void StatusSpecialAttackChoose()
    {
        currentAction = "SAStatus";

        lists.buttons[0].GetComponentInChildren<Text>().text = attacker.statusName1;

        lists.buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();

        lists.buttons[0].GetComponent<Button>().onClick.AddListener(() => attacker.StatusSpecialAttack1(attacker.statusName1, attacker));
        lists.buttons[0].GetComponent<Button>().onClick.AddListener(TurnOrder);
        if (attacker.statusName2 != "")
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = attacker.statusName2;
            lists.buttons[1].GetComponent<Button>().interactable = true;
            lists.buttons[1].GetComponent<Button>().onClick.RemoveAllListeners();
            lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => attacker.StatusSpecialAttack2(attacker.statusName2, attacker));
            lists.buttons[1].GetComponent<Button>().onClick.AddListener(TurnOrder);
        }
        else
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = "";
            lists.buttons[1].GetComponent<Button>().interactable = false;
        }
        if (attacker.statusName3 != "")
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = attacker.statusName3;
            lists.buttons[2].GetComponent<Button>().interactable = true;
            lists.buttons[2].GetComponent<Button>().onClick.RemoveAllListeners();
            lists.buttons[2].GetComponent<Button>().onClick.AddListener(() => attacker.StatusSpecialAttack3(attacker.statusName3, attacker));
            lists.buttons[2].GetComponent<Button>().onClick.AddListener(TurnOrder);
        }
        else
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = "";
            lists.buttons[2].GetComponent<Button>().interactable = false;
        }


    }

} 
