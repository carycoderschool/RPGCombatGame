using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        attacker.SP -= 3;
        float upAttack = attacker.attack *= 2;
        float damage = upAttack - target.def;
        target.HP -= damage;
        if (attacker.status == "")
        {
            //target.gameObject.GetComponent<SpriteRenderer>().color = new Color32(143, 0, 254, 255);
            target.status = "Depressed";
            target.statusDuration = 3;
        } 
                
        
        
    }
    public override void SpecialAttack2(string name, baseStats attacker, baseStats target)
    {

    }
    public override void SpecialAttack3(string name, baseStats attacker, baseStats target)
    {

    }
    public override void StatusSpecialAttack1(string name, baseStats attacker)
    {
        attacker.SP -= 3;
        float heal = 20;
        heal = Mathf.Round(heal);
        attacker.HP += heal;
        if (attacker.HP > attacker.ogHP)
        {
            attacker.HP = attacker.ogHP;
        }
    }
    public override void StatusSpecialAttack2(string name, baseStats attacker)
    {
        attacker.status = "Confused";
        attacker.statusDuration = 3;
    }
    public override void StatusSpecialAttack3(string name, baseStats attacker)
    {

    }
}
