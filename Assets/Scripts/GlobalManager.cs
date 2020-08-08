using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager instance;
    public List<Item> inventory = new List<Item>();
    public List<Item> keyInventory = new List<Item>();
    public List<stats> encounter;
    public List<baseStats> currentParty;
    public List<baseStats> overallParty;
    bool on;
    public bool boss = false;
    public bool secretBoss = false;
    Scene currentScene;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public AudioClip overworldMusic;
    public AudioClip startBattle;
    AudioClip battleMusic;
    public bool start = false;
    public Image panel;
    public GameObject uniCanvas;
    //GameObject enemy;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("messed up");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

    }
    // Start is called before the first frame update
    public void BattleTransition(EnemyEncounter encounterr)
    {
        //enemy = encounterr.gameObject;
        //encounterr.on = true;
        if (encounterr.gameObject.tag == "boss")
        {
            boss = true;
        } else
        {
            boss = false;
        }
        if (encounterr.gameObject.tag == "secretBoss")
        {
            secretBoss = true;
        }
        else
        {
            secretBoss = false;
        }

        List<baseStats> party = currentParty;
        int ran = Random.Range(0, 3);
        if (ran == 0)
        {
            encounter = encounterr.encounter1;
        } else if (ran == 1)
        {
            encounter = encounterr.encounter2;
        } else if (ran == 2)
        {
            encounter = encounterr.encounter3;
        }
        
        encounterr.gameObject.GetComponent<EnemyEncounter>().enabled = false;
        foreach (baseStats person in currentParty)
        {
            if (person.gameObject.tag == "Overworld Player")
            {
                person.gameObject.GetComponent<playerMovement>().enabled = false;
                person.gameObject.GetComponentInChildren<AudioListener>().enabled = false;
                person.gameObject.GetComponent<baseStats>().on = false;
            } else if (person.gameObject.tag == "party")
            {
                person.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            person.on = false;
        }
        battleMusic = encounterr.battleMusic;
        enemySpawner.instance.spawning = false;
        enemySpawner.instance.spawnLocations.Clear();
        StartCoroutine(TransitionB());
        
        
        
    }
    public void RunTransition(GameObject player)
    {
        currentParty = currentParty.OrderBy(p => p.importance).ToList();
        player.GetComponent<battleSystem>().stats = player.GetComponent<battleSystem>().stats.OrderBy(s => s.importance).ToList();
        for (int i = 0; i < currentParty.Count; i++)
        {
            currentParty[i].character = player.GetComponent<battleSystem>().stats[i].state;
        }
        if (objectLists.instance.items.Count > 0)
        {
            for (int i = 0; i < objectLists.instance.items.Count; i++)
            {
                inventory[i] = objectLists.instance.items[i];
            }
        }
        else if (objectLists.instance.items.Count < 1)
        {
            inventory.Clear();
        }

        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("OverworldScene");
        GetComponent<AudioSource>().clip = overworldMusic;
        GetComponent<AudioSource>().Play();
        enemySpawner.instance.spawning = true;
    }
    public void BattleToOverworldTransition(GameObject player)
    {
        GetComponent<AudioSource>().Stop();
        if (secretBoss == true)
        {
            SceneManager.LoadScene("SecretEnding");
        } else
        {
            if (boss == true)
            {
                SceneManager.LoadScene("NormalEnding");
            }
            else if (boss == false)
            {
                currentParty = currentParty.OrderBy(p => p.importance).ToList();
                player.GetComponent<battleSystem>().charStats = player.GetComponent<battleSystem>().charStats.OrderBy(s => s.GetComponent<baseStats>().importance).ToList();
                for (int i = 0; i < currentParty.Count; i++)
                {
                    currentParty[i].character = player.GetComponent<battleSystem>().charStats[i].GetComponent<baseStats>().state;
                    if (currentParty[i].gameObject.tag == "Overworld Player")
                    {
                        currentParty[i].gameObject.GetComponent<playerMovement>().speed = 3;
                    }
                    
                    currentParty[i].Start();
                }
                if (objectLists.instance.items.Count > 0)
                {
                    for (int i = 0; i < objectLists.instance.items.Count; i++)
                    {
                        inventory[i] = objectLists.instance.items[i];
                    }
                }
                else if (objectLists.instance.items.Count < 1)
                {
                    inventory.Clear();
                }
            }
            

            //Destroy(enemy);
            StartCoroutine(TransitionOW());
           

        }
        
    }
    public void StartToOverworldTransition()
    {
        
        SceneManager.LoadScene("OverworldScene");

        
    }
    public void BossBattleFinish(GameObject player)
    {

    }
    // Update is called once per frame
    void Update()
    {
        currentParty = currentParty.OrderBy(s => s.importance).ToList();
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "StartMenu" || currentScene.name == "BattleScene")
        {
            on = false;
        }
        else if (currentScene.name == "OverworldScene")
        {
            on = true;
        }
        if (on == false)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.tag == "Overworld Player")
                {
                    transform.GetChild(i).gameObject.GetComponent<playerMovement>().enabled = false;
                    transform.GetChild(i).gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = false;

                }
                else if (transform.GetChild(i).gameObject.tag == "party")
                {
                    
                    transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }      
        }
        else
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.tag == "Overworld Player")
                {
                    transform.GetChild(i).gameObject.GetComponent<playerMovement>().enabled = true;
                    transform.GetChild(i).gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    
                }
                else if (transform.GetChild(i).gameObject.tag == "party")
                {
                    
                    transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }
    public void AddItem(Item item)
    {
        inventory.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    public void Remove(Item item)
    {

        inventory.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    IEnumerator Wait()
    {
        
        
        SceneManager.LoadScene("BattleScene");
        GetComponent<AudioSource>().PlayOneShot(startBattle);
        yield return new WaitForSeconds(6f);
        GetComponent<AudioSource>().clip = battleMusic;
        GetComponent<AudioSource>().Play();
    }
    IEnumerator TransitionB()
    {
        panel.GetComponent<Animator>().SetBool("fade", true);
        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = battleMusic;
        SceneManager.LoadScene("BattleScene");
        GetComponent<AudioSource>().Play();
        panel.GetComponent<Animator>().SetBool("fade", false);
    }
    IEnumerator TransitionOW()
    {
        panel.GetComponent<Animator>().SetBool("fade", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("OverworldScene");
        GetComponent<AudioSource>().clip = overworldMusic;
        GetComponent<AudioSource>().Play();
        enemySpawner.instance.spawning = true;
        panel.GetComponent<Animator>().SetBool("fade", false);
    }
    public void GameOver()
    {
        SceneManager.LoadScene("StartMenu");
        Destroy(uniCanvas);
        Destroy(gameObject);
    }
}

