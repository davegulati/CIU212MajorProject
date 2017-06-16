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
    private float doubleJumpForceMultiplier = 1.75f;

    void Start()
    {
        senRigidbody = GetComponent<Rigidbody2D>();
        facingRight = true;
        senCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();

        HandleMovement(horizontal);

        Flip(horizontal);

        ResetValues();
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
						senRigidbody.AddForce(new Vector2(0, jumpForce / doubleJumpForceMultiplier));
                        //anim.SetTrigger("Jump");
                        doubleJump = doubleJump + 1;
					}
                }
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			jump = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Dodge();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
			if (isTouchingDropPlatform)
			{
                senCollider.isTrigger = true;
			}
        }
    }

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

    private void Dodge()
    {
        Debug.Log("Dodge function called.");
        //anim.SetTrigger("Dodge");
    }

    private void Interact()
    {
        Debug.Log("Interact function called.");
        //anim.SetTrigger("Interact");
    }

    private void ResetValues()
    {
        jump = false;
    }

    public void FootstepRight()
    {

    }

    public void FootstepLeft()
    {

    }

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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DropPlatform")
        {
            isTouchingDropPlatform = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DropPlatform")
        {
            senCollider.isTrigger = false;
        }
    }
}
