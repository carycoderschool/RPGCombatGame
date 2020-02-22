using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseStats
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SpecialAttack1(string name /*, string statusEffect, string statsEffect, BaseStats attacker, BaseStats target, int damageMultiplier, int cost*/)
    {
        attacker.SP -= cost;
        float upAttack = attacker.attack *= damageMultiplier;

        float damage = upAttack - target.def;
        target.HP -= damage;

    }
}
