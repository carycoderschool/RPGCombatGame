using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseHero : baseStats
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SpecialAttack1(string name , string statusEffect, string statsEffect, baseStats attacker, baseStats target, int damageMultiplier, int cost)
    {
        
        attacker.SP -= cost;
        float upAttack = attacker.attack *= damageMultiplier;

        float damage = upAttack - target.def;
        target.HP -= damage;

    }
    public override void SpecialAttack2(string name, string statusEffect, string statseffect, baseStats attacker, baseStats target, int damageMultiplier, int cost)
    {
        attacker.SP -= cost;
        float heal = attacker.HP * .25f;
        heal = Mathf.Round(heal);
        attacker.HP += heal;
        if (attacker.HP > ogHP)
        {
            attacker.HP = ogHP;
        } 
    }
}
