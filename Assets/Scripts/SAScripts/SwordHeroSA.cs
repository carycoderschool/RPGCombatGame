using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "SwordHeroSA", menuName = "SpecialAttacks/Hero/Sword")]
public class SwordHeroSA : specialAttacksScript
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
            attacker.b.battleText.text = attacker.name + " uses " + name;
            foreach (GameObject button in attacker.b.lists.buttons)
            {
                button.GetComponent<Button>().interactable = false;
            }
            attacker.SP -= 5;
            float upAttack = attacker.attack * .50f;
            float supAttack = attacker.attack + upAttack;
            float damage = supAttack - target.def;
            if (damage < 1)
            {
                damage = 1;
            }
            target.HP -= damage;
            attacker.HP -= upAttack;
            if (target.GetComponent<baseStats>().HP < 1)
            {
                attacker.StartCoroutine(attacker.b.Die(target.GetComponent<baseStats>()));
            }
            else
            {
                attacker.b.TurnOrder();
            }
            yield break;
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
        attacker.StartCoroutine(SA());
        IEnumerator SA()
        {
            foreach (GameObject button in attacker.b.lists.buttons)
            {
                button.GetComponent<Button>().interactable = false;
            }
            if (target.buffedStat == "" && attacker.SP > 1)
            {
                attacker.b.battleText.text = attacker.name + " uses " + name;
                foreach (GameObject button in attacker.b.lists.buttons)
                {
                    button.GetComponent<Button>().interactable = false;
                }
                attacker.b.battleTarget = target.gameObject;
                target.slash = SAPrefab4;
                target.AttackSlash1();
                yield return new WaitForSeconds(2f);
                attacker.b.battleText.text = target.name + "'s attack goes up!";
                attacker.SP -= 2;
                target.buff = 10;
                target.attack += target.buff;
                target.buffDuration = 3;
                target.buffedStat = "attack";
                yield return new WaitForSeconds(2f);
                attacker.b.TurnOrder();
            }
            else
            {
                attacker.skip = true;
            }
        }
        
    }  
    public override void StatusSpecialAttack2(string name, baseStats attacker, baseStats target)
    {
        
    }
    public override void StatusSpecialAttack3(string name, baseStats attacker, baseStats target)
    {
        
    }
    public override void enemyAI( baseStats attacker)
    {

    }
}
