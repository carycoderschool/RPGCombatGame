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
            
            Poison(target);
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
    public static void Poison(baseStats target)
    {
        
        if (target.statusDuration > 0)
        {
            target.gameObject.GetComponent<SpriteRenderer>().color = new Color32(143, 0, 254, 255);
            float damage = Mathf.Round(target.HP * 0.05f);
            target.HP -= damage;
            target.statusDuration -= 1;
        } else
        {
            target.status = null;
        }
    }
    public static void Sleep(baseStats target, battleSystem bat)
    {
        if (target.statusDuration > 0)
        {
            target.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            bat.StartCoroutine(bat.sleep());
            target.statusDuration -= 1;
        }
        else
        {
            target.status = null;
        }
    }
    public static void BuffDebuff(baseStats target, float buffedStat)
    {
            target.buffDuration -= 1;
    }
    public static void Confuse(baseStats target, battleSystem bat, MonoBehaviour poo)
    {
        if (target.statusDuration > 0)
        {
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
                Debug.Log(bat.damage);
                poo.StartCoroutine(Wait(target, bat));
                

            } else if (ran1 < 1) 
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
                target.status = null;
            } else if (target.gameObject.tag == "enemy")
            {
                target.turn = true;
                int ran = Random.Range(0, bat.lists.chars.Count);
                bat.battleTarget = bat.lists.chars[ran];
                bat.attacker = target;
                foreach (InventorySlot slot in bat.slots)
                {
                    slot.attacker = bat.attacker;

                }
                target.status = null;

                bat.StartCoroutine(bat.enemyAttack(bat.battleTarget));
            }
            
        }
        IEnumerator Wait(baseStats targett, battleSystem batt)
        {
            yield return new WaitForSeconds(2f);
            targett.AttackSlash1();
            yield return new WaitForSeconds(1f);
            targett.GetComponent<baseStats>().HP -= batt.damage;
            targett.GetComponent<baseStats>().damageText.gameObject.GetComponent<Text>().text = batt.damage.ToString();
            targett.GetComponent<baseStats>().damageText.gameObject.GetComponent<DamageTextEffect>().StartFloating();
            batt.StartCoroutine(batt.sleep());
        }


    }
    public static void Depression(baseStats target, battleSystem bat, MonoBehaviour baat)
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
                target.gameObject.GetComponent<Animator>().SetBool("enemyAttacks", true);
                baat.StartCoroutine(Wait(bat.battleTarget.GetComponent<baseStats>(), bat));
            } else if (target.gameObject.tag == "Player")
            {
                target.turn = true;
                int ran = Random.Range(0, bat.lists.enemies.Count);
                bat.battleTarget = bat.lists.enemies[ran];
                foreach (InventorySlot slot in bat.slots)
                {
                    slot.attacker = bat.attacker;

                }
                bat.damage = target.attack * .25f;
                target.gameObject.GetComponent<Animator>().SetBool("attack", true);
                baat.StartCoroutine(Wait(bat.battleTarget.GetComponent<baseStats>(), bat));
            }
            target.statusDuration -= 1;

        } else
        {
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
        IEnumerator Wait(baseStats targett, battleSystem batt)
        {
            yield return new WaitForSeconds(2f);
            targett.GetComponent<baseStats>().HP -= batt.damage;
            targett.GetComponent<baseStats>().damageText.gameObject.GetComponent<Text>().text = batt.damage.ToString();
            targett.GetComponent<baseStats>().damageText.gameObject.GetComponent<DamageTextEffect>().StartFloating();
            batt.StartCoroutine(batt.sleep());
        }
    }
    
}
