using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour
{
    static List<string> Lines;
    static GameObject speakerA;
    static GameObject speakerB;
    public static Text textBox;
    static int current = 0;
    static string NPCName;
   
    // Start is called before the first frame update
   

    // Update is called once per frame
    public static void StartText(GameObject speaker)
    {
        
        if (speaker.tag == "Chest")
        {
            speakerA = speaker;
            textBox = speaker.GetComponent<chestInteract>().player.GetComponent<playerMovement>().textBox.GetComponentInChildren<Text>();
            ChestGenerate();
        } else if (speaker.tag == "NPC")
        {

            Lines = speaker.GetComponent<NPCLines>().Lines;
            textBox = speaker.GetComponent<NPCInteract>().player.GetComponent<playerMovement>().textBox.GetComponentInChildren<Text>();
            NPCName = speaker.GetComponent<NPCLines>().NPCName;
            speakerB = speaker;
            current = 0;
            NPCGenerate(speaker.GetComponent<NPCInteract>().player);
        }
    }
    static void ChestGenerate()
    {
        
        textBox.text = speakerA.GetComponent<chestInteract>().player.GetComponent<playerMovement>().nameA + " Gets the " + speakerA.GetComponent<chestInteract>().chestItem.name;
        speakerA = null;
    }
    public static void NPCGenerate(GameObject player)
    {

        Debug.Log(current);

        if (current >= Lines.Count)
        {
           
            
            Debug.Log("poo");
            
            Debug.Log(current);
            player.GetComponent<playerMovement>().buttonpress = current;
            current = 0;
            Debug.Log(Lines.Count);

        } else if (current < Lines.Count)
        {
            textBox.text = NPCName + ":" + Lines[current];
            current += 1;
            
            Debug.Log(Lines.Count);
            if (current >= Lines.Count)
            {
                
                player.GetComponent<playerMovement>().mode = "chestPause";
                player.GetComponent<playerMovement>().npc = speakerB;
            }
            
        }
        
    }
    static void UnpauseGame()
    {
        Color colorTextbox = textBox.color;
        Color colorText = textBox.gameObject.GetComponentInChildren<Text>().color;
        colorText.a = 0;
        colorTextbox.a = 0;
        textBox.color = colorTextbox;
        textBox.gameObject.GetComponentInChildren<Text>().color = colorText;
        
        Time.timeScale = 1;
    }
    void Poo()
    {

    }
    static void Unpaused()
    {
       speakerA.GetComponent<playerMovement>().UnpauseGame();
    }
}
