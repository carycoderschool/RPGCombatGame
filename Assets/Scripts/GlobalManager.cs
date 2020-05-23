using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager instance;
    public List<Item> inventory = new List<Item>();
    public List<stats> encounter;
    public List<baseStats> currentParty;
    public List<baseStats> overallParty;
    bool on;
    Scene currentScene;
    GameObject enemy;
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
        enemy = encounterr.gameObject;

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
            } else if (person.gameObject.tag == "party")
            {
                person.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
         }
        DontDestroyOnLoad(enemy.transform.parent);
        enemySpawner.instance.spawning = false;
        enemySpawner.instance.spawnLocations.Clear();
        SceneManager.LoadScene("BattleScene");
        
    }
    public void RunTransition()
    {

    }
    public void BattleToOverworldTransition(GameObject player)
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
        } else if (objectLists.instance.items.Count < 1)
        {
            inventory.Clear();
        }
        
        Destroy(enemy.transform.parent.gameObject);
        SceneManager.LoadScene("OverworldScene");
        enemySpawner.instance.spawning = true;
    }
    public void StartToOverworldTransition()
    {
        
        SceneManager.LoadScene("OverworldScene");
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
                    transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                else if (transform.GetChild(i).gameObject.tag == "party")
                {
                    
                    transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }
}
