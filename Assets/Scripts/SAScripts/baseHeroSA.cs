using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "baseHero", menuName = "SpecialAttacks/Hero/Base")]
public class baseHeroSA : specialAttacksScript
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
        attacker.StartCoroutine(SA1());
        IEnumerator SA1()
        {
            if (attacker.SP > 3)
            {
                foreach (GameObject button in attacker.b.lists.buttons)
                {
                    button.GetComponent<Button>().interactable = false;
                }
                attacker.b.battleTarget = target.gameObject;
                attacker.slash = SAPrefab1;
                attacker.SP -= 3;
                float upAttack = attacker.attack *= 2;
                float damage = upAttack - target.def;
                attacker.b.battleText.text = attacker.name + " uses " + name;
                attacker.gameObject.GetComponent<Animator>().SetBool("attack", true);
                yield return new WaitForSeconds(1.2f);

                attacker.gameObject.GetComponent<Animator>().SetBool("attack", false);
                yield return new WaitForSeconds(1f);

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
                attacker.skip = true;
            }
        }
        
        
    }
    public override void SpecialAttack2(string name, baseStats attacker, baseStats target)
    {
        
    }
    public override void SpecialAttack3(string name, baseStats attacker, baseStats target)
    {
        
    }
    public override void StatusSpecialAttack1(string name, baseStats attacker, baseStats target)
    {
        attacker.StartCoroutine(SSA1());
        IEnumerator SSA1()
        {
            if (attacker.SP > 2)
            {
                foreach (GameObject button in attacker.b.lists.buttons)
                {
                    button.GetComponent<Button>().interactable = false;
                }
                attacker.b.battleTarget = target.gameObject;
                target.slash = SAPrefab4;
                attacker.SP -= 3;
                float heal = 20;
                heal = Mathf.Round(heal);
                target.HP += heal;
                if (target.HP > target.ogHP)
                {
                    target.HP = target.ogHP;
                }
                attacker.b.battleText.text = attacker.name + " uses " + name;
                target.AttackSlash1();
                yield return new WaitForSeconds(1f);
                target.damageText.GetComponent<Text>().text = heal.ToString();
                target.damageText.GetComponent<DamageTextEffect>().HealStartFloating();
                yield return new WaitForSeconds(1f);
                target.b.TurnOrder();
            }
            else
            {
                if (attacker.SP > 0)
                {
                    attacker.SP -= 1;
                    attacker.StartCoroutine(attacker.b.Revive(target));
                }
                else
                {
                    attacker.skip = true;
                }
            }
        }
    }
    public override void StatusSpecialAttack2(string name, baseStats attacker, baseStats target)
    {
        target.HP = target.ogHP;
        attacker.StartCoroutine(attacker.b.Revive(target));
    }
    
    public override void StatusSpecialAttack3(string name, baseStats attacker, baseStats target)
    {
        
    }
    public override void enemyAI( baseStats attacker)
    {
        
    }
}
