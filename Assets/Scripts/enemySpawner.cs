using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemySpawner : MonoBehaviour
{
    public bool spawning;
    Scene currentScene;
    public static enemySpawner instance;
    public List<Transform> spawnLocations;
    public List<GameObject> spawnableEnemies;
    public int r;
    //private float spawnRate = 5.0f;
    //private float nextSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("messed up");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "StartMenu" || currentScene.name == "BattleScene")
        {
            spawning = false;
        } else if (currentScene.name == "OverworldScene")
        {
            
            spawning = true;
        }
        
       
        if (spawning == true)
        {
            StartCoroutine(Spawn());
        } else
        {

        }
    }
    IEnumerator Spawn()
    {
        spawnLocations = OverworldManager.instance.spawnLocations;
        spawnableEnemies = OverworldManager.instance.enemies;
        
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            r = Random.Range(0, spawnableEnemies.Count);
            if (spawnLocations[i].childCount < 1)
            {
                Instantiate(spawnableEnemies[r], spawnLocations[i]);
            }
            
        }
    }
}
