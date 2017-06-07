﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

    //Sen's movement variables.
	private float movementSpeed = 5.0f;
    private float jumpForce = 300.0f;
	private Rigidbody2D senRigidbody;
    private bool facingRight;
    public bool movementControlInAir = false;

    //Sen's animation variables.
	private Animator anim;

    //Sen's jump variables.
    [SerializeField]
    private Transform[] groundPoints;
    private bool isGrounded;
    private float groundRadius = 0.2f;
    private bool jump;
    [SerializeField]
    private LayerMask whatIsGround;

    //Inventory variables.
    private bool isInventoryContainerVisible = false;
    private GameObject panel_InventoryContainer;

	void Start () 
	{
		senRigidbody = GetComponent<Rigidbody2D> ();
		facingRight = true;
        panel_InventoryContainer = GameObject.Find("Panel_InventoryContainer");
        panel_InventoryContainer.SetActive(false);
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
        if (isGrounded || movementControlInAir)
        {
            senRigidbody.velocity = new Vector2(horizontal * movementSpeed, senRigidbody.velocity.y);
            //anim.SetTrigger("Jump");
        }

        if (isGrounded && jump)
        { 
            isGrounded = false; 
            senRigidbody.AddForce(new Vector2(0, jumpForce));
            //anim.SetTrigger("Jump");
        }
	}

    private void HandleInput ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space was pressed."); 
            jump = true;
        }

		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("LMB was clicked.");
            Attack();
		}

		if (Input.GetMouseButtonDown(1))
		{
			Debug.Log("RMB was clicked.");
			Dodge();
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log("E was pressed.");
            Interact();
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Debug.Log("Tab was pressed.");
            ToggleInventory();
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

    private void Attack ()
    {
        Debug.Log("Attack function called.");
        //anim.SetTrigger("Attack");
    }

	private void Dodge()
	{
		Debug.Log("Dodge function called.");
		//anim.SetTrigger("Dodge");
	}

    private void Interact ()
    {
		Debug.Log("Interact function called.");
		//anim.SetTrigger("Interact");
	}

    private void ToggleInventory()
    {
        Debug.Log("ToggleInventory function called.");
        isInventoryContainerVisible = !isInventoryContainerVisible;
        panel_InventoryContainer.SetActive(isInventoryContainerVisible);
        Debug.Log(isInventoryContainerVisible);
    }

    private void ResetValues ()
    {
        jump = false;
    }
}
