using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class battleSystem : MonoBehaviour
{
    SpecialDelegate SA;
    SpecialDelegate SSA;
    ItemDelegate itemUse;
    Item item;
    int SAnum;
    public int att;
    public Transform textParent;
    public objectLists lists;
    public List<baseStats> stats = new List<baseStats>();
    GameObject thatTarget;
    public GameObject battleTarget;
    public baseStats attacker;
    public float damage;
    public int currentActor = 0;
    public string atk = "normal";
    public delegate void SpecialDelegate(string name, baseStats attacker, baseStats target);
    public delegate void ItemDelegate();
    public Item itemPrefab;
    public InventorySlot[] slots;
    public Image[] slotSprites;
    public Transform itemsParent;
    public string currentAction;
    public Sprite BG;
    public Sprite buttonSpr;
    public List<baseStats> sortedStats;
    public GameObject slashPrefab;
    public Text battleText;

    // Start is called before the first frame update
    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slotSprites = itemsParent.GetComponentsInChildren<Image>();
        
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
            if (lists.enemies[i].GetComponent<baseStats>().character != null)
            stats.Add(lists.enemies[i].GetComponent<baseStats>());
        }
        for (int i = 0; i < lists.chars.Count; i++)
        {
            if (lists.chars[i].GetComponent<baseStats>().character != null)
                stats.Add(lists.chars[i].GetComponent<baseStats>());
        }
        TurnOrder();
    }
    public void Reset()
    {

        TurnOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacker.skip == true)
        {
            StartCoroutine(skip());
        }
    }
    IEnumerator skip()
    {
        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = false;

        }
        battleText.text = "You can't do that!";
        yield return new WaitForSeconds(1.5f);
        attacker.skip = false;
        playerTurn();
    }
    public void UseInventory()
    {
        currentAction = "items";
        itemsParent.gameObject.GetComponent<Image>().enabled = true;
        foreach (InventorySlot slot in slots)
        {
            slot.gameObject.GetComponentInChildren<Button>().interactable = true;
            slot.gameObject.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            slot.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => StoreItem(slot.item, slot.UseItemm));
            slot.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => partyChoose(false));
            if (slot.item != null)
            {
                slot.removeButton.interactable = true;
                slot.removeButton.gameObject.GetComponent<Image>().enabled = true;
            }
            else
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
        stats = sortedStats;
        baseStats currentCharTurn = sortedStats[currentActor];
        List<baseStats> enemyList = new List<baseStats>();

        foreach (baseStats stat in stats)
        {
            if (stat.gameObject.tag == "enemy")
            {
                enemyList.Add(stat);
            }
        }
        List<baseStats> heroList = new List<baseStats>();
        foreach (baseStats stat in stats)
        {
            if (stat.gameObject.tag == "Player")
            {
                heroList.Add(stat);
            }
        }
        // Take that turn
       
        if (enemyList.Count < 1)
        {
            Debug.Log("poggers");
            battleText.text = "POGGERS";
            GlobalManager.instance.BattleToOverworldTransition(gameObject);
        } else if (heroList.Count < 1)
        {
            Debug.Log("OmegaLOL");
            battleText.text = "OmegaLOL";
        }
        else if (currentCharTurn.status != "")
        {
            
            StatusEffects.CheckStatus(currentCharTurn, this);
            StatusEffects.CheckBuff(currentCharTurn);
            attacker = currentCharTurn;
            Debug.Log("Turn:" + attacker.nameChar);
        } 
        else if (currentCharTurn.gameObject.tag == "Player" && currentCharTurn.turn == false)
        {
            StatusEffects.CheckBuff(currentCharTurn);

            currentCharTurn.turn = true;
            attacker = currentCharTurn;
            foreach (InventorySlot slot in slots)
            {
                slot.attacker = attacker;

            }
            Debug.Log("Turn:" + attacker.nameChar);
            playerTurn();
        }
        else if (currentCharTurn.gameObject.tag == "enemy" && currentCharTurn.turn == false)
        {
            StatusEffects.CheckBuff(currentCharTurn);
            currentCharTurn.turn = true;
            
            attacker = currentCharTurn;
            foreach (InventorySlot slot in slots)
            {
                slot.attacker = attacker;

            }
            Debug.Log("Turn:" + attacker.nameChar);
            attacker.gameObject.GetComponent<enemyDeath>().Die();
            attacker.character.spec.enemyAI(attacker);
          

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
    public void playerTurn()
    {
        battleText.text = "What will " + attacker.nameChar + " do?";
        atk = "normal";
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
    public IEnumerator sleep()
    {
        yield return new WaitForSeconds(5f);
        TurnOrder();
    }
    public IEnumerator enemyAttack(GameObject target)
    {
        
        attacker.slash = slashPrefab;
        yield return new WaitForSeconds(1f);
        battleText.text = attacker.nameChar + " attacks!";
        if (attacker == null)
        {
            TurnOrder();
        }

        damage = attacker.GetComponent<baseStats>().attack - target.GetComponent<baseStats>().def;

        attacker.gameObject.GetComponent<Animator>().SetBool("attack", true);

        yield return new WaitForSeconds(2f);
        
        target.GetComponent<baseStats>().HP -= damage;
        target.GetComponent<baseStats>().damageText.gameObject.GetComponent<Text>().text = damage.ToString();
        target.GetComponent<baseStats>().damageText.gameObject.GetComponent<DamageTextEffect>().DamageStartFloating();
        attacker.gameObject.GetComponent<Animator>().SetBool("attack", false);
        if (target.GetComponent<baseStats>().HP < 1)
        {
            StartCoroutine(Die(target.GetComponent<baseStats>()));
        }
        else
        {
            TurnOrder();
        }
        
    }
    void Back()
    {
        if (currentAction == "enemyChoose" || currentAction == "SAChoose" || currentAction == "items")
        {
            playerTurn();
        }
        else if (currentAction == "SAPhysical" || currentAction == "SAStatus" || currentAction == "SAenemyChoose" || currentAction == "SAPartyChoose")
        {
            specialTypeChoose();
        }


    }
    void spec()
    {
        atk = "special";
        specialTypeChoose();
    }
    public IEnumerator Attack(GameObject enemy)
    {
        attacker.slash = slashPrefab;
        battleTarget = enemy;
        attacker.GetComponent<Animator>().SetBool("attack", true);
        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = false;

        }
        damage = attacker.attack - enemy.GetComponent<baseStats>().def;
        battleText.text = attacker.nameChar + " attacks!";
        yield return new WaitForSeconds(2f);
        enemy.GetComponent<baseStats>().HP -= damage;
        enemy.GetComponent<baseStats>().damageText.gameObject.GetComponent<Text>().text = damage.ToString();
        enemy.GetComponent<baseStats>().damageText.gameObject.GetComponent<DamageTextEffect>().DamageStartFloating();
        attacker.GetComponent<Animator>().SetBool("attack", false);
        if (enemy.GetComponent<baseStats>().HP < 1)
        {
            StartCoroutine(Die(enemy.GetComponent<baseStats>()));
        } else
        {
            TurnOrder();
        }
        



    }
    public void enemyChoose(bool SA)
    {
        List<baseStats> enemyList = new List<baseStats>();
        foreach (baseStats stat in stats)
        {
            if (stat.gameObject.tag == "enemy")
            {
                enemyList.Add(stat);
            }
        }
        enemyList = enemyList.OrderBy(c => c.GetComponent<baseStats>().importance).ToList();
        if (SA == true)
        {
            currentAction = "SAenemyChoose";
        }
        else
        {
            currentAction = "enemyChoose";
        }

        if (atk == "normal")
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                int gaming = i + 1;
                lists.buttons[i].GetComponentInChildren<Text>().text = enemyList[i].nameChar + "(" + gaming + ")";
                lists.buttons[i].GetComponent<Button>().interactable = true;
                GameObject enemy = enemyList[i].gameObject;
                lists.buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                lists.buttons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Attack(enemy)));
                lists.buttons[i + 1].GetComponentInChildren<Text>().text = "";
                lists.buttons[i + 1].GetComponent<Button>().interactable = false;

            }
        }
        else if (atk == "special")
        {
            for (int i = 0; i < enemyList.Count; i++)
            {

                lists.buttons[i].GetComponentInChildren<Text>().text = enemyList[i].nameChar;
                lists.buttons[i].GetComponent<Button>().interactable = true;
                baseStats enemy = enemyList[i];
                lists.buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                lists.buttons[i].GetComponent<Button>().onClick.AddListener(() => UseSA(enemy));
                lists.buttons[i + 1].GetComponentInChildren<Text>().text = "";
                lists.buttons[i + 1].GetComponent<Button>().interactable = false;
            }
        }
        if (enemyList.Count < 2)
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = "";
            lists.buttons[2].GetComponent<Button>().interactable = false;
        }
        lists.buttons[3].GetComponentInChildren<Text>().text = "Back";
        lists.buttons[3].GetComponent<Button>().interactable = true;
        lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[3].GetComponent<Button>().onClick.AddListener(Back);

    }
    public void partyChoose(bool revive)
    {
        if (currentAction == "SAStatus")
        {
            currentAction = "SAPartyChoose";
            if (revive == true)
            {
                List<baseStats> heroList = new List<baseStats>();
                foreach (GameObject charac in lists.chars)
                {
                    if (charac.tag == "Player" && charac.GetComponent<baseStats>().status == "Fainted")
                    {
                        heroList.Add(charac.GetComponent<baseStats>());
                    }
                }
                if (heroList.Count < 1)
                {
                    attacker.skip = true;
                }
                else
                {
                    for (int i = 0; i < heroList.Count; i++)
                    {
                        lists.buttons[i].GetComponentInChildren<Text>().text = heroList[i].nameChar;
                        lists.buttons[i].GetComponent<Button>().interactable = true;
                        baseStats hero = heroList[i];
                        lists.buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                        lists.buttons[i].GetComponent<Button>().onClick.AddListener(() => UseSA(hero));
                        lists.buttons[i + 1].GetComponentInChildren<Text>().text = "";
                        lists.buttons[i + 1].GetComponent<Button>().interactable = false;
                    }
                    if (heroList.Count < 2)
                    {
                        lists.buttons[2].GetComponentInChildren<Text>().text = "";
                        lists.buttons[2].GetComponent<Button>().interactable = false;
                    }
                    lists.buttons[3].GetComponentInChildren<Text>().text = "Back";
                    lists.buttons[3].GetComponent<Button>().interactable = true;
                    lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
                    lists.buttons[3].GetComponent<Button>().onClick.AddListener(Back);
                }
            } else
            {
                List<baseStats> heroList = new List<baseStats>();
                foreach (baseStats stat in stats)
                {
                    if (stat.gameObject.tag == "Player")
                    {
                        heroList.Add(stat);
                    }
                }
                
                    for (int i = 0; i < heroList.Count; i++)
                    {
                        lists.buttons[i].GetComponentInChildren<Text>().text = heroList[i].nameChar;
                        lists.buttons[i].GetComponent<Button>().interactable = true;
                        baseStats hero = heroList[i];
                        lists.buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                        lists.buttons[i].GetComponent<Button>().onClick.AddListener(() => UseSA(hero));
                        lists.buttons[i + 1].GetComponentInChildren<Text>().text = "";
                        lists.buttons[i + 1].GetComponent<Button>().interactable = false;
                    }
                    if (heroList.Count < 2)
                    {
                        lists.buttons[2].GetComponentInChildren<Text>().text = "";
                        lists.buttons[2].GetComponent<Button>().interactable = false;
                    }
                    lists.buttons[3].GetComponentInChildren<Text>().text = "Back";
                    lists.buttons[3].GetComponent<Button>().interactable = true;
                    lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
                    lists.buttons[3].GetComponent<Button>().onClick.AddListener(Back);
                
                
            } 
            
            
        }
        else if (currentAction == "items")
        {
            List<baseStats> heroList = new List<baseStats>();
            foreach (baseStats stat in stats)
            {
                if (stat.gameObject.tag == "Player")
                {
                    heroList.Add(stat);
                }
            }
            for (int i = 0; i < heroList.Count; i++)
            {
                lists.buttons[i].GetComponentInChildren<Text>().text = heroList[i].nameChar;
                lists.buttons[i].GetComponent<Button>().interactable = true;
                
                lists.buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                lists.buttons[i].GetComponent<Button>().onClick.AddListener(() => itemUse());
                lists.buttons[i + 1].GetComponentInChildren<Text>().text = "";
                lists.buttons[i + 1].GetComponent<Button>().interactable = false;
            }
            if (heroList.Count < 2)
            {
                lists.buttons[2].GetComponentInChildren<Text>().text = "";
                lists.buttons[2].GetComponent<Button>().interactable = false;
            }
            lists.buttons[3].GetComponentInChildren<Text>().text = "Back";
            lists.buttons[3].GetComponent<Button>().interactable = true;
            lists.buttons[3].GetComponent<Button>().onClick.RemoveAllListeners();
            lists.buttons[3].GetComponent<Button>().onClick.AddListener(Back);
        }
        
    }
    void specialTypeChoose()
    {
        currentAction = "SAChoose";
        lists.buttons[0].GetComponentInChildren<Text>().text = "Physical";
        lists.buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
        lists.buttons[0].GetComponent<Button>().onClick.AddListener(specialAttackChoose);
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
    public void specialAttackChoose()
    {
        currentAction = "SAPhysical";
        lists.buttons[0].GetComponentInChildren<Text>().text = attacker.character.spec.physicalName1;
        lists.buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
        SpecialDelegate one = attacker.character.spec.SpecialAttack1;
        lists.buttons[0].GetComponent<Button>().onClick.AddListener(() => StoreSA(one, 1));
        
        if (attacker.character.spec.physicalName2 != "")
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = attacker.character.spec.physicalName2;
            lists.buttons[1].GetComponent<Button>().interactable = true;
            lists.buttons[1].GetComponent<Button>().onClick.RemoveAllListeners();
            SpecialDelegate two = attacker.character.spec.SpecialAttack2;
            lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => StoreSA(two, 2));
            
        }
        else
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = "";
            lists.buttons[1].GetComponent<Button>().interactable = false;
        }
        if (attacker.character.spec.physicalName3 != "")
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = attacker.character.spec.physicalName3;
            lists.buttons[2].GetComponent<Button>().interactable = true;
            lists.buttons[2].GetComponent<Button>().onClick.RemoveAllListeners();
            SpecialDelegate three = attacker.character.spec.SpecialAttack3;
            lists.buttons[2].GetComponent<Button>().onClick.AddListener(() => StoreSA(three, 3));
            
        }
        else
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = "";
            lists.buttons[2].GetComponent<Button>().interactable = false;
        }


    }
    public void StatusSpecialAttackChoose()
    {
        currentAction = "SAStatus";

        lists.buttons[0].GetComponentInChildren<Text>().text = attacker.character.spec.statusName1;

        lists.buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
        SpecialDelegate one = attacker.character.spec.StatusSpecialAttack1;
        if (attacker.revive1 == true)
        {
            lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => StoreSSA(one, true, 4));
        }
        else if (attacker.revive1 == false)
        {
            lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => StoreSSA(one, false, 4));
        }

        if (attacker.character.spec.statusName2 != "")
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = attacker.character.spec.statusName2;
            lists.buttons[1].GetComponent<Button>().interactable = true;
            lists.buttons[1].GetComponent<Button>().onClick.RemoveAllListeners();
            SpecialDelegate two = attacker.character.spec.StatusSpecialAttack2;
            if (attacker.revive2 == true)
            {
                lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => StoreSSA(two, true, 5));
            } else if (attacker.revive2 == false)
            {
                lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => StoreSSA(two, false, 5));
            }
            
            
        }
        else
        {
            lists.buttons[1].GetComponentInChildren<Text>().text = "";
            lists.buttons[1].GetComponent<Button>().interactable = false;
        }
        if (attacker.character.spec.statusName3 != "")
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = attacker.character.spec.statusName3;
            lists.buttons[2].GetComponent<Button>().interactable = true;
            lists.buttons[2].GetComponent<Button>().onClick.RemoveAllListeners();
            SpecialDelegate three = attacker.character.spec.StatusSpecialAttack3;
            if (attacker.revive3 == true)
            {
                lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => StoreSSA(three, true, 6));
            }
            else if (attacker.revive3 == false)
            {
                lists.buttons[1].GetComponent<Button>().onClick.AddListener(() => StoreSSA(three, false, 6));
            }

        }
        else
        {
            lists.buttons[2].GetComponentInChildren<Text>().text = "";
            lists.buttons[2].GetComponent<Button>().interactable = false;
        }


    }
    public IEnumerator Die(baseStats stat)
    {
        stats.Remove(stat);
        sortedStats.Remove(stat);
        stat.gameObject.GetComponent<Animator>().SetBool("fainted", true);
        stat.HP = 0;
        if (stat.gameObject.tag == "enemy")
        {
            battleText.text = "The " + stat.nameChar + " is destroyed!";
            
            yield return new WaitForSeconds(1f);

            sortedStats = stats.OrderBy(s => s.speed).ToList();

            sortedStats.Reverse();
            foreach (baseStats state in sortedStats)
            {
                state.turn = false;
            }
            currentActor = 0;
            TurnOrder();

        } else if (stat.gameObject.tag == "Player")
        {
            battleText.text = stat.nameChar + " faints!";

            stat.status = "Fainted";
            yield return new WaitForSeconds(1f);

            sortedStats = stats.OrderBy(s => s.speed).ToList();

            sortedStats.Reverse();
            foreach (baseStats state in sortedStats)
            {
                state.turn = false;
            }
            currentActor = 0;
            TurnOrder();


        } 
            
        
    }
    public IEnumerator Revive(baseStats stat)
    {
        stats.Add(stat);
        foreach (GameObject button in lists.buttons)
        {
            button.GetComponent<Button>().interactable = false;

        }
        
        if (stat.gameObject.tag == "enemy")
        {
            battleText.text = "The " + stat.nameChar + " came back from the dead!";
            stat.gameObject.GetComponent<Animator>().SetBool("dead", false);
            yield return new WaitForSeconds(1f);
            sortedStats = stats.OrderBy(s => s.speed).ToList();

            sortedStats.Reverse();
            foreach (baseStats state in sortedStats)
            {
                state.turn = false;
            }
            currentActor = 0;
            TurnOrder();
        }
        else if (stat.gameObject.tag == "Player")
        {
            battleText.text = stat.nameChar + " is revived!";
            stat.gameObject.GetComponent<Animator>().SetBool("fainted", false);
            stat.status = "";
            yield return new WaitForSeconds(1f);
            sortedStats = stats.OrderBy(s => s.speed).ToList();

            sortedStats.Reverse();
            foreach (baseStats state in sortedStats)
            {
                state.turn = false;
            }
            currentActor = 0;
            TurnOrder();
        }
    }
    void StoreSA(SpecialDelegate chosenSA, int num)
    {

        SA = chosenSA;
        SAnum = num;
        enemyChoose(true);
    }
    void StoreSSA(SpecialDelegate chosenSA, bool revive, int num)
    {
        SA = chosenSA;
        SAnum = num;
        if (revive == true)
        {
            partyChoose(true);
        } else if (revive == false)
        {
            partyChoose(false);
        }
    }
    void UseSA(baseStats target)
    {
        if (SAnum == 1)
        {
            SA(attacker.character.spec.physicalName1, attacker, target);
        } else if (SAnum == 2)
        {
            SA(attacker.character.spec.physicalName2, attacker, target);
        } else if (SAnum == 3)
        {
            SA(attacker.character.spec.physicalName3, attacker, target);
        } else if (SAnum == 4)
        {
            SA(attacker.character.spec.statusName1, attacker, target);
        } else if (SAnum == 5)
        {
            SA(attacker.character.spec.statusName2, attacker, target);
        } else if (SAnum == 6)
        {
            SA(attacker.character.spec.statusName3, attacker, target);
        }
        
    }
    void StoreItem(Item itemm, ItemDelegate itemmm)
    {
        itemUse = itemmm;
    }
}
