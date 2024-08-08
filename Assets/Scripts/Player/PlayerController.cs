using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speedFactor = 10;
    public Rigidbody2D rb;
    public Animator animator;

    //private Vector2 moveVelocity;
    public static bool playerMoving;
    private Vector2 lastMove;
    

    private float xInput;
    private float yInput;

    public float speed = 230;
    private float savedOriginalSpeed;

    public float maxSpeed = 6;
    private float savedOriginalMaxSpeed;
    public float sneakSlower;

    private Rigidbody2D rb2d;

    //public float frictionPercentage = 0.10f;
    private float currentMoveSpeed;

    private float capYSpeedPercentage = 0.65f;

    public static bool canMove = true;

    //private Vector2 tempLastVelocity;
    //public AnimationState walkingState;

    void Start()
    {
        savedOriginalSpeed = speed;
        savedOriginalMaxSpeed = maxSpeed;
        rb2d = gameObject.GetComponent<Rigidbody2D>();   
    }
    void Update()
    {
        if (canMove)
        {
            UpdateAnimValues();
        }        
    }

    public void SetAnimActiveState(bool activeState)
    {
        if (activeState)
            animator.speed = 1;
        else
            animator.speed = 0;
    }

    private void UpdateAnimValues()
    {
        playerMoving = false;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        playerMoving = (xInput != 0 || yInput != 0);

        /*
        if (WaterHandler.isInWater)
        {
            animator.SetBool("IsUnderWater", true);
        } else
        {
            animator.SetBool("IsUnderWater", false);
        }
        */



        animator.SetFloat("MoveX", xInput);
        animator.SetFloat("MoveY", yInput);
        animator.SetBool("PlayerMoving", playerMoving);
        animator.SetFloat("LastMoveY", lastMove.y);
        animator.SetFloat("LastMoveX", lastMove.x);
    }

    private void CapSpeedIfAboveBoundary()
    {
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.y > maxSpeed* capYSpeedPercentage)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxSpeed * capYSpeedPercentage);
        }
        if (rb2d.velocity.y < -maxSpeed* capYSpeedPercentage)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -maxSpeed * capYSpeedPercentage);
        }
    }

    private void AddFakeFriction(double h, double v)
    {
        if (h == 0)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        if (v == 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        }
        //if (v == 1 && h == 1)
        //{
        //    rb2d.velocity = new Vector2(1, 1);
        //}

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            MovePlayer();
        } else
        {
            rb2d.velocity = new Vector2(0,0);
        }
        
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Debug.Log("pressing leftshiiit, doestn work well. Fix speed and fix animator to slow down!!");
            //If sneaking slow down player
            maxSpeed = savedOriginalMaxSpeed * sneakSlower;
            animator.SetBool("Sneak", true);
        }
        else
        {
            maxSpeed = savedOriginalMaxSpeed;
            animator.SetBool("Sneak", false);
        }
        //Vector3 easeVelocity = rb2d.velocity;
        //easeVelocity.y *= frictionPercentage;
        //easeVelocity.z = 0.0f;
        //easeVelocity.x *= frictionPercentage;


        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        playerMoving = (h != 0 || v != 0);

        if (playerMoving)
        {
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        AddFakeFriction(h, v);


        if (Mathf.Abs(h) > 0.5f && Mathf.Abs(h) > 0.5f)
        {
            currentMoveSpeed = speed / 5f;
        }
        else
        {
            currentMoveSpeed = speed;
        }



        rb2d.AddForce((Vector2.right * currentMoveSpeed * 1.3f * h) + (Vector2.up * currentMoveSpeed * 0.70f * v));
        CapSpeedIfAboveBoundary();
    }
}