using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
[CreateAssetMenu(fileName = "BossSA", menuName = "SpecialAttacks/Enemy/Boss")]
public class BossSA : specialAttacksScript
{
    public override void SpecialAttack1(string name, baseStats attacker, baseStats target)
    {
        attacker.StartCoroutine(SA());
        IEnumerator SA()
        {
            if (attacker.SP > 2)
            {
                attacker.SP -= 3;
                foreach (GameObject button in attacker.b.lists.buttons)
                {
                    button.GetComponent<Button>().interactable = false;
                }
                attacker.b.battleText.text = attacker.nameChar + " uses " + name;
                attacker.gameObject.GetComponent<Animator>().SetBool("attack", true);
                attacker.slash = SAPrefab1;
                attacker.gameObject.GetComponent<enemyDeath>().multi = true;
                yield return new WaitForSeconds(1.2f);
                attacker.gameObject.GetComponent<Animator>().SetBool("attack", false);
                yield return new WaitForSeconds(1.5f);
                float attackUp = attacker.attack * 1.5f;
                foreach (GameObject hero in attacker.b.charStats)
                {
                    float damage = attackUp - hero.GetComponent<baseStats>().def;
                    if (damage < 1)
                    {
                        damage = 1;
                    }
                    hero.GetComponent<baseStats>().HP -= damage;
                    hero.GetComponent<baseStats>().damageText.GetComponent<Text>().text = damage.ToString();
                    hero.GetComponent<baseStats>().damageText.GetComponent<DamageTextEffect>().DamageStartFloating();
                }
                if (target.GetComponent<baseStats>().HP < 1)
                {
                    attacker.StartCoroutine(attacker.b.Die(target.GetComponent<baseStats>()));
                }
                else
                {
                    attacker.b.TurnOrder();
                }
            }
            else
            {
                int ran = Random.Range(0, attacker.b.charStats.Count);
                attacker.b.battleTarget = attacker.b.charStats[ran];
                attacker.StartCoroutine(attacker.b.enemyAttack(attacker.b.battleTarget));
            }
        }
        
    }

    public override void SpecialAttack2(string name, baseStats attacker, baseStats target)
    {
        
        attacker.StartCoroutine(SA());
        IEnumerator SA()
        {
            if (attacker.SP > 3)
            {
                foreach (GameObject button in attacker.b.lists.buttons)
                {
                    button.GetComponent<Button>().interactable = false;
                }
                attacker.b.battleText.text = attacker.nameChar + " uses " + name;
                attacker.gameObject.GetComponent<Animator>().SetBool("attack", true);


                attacker.slash = SAPrefab2;
                attacker.SP -= 4;
                yield return new WaitForSeconds(1.2f);
                
                attacker.gameObject.GetComponent<Animator>().SetBool("attack", false);
                yield return new WaitForSeconds(1.5f);
                if (target.status == "")
                {
                    target.status = "Sleep";
                    target.statusDuration = 2;
                }
                float attackUp = attacker.attack * 2f;
                float damage = attackUp - target.GetComponent<baseStats>().def;
                if (damage < 1)
                {
                    damage = 1;
                }
                target.GetComponent<baseStats>().HP -= damage;
                target.GetComponent<baseStats>().damageText.GetComponent<Text>().text = damage.ToString();
                target.GetComponent<baseStats>().damageText.GetComponent<DamageTextEffect>().DamageStartFloating();

                if (target.GetComponent<baseStats>().HP < 1)
                {
                    attacker.StartCoroutine(attacker.b.Die(target.GetComponent<baseStats>()));
                }
                else
                {

                    attacker.b.TurnOrder();
                }
            }
            else
            {
                int ran = Random.Range(0, attacker.b.charStats.Count);
                attacker.b.battleTarget = attacker.b.charStats[ran];
                attacker.StartCoroutine(attacker.b.enemyAttack(attacker.b.battleTarget));
            }

        }
    }


    public override void SpecialAttack3(string name, baseStats attacker, baseStats target)
    {
        
    }


    public override void StatusSpecialAttack1(string name, baseStats attacker, baseStats target)
    {
        attacker.StartCoroutine(SA());
        IEnumerator SA()
        {
            if (attacker.SP > 0)
            {
                float heal = 10;
                attacker.SP -= 1;
                attacker.b.battleText.text = attacker.nameChar + " uses " + name;
                target.slash = SAPrefab4;
                target.AttackSlash1();
                yield return new WaitForSeconds(1f);

                target.HP += heal;
                target.damageText.GetComponent<Text>().text = heal.ToString();
                target.damageText.GetComponent<DamageTextEffect>().HealStartFloating();
                yield return new WaitForSeconds(1f);
                attacker.b.TurnOrder();
            }
            else
            {
                int ran = Random.Range(0, attacker.b.charStats.Count);
                attacker.b.battleTarget = attacker.b.charStats[ran];
                attacker.StartCoroutine(attacker.b.enemyAttack(attacker.b.battleTarget));
            }
            
        }
    }


    public override void StatusSpecialAttack2(string name, baseStats attacker, baseStats target)
    {

    }

    public override void StatusSpecialAttack3(string name, baseStats attacker, baseStats target)
    {

    }

    public override void enemyAI(baseStats attacker)
    {
        Debug.Log(attacker.HP);
        bool phase2 = false;
        bool phase3 = false;
        float lessQuarter = 2 * (attacker.ogHP / 3); 
        float lessQuarters = attacker.ogHP / 3;
        Debug.Log(lessQuarter);
        Debug.Log(lessQuarters);
        if (attacker.HP > lessQuarter)
        {
            phase2 = false;
            phase3 = false;
        } else if (attacker.HP < lessQuarter && attacker.HP > lessQuarters)
        {
            phase2 = true;
            phase3 = false;
        } else if (attacker.HP < lessQuarter && attacker.HP < lessQuarters)
        {
            
            phase2 = false;
            phase3 = true;
        }
        List<GameObject> weakList = new List<GameObject>();
        foreach (baseStats charac in attacker.b.stats)
        {
            if (charac.gameObject.tag == "Player")
            {
                weakList.Add(charac.gameObject);
            }
        }
        int ran = Random.Range(0, weakList.Count);
        attacker.b.battleTarget = weakList[ran];
        if (phase2 == false && phase3 == false)
        {
            attacker.StartCoroutine(attacker.b.enemyAttack(attacker.b.battleTarget));
            
        } else if (phase2 == true && phase3 == false)
        {
            int rano = Random.Range(1, 5);
            if (rano == 1 || rano == 2)
            {
                attacker.StartCoroutine(attacker.b.enemyAttack(attacker.b.battleTarget));
            } else if (rano == 3)
            {
                SpecialAttack1(attacker.character.spec.physicalName1, attacker, attacker.b.battleTarget.GetComponent<baseStats>());
            } else if (rano == 4)
            {
                weakList = weakList.OrderBy(c => c.gameObject.GetComponent<baseStats>().HP).ToList();
                attacker.b.battleTarget = weakList[0];
                SpecialAttack2(attacker.character.spec.physicalName2, attacker, attacker.b.battleTarget.GetComponent<baseStats>());
            }
        } else if (phase2 == false && phase3 == true)
        {
            int rano = Random.Range(1, 7);
            if (rano == 1 || rano == 2)
            {
                attacker.StartCoroutine(attacker.b.enemyAttack(attacker.b.battleTarget));
            } else if (rano == 3 || rano == 4)
            {
                attacker.b.battleTarget = attacker.gameObject;
                StatusSpecialAttack1(attacker.character.spec.statusName1, attacker, attacker.b.battleTarget.GetComponent<baseStats>());
            }
            else if (rano == 5)
            {
                SpecialAttack1(attacker.character.spec.physicalName1, attacker, attacker.b.battleTarget.GetComponent<baseStats>());
            }
            else if (rano == 6)
            {
                weakList = weakList.OrderBy(c => c.gameObject.GetComponent<baseStats>().HP).ToList();
                attacker.b.battleTarget = weakList[0];
                SpecialAttack2(attacker.character.spec.physicalName1, attacker, attacker.b.battleTarget.GetComponent<baseStats>());
            }
        }
        /*if (attacker.HP < lessHalf)
        {
            weakList = weakList.OrderBy(c => c.gameObject.GetComponent<baseStats>().HP).ToList();
            attacker.b.battleTarget = weakList[0];
            SpecialAttack2(attacker.character.spec.physicalName1, attacker, attacker.b.battleTarget.GetComponent<baseStats>());
        }
        else
        {
            attacker.StartCoroutine(attacker.b.enemyAttack(attacker.b.battleTarget));
        } */

    }
}
