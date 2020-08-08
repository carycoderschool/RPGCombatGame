using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endMusic : MonoBehaviour
{
    public AudioSource mario;
    public AudioClip rick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndMusic()
    {
        mario.enabled = false;
    }
    public void ChangeMusic()
    {
        mario.clip = rick;
        mario.Play();
    }
}
