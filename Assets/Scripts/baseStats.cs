using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class baseStats : MonoBehaviour
{
    public float importance;
    public GameObject slash;
    public float buff;
    public stats character;
    public Text sp;
    public Text hp;
    public battleSystem b;
    public GameObject damageText;
    public string nameChar;
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
    public bool revive1;
    public bool revive2;
    public bool revive3;
    [HideInInspector] public float ogHP;
    [HideInInspector] public float ogSP;
     public bool skip = false;
    public stats state;

    // Start is called before the first frame update
    void Start()
    {
       
        if (character != null)
        {
            nameChar = character.nameChar;
            gameObject.GetComponent<SpriteRenderer>().sprite = character.icon;
            HP = character.HP;
            SP = character.SP;
            def = character.def;
            attack = character.attack;
            speed = character.speed;
            ogHP = character.ogHP;
            ogSP = character.ogSP;
            revive1 = character.spec.revive1;
            revive2 = character.spec.revive2;
            revive3 = character.spec.revive3;
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
    private void Update()
    {
        if (SP < 0)
        {
            SP = 0;
        }
        UpdateCurrentState();
    }
    public void AttackSlash1()
    {
        Vector3 babb = b.battleTarget.transform.position;
        GameObject sslash = Instantiate(slash);
        sslash.transform.position = b.battleTarget.transform.position;
    }
    public IEnumerator AnimationWait()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().SetBool("attack", false);
    }
    void UpdateCurrentState()
    {
        if (character != null)
        {
            state.nameChar = nameChar;
            state.HP = HP;
            state.SP = SP;
            state.attack = attack;
            state.def = def;
            state.speed = speed;
            state.ogHP = ogHP;
            state.ogSP = ogSP;
            state.spec = character.spec;
            state.icon = character.icon;
        }
        
    }
}
