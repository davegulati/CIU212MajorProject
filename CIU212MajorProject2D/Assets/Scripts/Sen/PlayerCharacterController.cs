using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

	public float movementSpeed = 10.0f;
    public float jumpForce = 10;

	private CharacterController characterController;
	private Rigidbody2D senRigidbody;
	
    private bool facingRight;

    public bool movementControlInAir = false;

	private Animator anim;

	void Start () 
	{
		characterController = GetComponent<CharacterController> ();
		senRigidbody = GetComponent<Rigidbody2D> ();
		facingRight = true;
	}

	void FixedUpdate () 
	{
		float horizontal = Input.GetAxis ("Horizontal");

		HandleMovement (horizontal);
		Flip (horizontal);
	}

	private void HandleMovement (float horizontal)
	{
		senRigidbody.velocity = new Vector2 (horizontal * movementSpeed, senRigidbody.velocity.y);
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
}
