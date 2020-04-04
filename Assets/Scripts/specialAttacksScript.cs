using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class specialAttacksScript : ScriptableObject
{
    public string physicalName1;
    public string physicalName2;
    public string physicalName3;
    public string statusName1;
    public string statusName2;
    public string statusName3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public abstract void SpecialAttack1(string name, baseStats attacker, baseStats target);

    public abstract void SpecialAttack2(string name, baseStats attacker, baseStats target);


    public abstract void SpecialAttack3(string name, baseStats attacker, baseStats target);


    public abstract void StatusSpecialAttack1(string name, baseStats attacker);


    public abstract void StatusSpecialAttack2(string name, baseStats attacker);

    public abstract void StatusSpecialAttack3(string name, baseStats attacker);
    
    
}
