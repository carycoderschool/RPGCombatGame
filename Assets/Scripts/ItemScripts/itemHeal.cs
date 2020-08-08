using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HealItem", menuName = "ItemAction/HealItem")]
public class itemHeal : ItemAction
{
    public int heal;
    public override void Act(baseStats attacker)
    {
        attacker.HP += heal;
        if (attacker.HP > attacker.ogHP)
        {
            attacker.HP = attacker.ogHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
