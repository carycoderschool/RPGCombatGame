using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    float hor;
    float ver;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        Vector2 move = new Vector2(hor, ver);
        transform.Translate(move * Time.fixedDeltaTime * speed);
    }
}
