using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterStats : MonoBehaviour
{
    public float HP;
    public float SP;
    public float def;
    public float attack;
    public float speed;
    public Text sp;
    public Text hp;
    float ogHP;
    float ogSP;

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

}
