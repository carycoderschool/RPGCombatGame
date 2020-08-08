using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class specialAttacksScript : ScriptableObject
{
    public string physicalName1;
    public string physicalName2;
    public string physicalName3;
    public string statusName1;
    public string statusName2;
    public string statusName3;
    public GameObject SAPrefab1;
    public GameObject SAPrefab2;
    public GameObject SAPrefab3;
    public GameObject SAPrefab4;
    public GameObject SAPrefab5;
    public GameObject SAPrefab6;
    public bool revive1;
    public bool revive2;
    public bool revive3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public abstract void SpecialAttack1(string name, baseStats attacker, baseStats target);

    public abstract void SpecialAttack2(string name, baseStats attacker, baseStats target);


    public abstract void SpecialAttack3(string name, baseStats attacker, baseStats target);


    public abstract void StatusSpecialAttack1(string name, baseStats attacker, baseStats target);


    public abstract void StatusSpecialAttack2(string name, baseStats attacker, baseStats target);

    public abstract void StatusSpecialAttack3(string name, baseStats attacker, baseStats target);

    public abstract void enemyAI(baseStats attacker);
}
