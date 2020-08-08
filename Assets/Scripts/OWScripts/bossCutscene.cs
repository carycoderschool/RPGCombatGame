using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCutscene : MonoBehaviour
{
    bool off = false;
    public AudioClip roar;
    public float clipTime;
    public playerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Overworld Player").GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(bruh());
        IEnumerator bruh()
        {
            if (off == false)
            {
                player.PauseGame();
                GlobalManager.instance.GetComponent<AudioSource>().Stop();
                GlobalManager.instance.GetComponent<AudioSource>().PlayOneShot(roar);

                yield return new WaitForSecondsRealtime(clipTime);
                GlobalManager.instance.GetComponent<AudioSource>().clip = GlobalManager.instance.overworldMusic;
                GlobalManager.instance.GetComponent<AudioSource>().Play();
                Time.timeScale = 1;
                player.isGamePaused = false;
                off = true;
            }
        }
        
    }
}
