using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffects : MonoBehaviour
{
    static StatusEffects instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("messed up");
        }
        instance = this;
    }

    // Update is called once per frame
    public static void CheckStatus(baseStats target, battleSystem battle)
    {
        
        if (target.status == "Poison")
        {
            
            Poison(target, battle);
        } else if (target.status == "Sleep")
        {
            Sleep(target, battle);
        } else if (target.status == "Confused")
        {
            Confuse(target, battle, battle);
        } else if (target.status == "Depressed")
        {
            Depression(target, battle, battle);
        }
    }
    public static void CheckBuff(baseStats target)
    {
        if (target.buffDuration > 0)
        {
            if (target.buffedStat == "attack")
            {
                BuffDebuff(target, target.attack);
            }
            else if (target.buffedStat == "defence")
            {
                BuffDebuff(target, target.def);
            }
            else if (target.buffedStat == "speed")
            {
                BuffDebuff(target, target.speed);
            } 
        } else
        {
            if (target.buffedStat == "attack")
            {
                target.attack -= target.buff;
                target.buffedStat = "";
                target.buff = 0;
            }
            else if (target.buffedStat == "defence")
            {
                target.def -= target.buff;
                target.buffedStat = "";
                target.buff = 0;
            }
            else if (target.buffedStat == "speed")
            {
                target.speed -= target.buff;
                target.buffedStat = "";
                target.buff = 0;
            }
            
            
            
        }
    }
    public static void Poison(baseStats target, battleSystem bat)
    {
        target.StartCoroutine(poison());
        IEnumerator poison()
        {
            if (target.statusDuration > 0)
            {
                target.gameObject.GetComponent<SpriteRenderer>().color = new Color32(143, 0, 254, 255);
                float damage = Mathf.Round(target.ogHP * 0.05f);
                yield return new WaitForSeconds(1f);
                bat.battleText.text = target.nameChar + " is hurt by poison.";
                target.HP -= damage;
                target.damageText.GetComponent<Text>().text = damage.ToString();
                target.damageText.GetComponent<DamageTextEffect>().DamageStartFloating();
                target.statusDuration -= 1;
                yield return new WaitForSeconds(1f);
                if (target.gameObject.tag == "enemy")
                {
                    target.turn = true;
                    int ran = Random.Range(0, bat.lists.chars.Count);
                    bat.battleTarget = bat.lists.chars[ran];
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }

                    bat.StartCoroutine(bat.enemyAttack(bat.battleTarget));
                }
                else if (target.gameObject.tag == "Player")
                {
                    target.turn = true;
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }
                    bat.playerTurn();
                }
            }
            else
            {
                target.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                bat.battleText.text = target.nameChar + " is cured of poison.";
                target.status = null;
                yield return new WaitForSeconds(1f);
                if (target.gameObject.tag == "enemy")
                {
                    target.turn = true;
                    int ran = Random.Range(0, bat.lists.chars.Count);
                    bat.battleTarget = bat.lists.chars[ran];
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }

                    bat.StartCoroutine(bat.enemyAttack(bat.battleTarget));
                }
                else if (target.gameObject.tag == "Player")
                {
                    target.turn = true;
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }
                    bat.playerTurn();
                }
            }
            
        }
        
    }
    public static void Sleep(baseStats target, battleSystem bat)
    {
        bat.StartCoroutine(sleep());
         IEnumerator sleep()
        {
            if (target.statusDuration > 0)
            {
                bat.battleText.text = target.nameChar + " is sleeping...";
                target.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                bat.StartCoroutine(bat.sleep());
                target.statusDuration -= 1;
            }
            else
            {
                bat.battleText.text = target.nameChar + " woke up!";
                target.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                target.status = null;
                yield return new WaitForSeconds(1f);
                if (target.gameObject.tag == "enemy")
                {
                    target.turn = true;
                    int ran = Random.Range(0, bat.lists.chars.Count);
                    bat.battleTarget = bat.lists.chars[ran];
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }

                    bat.StartCoroutine(bat.enemyAttack(bat.battleTarget));
                }
                else if (target.gameObject.tag == "Player")
                {
                    target.turn = true;
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }
                    bat.playerTurn();
                }
            }
        }
        
    }
    public static void BuffDebuff(baseStats target, float buffedStat)
    {
            target.buffDuration -= 1;
    }
    public static void Confuse(baseStats target, battleSystem bat, MonoBehaviour poo)
    {
        bat.StartCoroutine(confuse());
        IEnumerator confuse()
        {
            if (target.statusDuration > 0)
            {
                bat.battleText.text = target.nameChar + " is confused!";
                yield return new WaitForSeconds(2f);
                int ran1 = Random.Range(0, 3);
                if (ran1 > 0)
                {

                    if (target.gameObject.tag == "enemy")
                    {
                        bat.attacker = target;
                        int ran = Random.Range(0, bat.lists.enemies.Count);
                        bat.battleTarget = bat.lists.enemies[ran];

                    }
                    else if (target.gameObject.tag == "Player")
                    {
                        bat.attacker = target;
                        int ran = Random.Range(0, bat.lists.chars.Count);
                        bat.battleTarget = bat.lists.chars[ran];

                    }
                    bat.damage = Mathf.Round(bat.attacker.ogHP * 0.1f);

                    
                    yield return new WaitForSeconds(2f);
                    bat.battleText.text = target.gameObject.name + " harms their own party!";
                    target.AttackSlash1();
                    yield return new WaitForSeconds(1f);
                    target.GetComponent<baseStats>().HP -= bat.damage;
                    target.GetComponent<baseStats>().damageText.gameObject.GetComponent<Text>().text = bat.damage.ToString();
                    target.GetComponent<baseStats>().damageText.gameObject.GetComponent<DamageTextEffect>().DamageStartFloating();
                    bat.StartCoroutine(bat.sleep());


                }
                else if (ran1 < 1)
                {
                    if (target.gameObject.tag == "enemy")
                    {
                        target.turn = true;
                        int ran = Random.Range(0, bat.lists.chars.Count);
                        bat.battleTarget = bat.lists.chars[ran];
                        bat.attacker = target;
                        foreach (InventorySlot slot in bat.slots)
                        {
                            slot.attacker = bat.attacker;

                        }

                        bat.StartCoroutine(bat.enemyAttack(bat.battleTarget));
                    }
                    else if (target.gameObject.tag == "Player")
                    {
                        target.turn = true;
                        bat.attacker = target;
                        foreach (InventorySlot slot in bat.slots)
                        {
                            slot.attacker = bat.attacker;

                        }
                        bat.playerTurn();
                    }
                }
                target.statusDuration -= 1;
            }
            else
            {
                target.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                bat.battleText.text = target.nameChar + " regained consciousness.";
                target.status = null;
                yield return new WaitForSeconds(1f);
                if (target.gameObject.tag == "Player")
                {
                    target.turn = true;
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }
                    Debug.Log("Turn:" + bat.attacker.name);
                    bat.playerTurn();
                    
                }
                else if (target.gameObject.tag == "enemy")
                {
                    target.turn = true;
                    int ran = Random.Range(0, bat.lists.chars.Count);
                    bat.battleTarget = bat.lists.chars[ran];
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }
                    

                    bat.StartCoroutine(bat.enemyAttack(bat.battleTarget));
                }

            }
            
        }


    }
    public static void Depression(baseStats target, battleSystem bat, MonoBehaviour baat)
    {
        baat.StartCoroutine(Depressed());
        IEnumerator Depressed()
        {
            
            if (target.statusDuration > 0)
            {
                if (target.gameObject.tag == "enemy")
                {
                    target.turn = true;
                    int ran = Random.Range(0, bat.lists.chars.Count);
                    bat.battleTarget = bat.lists.chars[ran];
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }
                    bat.damage = target.attack * .25f;
                    target.gameObject.GetComponent<Animator>().SetBool("attack", true);
                    
                }
                else if (target.gameObject.tag == "Player")
                {
                    target.turn = true;
                    int ran = Random.Range(0, bat.lists.enemies.Count);
                    bat.battleTarget = bat.lists.enemies[ran];
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }
                    bat.damage = Mathf.Round(target.attack * .25f);
                    target.gameObject.GetComponent<Animator>().SetBool("attack", true);
                    
                }
                target.statusDuration -= 1;
                bat.battleText.text = target.nameChar + " attacks weakly.";
                yield return new WaitForSeconds(2f);
                target.gameObject.GetComponent<Animator>().SetBool("attack", false);
                target.GetComponent<baseStats>().HP -= bat.damage;
                target.GetComponent<baseStats>().damageText.gameObject.GetComponent<Text>().text = bat.damage.ToString();
                target.GetComponent<baseStats>().damageText.gameObject.GetComponent<DamageTextEffect>().DamageStartFloating();
                bat.StartCoroutine(bat.sleep());
            } else
             {
                bat.battleText.text = target.nameChar + " feels fine again.";
                yield return new WaitForSeconds(2f);
                if (target.gameObject.tag == "Player")
                {
                    target.turn = true;
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }
                    Debug.Log("Turn:" + bat.attacker.name);
                    bat.playerTurn();

                }
                else if (target.gameObject.tag == "enemy")
                {
                    target.turn = true;
                    int ran = Random.Range(0, bat.lists.chars.Count);
                    bat.battleTarget = bat.lists.chars[ran];
                    bat.attacker = target;
                    foreach (InventorySlot slot in bat.slots)
                    {
                        slot.attacker = bat.attacker;

                    }


                    bat.StartCoroutine(bat.enemyAttack(bat.battleTarget));
                }
                
                target.status = null;
            }
        }
    }
    
}
