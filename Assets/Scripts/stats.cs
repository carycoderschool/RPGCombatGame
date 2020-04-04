using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Char", menuName = "Character")]
public class stats : ScriptableObject
{
    public specialAttacksScript spec;
    public Sprite icon;
    public float HP;
    public float SP;
    public float def;
    public float attack;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
