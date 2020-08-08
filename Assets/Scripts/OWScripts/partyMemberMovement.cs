using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partyMemberMovement : MonoBehaviour
{
    public Transform leader;
    public float moveSpeed;
    public float stoppingDistance;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector2.Distance(transform.position, leader.position);
        //Vector2 gamer = new Vector2(leader.position.x + 1)
        if (Vector2.Distance(transform.position, leader.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, leader.position, moveSpeed * Time.deltaTime);
        } else
        {

        }
        
        //transform.Translate(leader.position * moveSpeed * Time.deltaTime);
    }
}
