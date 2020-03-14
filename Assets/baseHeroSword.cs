using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseHeroSword : baseStats
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
        int random = Random.Range(0, 1);
        if (random == 0)
        {
            float attackPlus = attacker.attack * .25f;
            float damage = attacker.attack + attackPlus - target.def;
            attacker.HP -= attackPlus * 2;
            target.HP -= damage;
        }
    }
}
