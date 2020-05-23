using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public List<baseStats> partyOverworld;
    public GameObject npc;
    public bool poo = false;
    public bool isGamePaused;
    public string nameA;
    public string mode = "gameplay";
    float hor;
    float ver;
    public float speed;
    public float sprintMeter;
    float ogSM;
    public GameObject textParent;
    public Image textBox;
    public int buttonpress;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(textParent);
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Fire1"))
        {

        }
        if (mode == "chestPause")
        {

            if (Input.GetButtonDown("Jump"))
            {
                if (isGamePaused == false)
                {
                    PauseGame();
                    
                }
                else
                {
                   
                       
                    UnpauseGame();
                }
            } 
        } else if (mode == "TalkNPC")
        {
            
            StartCoroutine(WaitNPC(0.1f));
            
        }
    }
    private void FixedUpdate()
    {
        if (isGamePaused == false)
        {
            Vector2 move = new Vector2(hor, ver);
            transform.Translate(move * Time.fixedDeltaTime * speed);
        } else
        {

        }
        
        
    }
    public void UnpauseGame()
    {
        if (mode == "TalkNPC" || mode == "chestPause")
        {
            Color colorTextbox = textBox.color;
            Color colorText = textBox.gameObject.GetComponentInChildren<Text>().color;
            colorText.a = 0;
            colorTextbox.a = 0;
            textBox.color = colorTextbox;
            textBox.gameObject.GetComponentInChildren<Text>().color = colorText;
            mode = "gameplay";
        }
        if (npc != null)
        {
            StartCoroutine(NPCReset());
        }
        Time.timeScale = 1;
        isGamePaused = false;
    }
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        
    }
    IEnumerator WaitNPC(float waitTime)
    {
        
        if (poo == false)
        {
            poo = true;
            yield return new WaitForSecondsRealtime(waitTime);
            /*if (Input.GetButtonDown("Jump"))
            {
                
                    poo = false;
                    TextGenerator.NPCGenerate(gameObject);
                    buttonpress += 1;
                
            }*/
            poo = true;
        }
        else if (poo == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                
                
                    poo = false;
                    TextGenerator.NPCGenerate(gameObject);
                    buttonpress += 1;
                
            }
            
        }
        
    }
    IEnumerator NPCReset()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        
            npc.GetComponent<NPCInteract>().interactable = false;
            npc = null;
        
    }
}
