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
    public override void SpecialAttack1(string name,  baseStats attacker, baseStats target)
    {
        
        attacker.SP -= 3;
        float upAttack = attacker.attack *= 2;

        float damage = upAttack - target.def;
        target.HP -= damage;

    }
    public override void StatusSpecialAttack1(string name, baseStats attacker)
    {
        attacker.SP -= 3;
        float heal = 20;
        heal = Mathf.Round(heal);
        attacker.HP += heal;
        if (attacker.HP > ogHP)
        {
            attacker.HP = ogHP;
        } 
    }
}
