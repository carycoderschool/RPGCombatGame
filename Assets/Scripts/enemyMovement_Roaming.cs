using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement_Roaming : MonoBehaviour
{
    float moveRate = 5f;
    float nextMove;
    public float speed;
    public Rigidbody2D rb;
    Vector2 orgPos;
    public float dist;
    Vector2 lastMove;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orgPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(orgPos, transform.position);
        if (Time.time > nextMove && dist < 2)
        {
            int ran = Random.Range(0, 4);
            if (ran == 0)
            {
                rb.AddForce(Vector2.up * speed);
                nextMove = Time.time + moveRate;
                lastMove = Vector2.up;
            } else if (ran == 1)
            {
                rb.AddForce(Vector2.down * speed);
                nextMove = Time.time + moveRate;
                lastMove = Vector2.down;
            } else if (ran == 2)
            {
                rb.AddForce(Vector2.right * speed);
                nextMove = Time.time + moveRate;
                lastMove = Vector2.right;
            } else if (ran == 3)
            {
                rb.AddForce(Vector2.left * speed);
                nextMove = Time.time + moveRate;
                lastMove = Vector2.left;
            }
        } else if (dist > 2)
        {
            float spd = speed - 10;
            rb.AddForce(-lastMove * spd);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            rb.AddForce(-lastMove * speed);
        }
        
    }
}
