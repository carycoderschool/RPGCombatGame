using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    public List<stats> encounter1;
    public List<stats> encounter2;
    public List<stats> encounter3;
    public bool on;
    public bool boss;
    public AudioClip battleMusic;
    // Start is called before the first frame update
    void Start()
    {
        //on = false;
        if (boss == false)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<enemyMovement_Roaming>().enabled = false;
        } else
        {
            TurnOn();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (on == true)
        {
            DontDestroyOnLoad(gameObject.transform.parent.gameObject);
        } else
        {
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
            if (collision.gameObject.tag == "Overworld Player" || collision.gameObject.tag == "party")
            {
                if (collision.gameObject.tag == "Overworld Player")
                {
                    collision.gameObject.GetComponent<playerMovement>().speed = 0;
                Debug.Log("er");
                }

        

            
                int ran = Random.Range(1, 4);
                
                GlobalManager.instance.BattleTransition(this);
                
            }
        
    }
   public void TurnOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        if (boss == false)
        {
            GetComponent<enemyMovement_Roaming>().enabled = true;
        }
        
    }
}
