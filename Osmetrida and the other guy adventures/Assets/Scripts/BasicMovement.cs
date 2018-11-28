using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimerCounter;
    public float jumpTime;
    private bool isJumping;

    private Animator animator;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
	
	void FixedUpdate ()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //Animator Control

    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

	if (Input.GetKeyDown(KeyCode.Return)){

		SceneManager.LoadScene("BossFight1");	

	}

        if (isGrounded)
        {
            animator.SetBool("isFly", false);
            isWalking();
        }
        else
        {
            animator.SetBool("isFly", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("jump");
        }

        else
        {
            animator.SetBool("isJump", false);
        }

        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimerCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimerCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimerCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            isWalking();
        }

        //Animator Control



    }
    public void isWalking(){

        if (moveInput != 0)
        {
            animator.SetBool("isWalk", true);
        }

        else
        {
            animator.SetBool("isWalk", false);
        }
    }
}
