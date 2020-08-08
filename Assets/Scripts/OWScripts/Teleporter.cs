using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public Image panel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Epic());
        IEnumerator Epic()
        {
            if (collision.gameObject.tag == "Overworld Player")
            {
                //player.transform.position = destination.position;
                foreach (baseStats stat in GlobalManager.instance.currentParty)
                {
                    stat.transform.position = destination.position;
                }
                Color gaming = panel.color;
                gaming.a = 1;
                panel.color = gaming;
                yield return new WaitForSecondsRealtime(6f);
                gaming.a = 0;
                panel.color = gaming;
            }
        }
        
    }
}
