using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    public List<stats> encounter1;
    public List<stats> encounter2;
    public List<stats> encounter3;
    //bool on;
    // Start is called before the first frame update
    void Start()
    {
        //on = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<enemyMovement_Roaming>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
            if (collision.gameObject.tag == "Overworld Player" || collision.gameObject.tag == "party")
            {
                SceneManager.LoadScene("BattleScene");
                int ran = Random.Range(1, 4);

                GlobalManager.instance.BattleTransition(this);
            }
        
    }
   public void TurnOn()
    {
        Debug.Log("on");
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<enemyMovement_Roaming>().enabled = true;
    }
}
