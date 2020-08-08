using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New HealItem", menuName = "ItemAction/DefUpItem")]
public class itemDefUp : ItemAction
{
    public float buff;
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
        attacker.buff = buff;
        attacker.attack += attacker.buff;
        attacker.buffDuration = 3;
    }
}
