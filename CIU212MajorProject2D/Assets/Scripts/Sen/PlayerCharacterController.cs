using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    //Sen's movement variables.
    private float movementSpeed = 5.0f;
    private Rigidbody2D senRigidbody;
    private Collider2D senCollider;
    private bool facingRight;
    public bool movementControlInAir = false;

    //Ground variables
    private bool isTouchingDropPlatform = false;

    //Sen's animation variables.
    private Animator anim;

    //Sen's jump variables.
    [SerializeField]
    private Transform[] groundPoints;
    private bool isGrounded;
    private float groundRadius = 0.2f;
    private bool jump;
    private float jumpForce = 500.0f;
    [SerializeField]
    private LayerMask whatIsGround;
	public bool doubleJumpUnlocked = false;
    public int doubleJump = 0;
    private float doubleJumpForceDivider = 1.0f; // The closer it is to 0, the higher the double-jump will be.

    // Sen's weapon variables.
    private GameObject axe;
    private BoxCollider2D axeBC2D;
    private GameObject bow;

    //Sen's dodge variables.
    private Vector2 dodgeRightForce = new Vector2(6000, 0);
    private Vector2 dodgeLeftForce = new Vector2(-6000, 0);

    // Enemy variables.
    private GameObject[] enemies;

    void Start()
    {
        senRigidbody = GetComponent<Rigidbody2D>(); // Get Sen's Rigidbody2D component.
        facingRight = true;
        senCollider = GetComponent<Collider2D>(); // Get Sen's BoxCollider2D component.
        axe = gameObject.transform.Find("Axe").gameObject;
        axeBC2D = axe.GetComponent<BoxCollider2D>();
        axeBC2D.enabled = false;
        bow = gameObject.transform.Find("Bow").gameObject;
        bow.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("GroundEnemy");
    }

    void Update()
    {
        HandleInput(); // Handles player input.
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded(); // Checks if Sen is grounded.

        HandleMovement(horizontal); // Handles player movement.

        Flip(horizontal); // Flips Sen's sprite to the direction he is moving.

        ResetValues(); // Resets values
    }

    private void HandleMovement(float horizontal)
    {
        if (isGrounded || movementControlInAir)
        {
            senRigidbody.velocity = new Vector2(horizontal * movementSpeed, senRigidbody.velocity.y);
            //anim.SetTrigger("Jump");
        }

        if (jump)
        {
            if (isGrounded)
            {
                isGrounded = false;
                senRigidbody.AddForce(new Vector2(0, jumpForce));
                //anim.SetTrigger("Jump");
            }
            else if (!isGrounded)
            {
                if (doubleJumpUnlocked)
                {
                    if (doubleJump <= 0)
                    {
                        senRigidbody.velocity = Vector2.zero;
						senRigidbody.AddForce(new Vector2(0, jumpForce / doubleJumpForceDivider));
                        //anim.SetTrigger("Jump");
                        doubleJump = doubleJump + 1;
					}
                }
            }
        }
    }

    // Handles player input.
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && axe.activeSelf & isGrounded)
        {
            axeBC2D.enabled = true;
        }

		if (Input.GetMouseButtonUp(0) && axe.activeSelf & isGrounded)
		{
            axeBC2D.enabled = false;
		}

        if (Input.GetKeyDown(KeyCode.Space))
        {
			jump = true;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetMouseButtonDown(1))
        {
            if (isGrounded)
            {
                DodgeRight();
            }
        }

		if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonDown(1))
		{
            if (isGrounded)
            {
                DodgeLeft();
            }
		}

        if (Input.GetKeyDown(KeyCode.S))
        {
			if (isTouchingDropPlatform)
			{
                senCollider.isTrigger = true;
			}
        }

		if (Input.GetKeyDown(KeyCode.G))
		{
			foreach (GameObject enemy in enemies)
			{
				enemy.GetComponent<GroundEnemy>().Stun();
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            bow.SetActive(false);
            axe.SetActive(true);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			bow.SetActive(true);
			axe.SetActive(false);
		}
    }

    // Flips Sen's sprite depending on which direction he is moving.
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    // Checks if Sen is grounded.
    private bool IsGrounded()
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

    // Sen's dodge (right) mechanic.
    private void DodgeRight()
    {
        senRigidbody.AddForce(dodgeRightForce);
        //anim.SetTrigger("Dodge");
    }

	// Sen's dodge (left) mechanic.
	private void DodgeLeft()
	{
        senRigidbody.AddForce(dodgeLeftForce);
		//anim.SetTrigger("Dodge");
	}

    // Sen's interaction mechanic.
    private void Interact()
    {
        Debug.Log("Interact function called.");
        //anim.SetTrigger("Interact");
    }

    // Resets values.
    private void ResetValues()
    {
        jump = false;
    }

    // When Sen's right foot hits the ground.
    public void FootstepRight()
    {

    }
    // When Sen's left foot hits the ground.
    public void FootstepLeft()
    {

    }

    // When Sen collides with something.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DropPlatform")
        {
            isTouchingDropPlatform = true;
            doubleJump = 0;
        }
        else
        {
            isTouchingDropPlatform = false;
        }

        if (collision.gameObject.tag == "Platform")
        {
            doubleJump = 0;
        }
    }

    // When Sen exits the collider he collided with.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DropPlatform")
        {
            isTouchingDropPlatform = false;
        }
    }

    // When Sen exits the trigger he was overlapping with.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DropPlatform")
        {
            senCollider.isTrigger = false;
        }
    }
}
