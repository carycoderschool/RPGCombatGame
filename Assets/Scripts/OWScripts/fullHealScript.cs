using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fullHealScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(DoThing());
        IEnumerator DoThing()
        {
            foreach (baseStats gamer in GlobalManager.instance.currentParty)
            {
                gamer.HP = gamer.ogHP;
                gamer.SP = gamer.ogSP;
            }
            yield return new WaitForSecondsRealtime(2f);
            Destroy(gameObject);
        }
        
    }
}
