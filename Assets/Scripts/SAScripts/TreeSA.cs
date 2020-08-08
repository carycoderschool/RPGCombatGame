using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "TreeSA", menuName = "SpecialAttacks/Enemy/Tree")]
public class TreeSA : specialAttacksScript
{
    public override void SpecialAttack1(string name, baseStats attacker, baseStats target)
    {
        

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
            if (attacker.SP > 4)
            {
                float heal = attacker.b.battleTarget.GetComponent<baseStats>().ogHP * .1f;
                attacker.SP -= 5;
                attacker.b.battleText.text = attacker.gameObject.name + " uses " + name;
                target.slash = SAPrefab4;
                target.AttackSlash1();
                yield return new WaitForSeconds(1f);
                heal = Mathf.Round(heal);
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

        int ran = Random.Range(0, attacker.b.charStats.Count);
        attacker.b.battleTarget = attacker.b.charStats[ran];
        int attack = 0;
        List<GameObject> weakList = new List<GameObject>();
        foreach (baseStats charac in attacker.b.stats)
        {
            if (charac.gameObject.tag == "enemy")
            {
                weakList.Add(charac.gameObject);
            }
        }
        for (int i = 0; i < weakList.Count; i++)
        {
            
            float lessHalf = weakList[i].GetComponent<baseStats>().ogHP / 2;
            if (weakList[i].GetComponent<baseStats>().HP < lessHalf && attacker.SP > 0)
            {
                attacker.b.battleTarget = attacker.b.lists.enemies[i];
                StatusSpecialAttack1(attacker.character.spec.statusName1, attacker, attacker.b.lists.enemies[i].GetComponent<baseStats>());
                
            } else
            {
                attack += 1;
               
            }
        }
      if(attack == weakList.Count)
        {
            attacker.StartCoroutine(attacker.b.enemyAttack(attacker.b.battleTarget));
        }
        
    }
}
