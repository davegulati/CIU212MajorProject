﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

	private float movementSpeed = 3.0f;
    private float jumpForce = 300.0f;

	private CharacterController characterController;
	private Rigidbody2D senRigidbody;
	
    private bool facingRight;

    public bool movementControlInAir = false;

	private Animator anim;

    [SerializeField]
    private Transform[] groundPoints;
    private bool isGrounded;
    private float groundRadius = 0.2f;
    private bool jump;

    [SerializeField]
    private LayerMask whatIsGround;

	void Start () 
	{
		characterController = GetComponent<CharacterController> ();
		senRigidbody = GetComponent<Rigidbody2D> ();
		facingRight = true;
	}

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate () 
	{
		float horizontal = Input.GetAxis ("Horizontal");

        isGrounded = IsGrounded();

		HandleMovement (horizontal);

		Flip (horizontal);

        ResetValues();
	}

	private void HandleMovement (float horizontal)
	{
        if (isGrounded)
        {
            senRigidbody.velocity = new Vector2(horizontal * movementSpeed, senRigidbody.velocity.y);
        }

        if (isGrounded && jump)
        { 
            isGrounded = false; 
            senRigidbody.AddForce(new Vector2(0, jumpForce));
        }
	}

    private void HandleInput ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("space was pressed"); 
            jump = true;
        }
    }

	private void Flip (float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) 
		{
			facingRight = !facingRight;

			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}

    private bool IsGrounded ()
    {
        if (senRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void ResetValues ()
    {
        jump = false;
    }
}
