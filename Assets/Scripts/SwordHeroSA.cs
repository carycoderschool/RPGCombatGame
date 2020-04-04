using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        
        attacker.SP -= 5;
        float upAttack = attacker.attack * .50f;
        float supAttack = attacker.attack += upAttack;
        float damage = upAttack - target.def;
        
        target.HP -= damage;
        attacker.HP -= supAttack;
    }
    public override void SpecialAttack2(string name, baseStats attacker, baseStats target)
    {
        target.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        target.status = "Sleep";
        target.statusDuration = 3;
    }
    public override void SpecialAttack3(string name, baseStats attacker, baseStats target)
    {

    }
    public override void StatusSpecialAttack1(string name, baseStats attacker)
    {
        Debug.Log("epic");
        if (attacker.buffedStat == "")
        {
            attacker.SP -= 2;
            attacker.buff = 10;
            attacker.attack += attacker.buff;
            attacker.buffDuration = 1;
            attacker.buffedStat = "attack";
            
        } else
        {
            attacker.skip = true;
        }
        
    }
    public override void StatusSpecialAttack2(string name, baseStats attacker)
    {
        
    }
    public override void StatusSpecialAttack3(string name, baseStats attacker)
    {

    }
}
