using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "SpiderSA", menuName = "SpecialAttacks/Enemy/Spider")]
public class SpiderSA : specialAttacksScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SpecialAttack1(string name, baseStats attacker, baseStats target)
    {
        attacker.StartCoroutine(SA());
        IEnumerator SA()
        {
            if (attacker.SP > 2)
            {
                foreach (GameObject button in attacker.b.lists.buttons)
                {
                    button.GetComponent<Button>().interactable = false;
                }
                attacker.b.battleTarget = target.gameObject;
                attacker.slash = SAPrefab1;
                attacker.SP -= 1;
                float upAttack = attacker.attack + 2;
                float damage = upAttack - target.def;
                if (damage < 1)
                {
                    damage = 1;
                }
                attacker.b.battleText.text = attacker.gameObject.name + " uses " + name;
                attacker.gameObject.GetComponent<Animator>().SetBool("attack", true);
                yield return new WaitForSeconds(1.2f);
                if (target.status == "")
                {
                    
                    target.status = "Poison";
                    
                    target.statusDuration = 3;
                }
                attacker.gameObject.GetComponent<Animator>().SetBool("attack", false);
                yield return new WaitForSeconds(1.5f);

                target.HP -= damage;
                target.damageText.GetComponent<Text>().text = damage.ToString();
                target.damageText.GetComponent<DamageTextEffect>().DamageStartFloating();

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
            if (attacker.SP > 1)
            {
                foreach (GameObject button in attacker.b.lists.buttons)
                {
                    button.GetComponent<Button>().interactable = false;
                }
                attacker.b.battleTarget = target.gameObject;
                attacker.slash = SAPrefab1;
                attacker.SP -= 2;
                float upAttack = attacker.attack + 2;
                float damage = upAttack - target.def;
                if (damage < 1)
                {
                    damage = 1;
                }
                attacker.b.battleText.text = attacker.gameObject.name + " uses " + name;
                attacker.gameObject.GetComponent<Animator>().SetBool("attack", true);
                yield return new WaitForSeconds(1.2f);
                if (target.status == "")
                {
                    target.status = "Confused";
                    
                    target.statusDuration = 4;
                }
                attacker.gameObject.GetComponent<Animator>().SetBool("attack", false);
                yield return new WaitForSeconds(1.5f);

                target.HP -= damage;
                target.damageText.GetComponent<Text>().text = damage.ToString();
                target.damageText.GetComponent<DamageTextEffect>().DamageStartFloating();

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

    }


    public override void StatusSpecialAttack2(string name, baseStats attacker, baseStats target)
    {

    }

    public override void StatusSpecialAttack3(string name, baseStats attacker, baseStats target)
    {

    }
    public override void enemyAI(baseStats attacker)
    {
        int ran = Random.Range(0, attacker.b.charStats.Count);
        attacker.b.battleTarget = attacker.b.charStats[ran];
        if (attacker.SP > 1)
        {
            //int ran2 = Random.Range(1, 3);
            //if (ran2 == 1)
            //{
                SpecialAttack1(attacker.character.spec.physicalName1, attacker, attacker.b.battleTarget.GetComponent<baseStats>());
            //} else if (ran2 == 2)
            //{
                //SpecialAttack2(attacker.character.spec.physicalName2, attacker, attacker.b.battleTarget.GetComponent<baseStats>());
            //}
        } else
        {
            attacker.StartCoroutine(attacker.b.enemyAttack(attacker.b.battleTarget));
        }
    }
}
