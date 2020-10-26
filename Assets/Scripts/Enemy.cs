using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed, waitTime, startWaitTime;
    public Transform[] moveSpots;
    private Rigidbody2D rb;
    private Animator anim;
    public SpriteRenderer heronSR;
    private int randomSpot;
    private bool facingRight = false;
    private float currentPosition, lastPosition;


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        rb = GetComponent<Rigidbody2D>();
        anim = GameObject.Find("Scale_Point").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        currentPosition = transform.position.x;
        Flip();
        
        lastPosition = currentPosition;
    }

    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
            }
            else
            {
                waitTime -= Time.deltaTime;
                
            }
        }
    }
    private void Flip()
    {
        if (facingRight == false && currentPosition > lastPosition)
        {
            heronSR.flipX = true;
            facingRight = true;
        }
        else if (facingRight == true && currentPosition < lastPosition)
        {
            heronSR.flipX = false;
            facingRight = false;
        }
    } 

}
