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
    public GameObject menuPanel;
    public Image menu;
    public GameObject buttonParent;
    Image[] buttons;
    public Image[] icons;
    Text[] buttonText;
    Text[] stats;
    public RectTransform itemsParent;
    public InventorySlot[] slots;
    Image[] slotSprites;
    public delegate void ItemDelegate(baseStats target);
    ItemDelegate itemUse;
    public Transform itemButtonParent;
    public Button[] itemButtons;
    public Button backButton;
    public Animator anim;
    public Animator partyAnim;
    public float horVelocity;
    public float verVelocity;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(textParent);
        itemsParent.GetComponent<Image>().enabled = false;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slotSprites = itemsParent.GetComponentsInChildren<Image>();
        foreach (InventorySlot slot in slots)
        {
            slot.gameObject.GetComponentInChildren<Button>().interactable = false;
            slot.removeButton.interactable = false;
            slot.removeButton.gameObject.GetComponent<Image>().enabled = false;
            slot.icon.enabled = false;
            slot.color.a = 0;
            slot.gameObject.GetComponentInChildren<Image>().enabled = false;
        }
        buttons = buttonParent.GetComponentsInChildren<Image>();
        Color32 menuColor = menu.color;
        menuColor.a = 0;
        menu.color = menuColor;
        foreach (Image button in buttonParent.GetComponentsInChildren<Image>())
        {
            Color32 textColor = button.GetComponentInChildren<Text>().color;
            textColor.a = 0;
           
            Color32 buttonColor = button.color;
            buttonColor.a = 0;
            button.color = buttonColor;
            button.GetComponentInChildren<Text>().color = textColor;
            button.gameObject.GetComponent<Button>().interactable = false;
        }
        icons = menuPanel.GetComponentsInChildren<Image>();
        foreach (Image icon in icons)
        {
            Color32 iconColor = icon.color;
            iconColor.a = 0;
            icon.color = iconColor;
            stats = icon.GetComponentsInChildren<Text>();
            foreach (Text stat in stats)
            {
                Color32 statColor = stat.color;
                statColor.a = 0;
                stat.color = statColor;
            }
        }
        itemButtons = itemButtonParent.GetComponentsInChildren<Button>();
        foreach(Button button in itemButtons)
        {
            button.GetComponent<Image>().enabled = false;
            button.interactable = false;
            button.GetComponentInChildren<Text>().enabled = false;
        }
        backButton.onClick.AddListener(Back);
        backButton.interactable = false;
        backButton.GetComponent<Image>().enabled = false;
        backButton.GetComponentInChildren<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        if (isGamePaused == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PauseGame();
                Color32 menuColor = menu.color;
                menuColor.a = 255;
                menu.color = menuColor;
                foreach (Image button in buttonParent.GetComponentsInChildren<Image>())
                {
                    Color32 textColor = button.GetComponentInChildren<Text>().color;
                    textColor.a = 255;
                    Color32 buttonColor = button.color;
                    buttonColor.a = 255;
                    button.color = buttonColor;
                    button.GetComponentInChildren<Text>().color = textColor;
                    button.gameObject.GetComponent<Button>().interactable = true;
                }
            }
        } else if (isGamePaused == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                UnpauseGame();
                Color32 menuColor = menu.color;
                menuColor.a = 0;
                menu.color = menuColor;
                foreach (Image button in buttonParent.GetComponentsInChildren<Image>())
                {
                    Color32 textColor = button.GetComponentInChildren<Text>().color;
                    textColor.a = 0;
                    Color32 buttonColor = button.color;
                    buttonColor.a = 0;
                    button.color = buttonColor;
                    button.GetComponentInChildren<Text>().color = textColor;
                    button.gameObject.GetComponent<Button>().interactable = false;
                }
                foreach (Image icon in icons)
                {
                    Color32 iconColor = icon.color;
                    iconColor.a = 0;
                    icon.color = iconColor;
                    stats = icon.GetComponentsInChildren<Text>();
                    foreach (Text stat in stats)
                    {
                        Color32 statColor = stat.color;
                        statColor.a = 0;
                        stat.color = statColor;
                    }
                }
                foreach (Button button in itemButtons)
                {
                    button.GetComponent<Image>().enabled = false;
                    button.interactable = false;
                    button.GetComponentInChildren<Text>().enabled = false;
                }
                foreach (InventorySlot slot in slots)
                {
                    slot.gameObject.GetComponentInChildren<Button>().interactable = false;
                    slot.removeButton.interactable = false;
                    slot.removeButton.gameObject.GetComponent<Image>().enabled = false;
                    slot.icon.enabled = false;
                    slot.color.a = 0;
                    slot.gameObject.GetComponentInChildren<Image>().enabled = false;
                }
            }
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
        /*if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            float lastSpeed = speed;
            speed = 0;
            StartCoroutine(wait());
            speed = lastSpeed;
            IEnumerator wait()
            {
                yield return new WaitForSeconds(.1f);
            }
        }*/
    }
    private void FixedUpdate()
    {
        
        if (isGamePaused == false)
        {
            Vector2 move = new Vector2(hor, ver);
            horVelocity = move.x;
            verVelocity = move.y;
            transform.Translate(move * Time.fixedDeltaTime * speed);
        } else
        {

        }
        
        
        if (horVelocity < 0)
        {

            anim.SetBool("left", true);
            anim.SetBool("right", false);
            anim.SetBool("up", false);
            anim.SetBool("down", false);

            partyAnim.SetBool("left", true);
            partyAnim.SetBool("right", false);
            partyAnim.SetBool("up", false);
            partyAnim.SetBool("down", false);
        }
        else if (horVelocity > 0)
        {

            anim.SetBool("left", false);
            anim.SetBool("right", true);
            anim.SetBool("up", false);
            anim.SetBool("down", false);

            partyAnim.SetBool("left", false);
            partyAnim.SetBool("right", true);
            partyAnim.SetBool("up", false);
            partyAnim.SetBool("down", false);
        }
        if (horVelocity == 0 && verVelocity == 0)
        {
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("up", false);
            anim.SetBool("down", false);

            partyAnim.SetBool("left", false);
            partyAnim.SetBool("right", false);
            partyAnim.SetBool("up", false);
            partyAnim.SetBool("down", false);
        }
        if (verVelocity > 0 && horVelocity == 0)
        {
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("up", true);
            anim.SetBool("down", false);

            partyAnim.SetBool("left", false);
            partyAnim.SetBool("right", false);
            partyAnim.SetBool("up", true);
            partyAnim.SetBool("down", false);
        }
        else if (verVelocity < 0 && horVelocity == 0)
        {
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("up", false);
            anim.SetBool("down", true);

            partyAnim.SetBool("left", false);
            partyAnim.SetBool("right", false);
            partyAnim.SetBool("up", false);
            partyAnim.SetBool("down", true);
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
    public void Stats()
    {
        for (int i = 0; i < GlobalManager.instance.currentParty.Count; i++)
        {
            icons[i].sprite = GlobalManager.instance.currentParty[i].character.icon;
            Color32 iconColor = icons[i].color;
            iconColor.a = 225;
            icons[i].color = iconColor;
            stats = icons[i].GetComponentsInChildren<Text>();
            for (int j = 0; j < stats.Length; j++)
            {
                if (stats[j].gameObject.name == "Atk")
                {
                    stats[j].text = "Attack: " + GlobalManager.instance.currentParty[i].attack;
                }
                else if (stats[j].gameObject.name == "Def")
                {
                    stats[j].text = "Defence: " + GlobalManager.instance.currentParty[i].def;
                }
                else if (stats[j].gameObject.name == "HP")
                {
                    stats[j].text = "HP: " + GlobalManager.instance.currentParty[i].HP + "/" + GlobalManager.instance.currentParty[i].ogHP;
                }
                else if (stats[j].gameObject.name == "SP")
                {
                    stats[j].text = "SP: " + GlobalManager.instance.currentParty[i].SP + "/" + GlobalManager.instance.currentParty[i].ogSP;
                }
                else if (stats[j].gameObject.name == "Spd")
                {
                    stats[j].text = "Speed: " + GlobalManager.instance.currentParty[i].speed;
                } else if (stats[j].gameObject.name == "Level")
                {
                    stats[j].text = "Level: " + GlobalManager.instance.currentParty[i].level;
                } else if (stats[j].gameObject.name == "Exp")
                {
                    stats[j].text = "Exp: " + GlobalManager.instance.currentParty[i].experience;
                }
                Color32 statColor = stats[j].color;
                statColor.a = 255;
                stats[j].color = statColor;
            }
            
            
            foreach (Image button in buttonParent.GetComponentsInChildren<Image>())
            {
                Color32 textColor = button.GetComponentInChildren<Text>().color;
                textColor.a = 0;
                Color32 buttonColor = button.color;
                buttonColor.a = 0;
                button.color = buttonColor;
                button.GetComponentInChildren<Text>().color = textColor;
                button.gameObject.GetComponent<Button>().interactable = false;
            }
            
        }
        backButton.interactable = true;
        backButton.GetComponent<Image>().enabled = true;
        backButton.GetComponentInChildren<Text>().enabled = true;
    }
    public void Items()
    {
        foreach (Image button in buttonParent.GetComponentsInChildren<Image>())
        {
            Color32 textColor = button.GetComponentInChildren<Text>().color;
            textColor.a = 0;
            Color32 buttonColor = button.color;
            buttonColor.a = 0;
            button.color = buttonColor;
            button.GetComponentInChildren<Text>().color = textColor;
            button.gameObject.GetComponent<Button>().interactable = false;
        }
        foreach (InventorySlot slot in slots)
        {
            slot.gameObject.GetComponentInChildren<Button>().interactable = true;
            slot.gameObject.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            
            if (slot.item != null)
            {
                slot.removeButton.interactable = true;
                slot.removeButton.gameObject.GetComponent<Image>().enabled = true;
                slot.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => StoreItem(slot.item, slot.UseItemm));
                slot.gameObject.GetComponentInChildren<Button>().onClick.AddListener(partyChoose);
            }
            else
            {
                slot.removeButton.interactable = false;
                slot.removeButton.gameObject.GetComponent<Image>().enabled = false;
                slot.gameObject.GetComponentInChildren<Button>().onClick.AddListener(None);
            }
            slot.icon.enabled = true;
            slot.color.a = 225;
            slot.gameObject.GetComponentInChildren<Image>().enabled = true;
        }
        
        backButton.interactable = true;
        backButton.GetComponent<Image>().enabled = true;
        backButton.GetComponentInChildren<Text>().enabled = true;
    }
    void StoreItem(Item itemm, ItemDelegate itemmm)
    {
        if (itemm.overworldItem == true)
        {
            itemUse = itemmm;
        } 
    }
    void None()
    {
        foreach (Button button in itemButtons)
        {
            button.GetComponent<Image>().enabled = false;
            button.interactable = false;
            button.GetComponentInChildren<Text>().enabled = false;
        }
        return;
    }
    void partyChoose()
    {
        for (int i = 0; i < GlobalManager.instance.currentParty.Count; i++)
        {
            itemButtons[i].interactable = true;
            itemButtons[i].GetComponent<Image>().enabled = true;
            itemButtons[i].GetComponentInChildren<Text>().enabled = true;
            itemButtons[i].GetComponentInChildren<Text>().text = GlobalManager.instance.currentParty[i].nameChar;
            itemButtons[i].onClick.RemoveAllListeners();
            baseStats target = GlobalManager.instance.currentParty[i];
            itemButtons[i].onClick.AddListener(() => itemUse(target));
        }
        
    }
    void Back()
    {
        Color32 menuColor = menu.color;
        menuColor.a = 255;
        menu.color = menuColor;
        foreach (Image button in buttonParent.GetComponentsInChildren<Image>())
        {
            Color32 textColor = button.GetComponentInChildren<Text>().color;
            textColor.a = 255;
            Color32 buttonColor = button.color;
            buttonColor.a = 255;
            button.color = buttonColor;
            button.GetComponentInChildren<Text>().color = textColor;
            button.gameObject.GetComponent<Button>().interactable = true;
        }
        foreach (Image icon in icons)
        {
            Color32 iconColor = icon.color;
            iconColor.a = 0;
            icon.color = iconColor;
            stats = icon.GetComponentsInChildren<Text>();
            foreach (Text stat in stats)
            {
                Color32 statColor = stat.color;
                statColor.a = 0;
                stat.color = statColor;
            }
        }
        foreach (Button button in itemButtons)
        {
            button.GetComponent<Image>().enabled = false;
            button.interactable = false;
            button.GetComponentInChildren<Text>().enabled = false;
        }
        foreach (InventorySlot slot in slots)
        {
            slot.gameObject.GetComponentInChildren<Button>().interactable = false;
            slot.removeButton.interactable = false;
            slot.removeButton.gameObject.GetComponent<Image>().enabled = false;
            slot.icon.enabled = false;
            slot.color.a = 0;
            slot.gameObject.GetComponentInChildren<Image>().enabled = false;
        }
        backButton.interactable = false;
        backButton.GetComponent<Image>().enabled = false;
        backButton.GetComponentInChildren<Text>().enabled = false;
    }
}
