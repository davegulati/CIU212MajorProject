using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{

    //Sen's movement variables.
    private float movementSpeed = 5.0f;
    private float jumpForce = 500.0f;
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
    [SerializeField]
    private LayerMask whatIsGround;

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

        if (isGrounded && jump)
        {
            isGrounded = false;
            senRigidbody.AddForce(new Vector2(0, jumpForce));
            //anim.SetTrigger("Jump");
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			Debug.Log("Space was pressed.");
			jump = true;
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
        }
        else
        {
            isTouchingDropPlatform = false;
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
