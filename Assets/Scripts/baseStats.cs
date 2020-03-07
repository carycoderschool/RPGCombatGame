using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class baseStats : MonoBehaviour
{
    
    public string physicalName1;
    public string physicalName2;
    public string physicalName3;
    public string statusName1;
    public string statusName2;
    public string statusName3;
    public float HP;
    public float SP;
    public float def;
    public float attack;
    public float speed;
    public bool turn;
    public Text sp;
    public Text hp;
    public float ogHP;
    public float ogSP;
    

    // Start is called before the first frame update
    void Start()
    {
        ogHP = HP;
        ogSP = SP;
    }

    // Update is called once per frame
    public void UpdateDisplay()
    {
        sp.text = "SP:" + SP.ToString() + "/" + ogSP.ToString();
        hp.text = "HP:" + HP.ToString() + "/" + ogHP.ToString();

    }
    
    public virtual void SpecialAttack1(string name , baseStats attacker, baseStats target)
    {
        
    }
    public virtual void SpecialAttack2(string name, baseStats attacker, baseStats target)
    {

    }
    public virtual void SpecialAttack3(string name, baseStats attacker, baseStats target)
    {

    }
    public virtual void StatusSpecialAttack1(string name, baseStats attacker)
    {

    }
    public virtual void StatusSpecialAttack2(string name, baseStats attacker)
    {

    }
    public virtual void StatusSpecialAttack3(string name, baseStats attacker)
    {

    }
}
