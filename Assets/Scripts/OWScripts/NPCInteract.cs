using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : interactScript
{
    public bool quest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    public override void Interact()
    {
        interactable = true;
        player.GetComponent<playerMovement>().isGamePaused = true;
        player.GetComponent<playerMovement>().mode = "TalkNPC";
        
            TextGenerator.StartText(gameObject);
    }
}
