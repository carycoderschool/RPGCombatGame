using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class baseStats : MonoBehaviour
{
    
    public string name1;
    public string name2;
    public string name3;
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
    void UpdateDisplay()
    {
        sp.text = "SP:" + SP.ToString() + "/" + ogSP.ToString();
        hp.text = "HP:" + HP.ToString() + "/" + ogHP.ToString();
    }
    public virtual void SpecialAttack1(string name , string statusEffect, string statseffect, baseStats attacker, baseStats target, int damageMultiplier, int cost)
    {
        
    }
    public virtual void SpecialAttack2(string name, string statusEffect, string statseffect, baseStats attacker, baseStats target, int damageMultiplier, int cost)
    {

    }
    public virtual void SpecialAttack3(string name, string statusEffect, string statseffect, baseStats attacker, baseStats target, int damageMultiplier, int cost)
    {

    }

}
