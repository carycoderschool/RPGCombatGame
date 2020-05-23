using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    public string location;
    public List<Transform> spawnLocations;
    public List<GameObject> enemies;
    public static OverworldManager instance;
    public int full;
    // Start is called before the first frame update
    void Start()
    {

        if (instance != null)
        {
            Debug.LogWarning("messed up");
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (full == spawnLocations.Count)
        {

        }
    }
}
