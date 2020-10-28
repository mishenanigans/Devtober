using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = true;
    public Transform GroundCheck;
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
    public Material normalMat;
    public Material hidingMat;

    public bool IsHiding
    {
        get {return isHidden;}
    }

    public bool IsCrouching
    {
        get {return isCrouching;}
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GameObject.Find("ScalePoint").GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Stretched", true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Stretched", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouching = false;
            anim.SetBool("Squashed", false);
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
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, WhatIsGround);

        //check if landing
        if (isGrounded != prevGrounded && rb.velocity.y <= 0)
        {
            // anim.SetBool("Squashed", true);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jump, 0);
            // anim.SetBool("Stretched", true);
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
            playerSR.flipX = false;
            facingRight = true;
        }
        else if (facingRight == true && Input.GetAxisRaw("Horizontal") < 0)
        {
            playerSR.flipX = true;
            facingRight = false;
        }
    }

    private void Crouch()
    {
        isCrouching = true;
        anim.SetBool("Squashed", true);
        // anim.Play("PlayerCrouchStill");
    }

    public void Hiding()
    {
        Debug.Log("Hiding method is active");
        if (isCrouching == true)
        {
            isHidden = true;
            Debug.Log("Crouching and ishidden is true");
        }
        else if (isCrouching == false)
        {
            isHidden = false;
        }
        
        //Change from regular sprite to hiding shader. Currently does not work.
        if (isHidden == true)
        {
            if (playerSR.material == normalMat)
            {
                playerSR.material = hidingMat;
            }
        }
        else if (isHidden == false)
        {
            if (playerSR.material == hidingMat)
            {
                playerSR.material = hidingMat;
            }
        }
    }

    public void NotHiding()
    {
        isHidden = false;
    }
        

    public void Detected()
    {
        //When a player is detected by the enemy this method fires
        Debug.Log("Player is Detected");
    }
    
}
