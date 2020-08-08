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
    public float experience;
    public float nextExp;
    public float defeatExp;
    public float level;
    public bool speedUp = false;
    public bool multi = false;
    public bool turn;
    public bool revive1;
    public bool revive2;
    public bool revive3;
    [HideInInspector] public float ogHP;
    [HideInInspector] public float ogSP;
    public bool skip = false;
    public stats state;
    public bool on;
    public bool overworld;
    // Start is called before the first frame update
    public void Start()
    {
       
        if (character != null)
        {
            nameChar = character.nameChar;
            
            if (overworld == false)
            {
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = character.icon;
                gameObject.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = character.anim;
            } else if (overworld == true)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = character.icon;
            }
            
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
            nextExp = character.nextExp;
            defeatExp = character.defeatExp;
            experience = character.experience;
            level = character.level;
        }
        on = true;
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
        if (on == true)
        {
            UpdateCurrentState();
        } else
        {

        }
        
    }
    public void AttackSlash1()
    {
        if (multi == false)
        {
            Vector3 babb = b.battleTarget.transform.position;
            GameObject sslash = Instantiate(slash);
            sslash.transform.position = b.battleTarget.transform.position;
        } else if (multi == true)
        {
            Debug.Log("p");
            foreach (GameObject hero in b.charStats)
            {
                Vector3 babb = hero.transform.position;
                GameObject sslash = Instantiate(slash);
                sslash.transform.position = hero.transform.position;
            }
            multi = false;
        }
        
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
            if (speedUp == true)
            {
                state.speed = speed - 1;
            } else
            {
                state.speed = speed;
            }
            state.ogHP = ogHP;
            state.ogSP = ogSP;
            state.spec = character.spec;
            state.icon = character.icon;
            state.anim = character.anim;
            state.experience = experience;
            state.nextExp = nextExp;
            state.level = level;
            //Debug.Log("update" + gameObject.name);
        }
    }
    public void StatUp()
    {
        int ran = Random.Range(1, 6);
        ogHP += 5;
        ogSP += 2;
        def += 2;
        attack += 2;
        speed += 2;
        switch(ran)
        {
            case 1:
                ogHP += 3;
                break;
            case 2:
                ogSP += 1;
                break;
            case 3:
                attack += 2;
                break;
            case 4:
                def += 2;
                break;
            case 5:
                speed += 2;
                break;
        }
    }
}
