using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = false;
    public Transform GroundCheck;//, GroundCheck2;
    public float checkRadius;
    public LayerMask WhatIsGround;
    private bool isGrounded;
    public Animator anim;
    public SpriteRenderer playerSR;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jump = 2f;
    private float horizontalInput;
    public Transform camTarget;
    public float aheadAmount, aheadSpeed;
    private bool isCrouching = false;
    private bool isHidden = false;
    private bool prevGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Flip();

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.S))
        {
            Crouch();
        }

        //if we're moving then have camera show ahead of player
         if (Input.GetAxisRaw("Horizontal") != 0)
        {
            camTarget.localPosition = new Vector3(aheadAmount * Input.GetAxisRaw("Horizontal"), camTarget.localPosition.y, camTarget.localPosition.z);
        }
        
        prevGrounded = isGrounded;
    }

    private void Movement()
    {
        //actual movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(horizontalInput * speed, rb.velocity.y);
        
        //check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, WhatIsGround);// || Physics2D.OverlapCircle(GroundCheck2.position, checkRadius, WhatIsGround);

        //check if landing
        if (isGrounded != prevGrounded)
        {
            anim.SetTrigger("Squash");
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jump, 0);
            anim.SetTrigger("Stretch");
        }
        
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * .5f, 0);
        }
    }

    private void Flip()
    {
         if (facingRight == false && Input.GetAxisRaw("Horizontal") > 0)
        {
            playerSR.flipX = true;
            facingRight = true;
        }
        else if (facingRight == true && Input.GetAxisRaw("Horizontal") < 0)
        {
            playerSR.flipX = false;
            facingRight = false;
        }
    }

    private void Crouch()
    {
        isCrouching = true;
        anim.SetTrigger("Squash");
    }

    public void Hiding()
    {
        Debug.Log("Hiding method is active");
        if (isCrouching == true)
        {
            isHidden = true;
            Debug.Log("Crouching and ishidden is true");
        }
        
    }

    //if a user presses down
    //sprite tweens down to squish a bit
    //while the button is held down they stay squish
    //Crouch state is true
    //
    //if we're inside the hiding collision oval while crouching then hiding = true
}
