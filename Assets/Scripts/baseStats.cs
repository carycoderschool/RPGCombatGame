using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class baseStats : MonoBehaviour
{

    public float buff;
    public stats character;
    public Text sp;
    public Text hp;
    public battleSystem b;
    public GameObject damageText;
    public string status;
    public bool buffed = false;
    public string buffedStat;
    public float statusDuration;
    public float buffDuration;
    public float HP;
    public float SP;
    public float def;
    public float attack;
    public float speed;
     public bool turn;
    [HideInInspector] public float ogHP;
    [HideInInspector] public float ogSP;
     public bool skip = false;


    // Start is called before the first frame update
    void Start()
    {
       
        if (character != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = character.icon;
            HP = character.HP;
            SP = character.SP;
            def = character.def;
            attack = character.attack;
            speed = character.speed;
            ogHP = HP;
            ogSP = SP;
        }
        
    }

    // Update is called once per frame
    public void UpdateDisplay()
    {
        if (character != null)
        {
            sp.text = "SP:" + SP.ToString() + "/" + ogSP.ToString();
            hp.text = "HP:" + HP.ToString() + "/" + ogHP.ToString();
        } else
        {
            sp.enabled = false;
            hp.enabled = false;
        }
        

    }
    public void AttackSlash1()
    {
        Vector3 babb = b.battleTarget.transform.position;
        GameObject slash = Instantiate(b.slashPrefab);
        slash.transform.position = b.battleTarget.transform.position;
    }

}
