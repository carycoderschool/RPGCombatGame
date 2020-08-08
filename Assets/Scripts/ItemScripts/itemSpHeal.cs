using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New HealItem", menuName = "ItemAction/HealSPItem")]
public class itemSpHeal : ItemAction
{
    public float heal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Act(baseStats attacker)
    {
        attacker.SP += heal;
        if (attacker.SP > attacker.ogSP)
        {
            attacker.SP = attacker.ogSP;
        }
    }
}
