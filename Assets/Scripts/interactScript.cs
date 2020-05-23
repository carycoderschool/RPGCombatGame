using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class interactScript : MonoBehaviour
{
    
    public GameObject player;
    public bool interactable;
    public float dist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Overworld Player");
        dist = Vector2.Distance(gameObject.transform.position, player.transform.position);
        if (dist < 1.5 && Input.GetKeyDown(KeyCode.Space) && interactable == false)
        {

            Color colorTextbox = player.GetComponent<playerMovement>().textBox.color;
            Color colorText = player.GetComponent<playerMovement>().textBox.gameObject.GetComponentInChildren<Text>().color;
            colorText.a = 225;
            colorTextbox.a = 225;
            player.GetComponent<playerMovement>().textBox.color = colorTextbox;
            player.GetComponent<playerMovement>().textBox.gameObject.GetComponentInChildren<Text>().color = colorText;
            Interact();
        }
    }
    public abstract void Interact();
    
}
