using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController instance;

    //Sen's movement variables.
    [HideInInspector]
    public float baseMovementSpeed = 12.5f;
    [HideInInspector]
    public float movementSpeed = 12.5f;
    private Rigidbody2D senRigidbody;
    private Collider2D senCollider;
    private bool facingRight;
    private bool movementControlInAir = true;

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
    private float jumpForce = 600.0f;
    [SerializeField]
    private LayerMask whatIsGround;
	private bool doubleJumpUnlocked = true;
    private int doubleJump = 0;
    private float doubleJumpForceDivider = 1.0f; // The closer it is to 0, the higher the double-jump will be.

    // Sen's weapon variables.
    private GameObject axe;
    private BoxCollider2D axeBC2D;
    private GameObject bow;
    private Bow bowScript;
    private GameObject currentWeapon;
    private WeaponSlot weaponSlot;

    //Sen's dodge variables.
    private float reactivateColliderAfterTime = 0.1f;
    private Vector2 dodgeRightForce = new Vector2(6000, 0);
    private Vector2 dodgeLeftForce = new Vector2(-6000, 0);

    // Sen's inventory variables.
    private InventorySlot activeInventorySlot1;
    private InventorySlot activeInventorySlot2;

    // Item variables.
    [HideInInspector]
    public bool pocketSniperUnlocked = false;

    // Bud variables.
    private Bud bud;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        senRigidbody = GetComponent<Rigidbody2D>(); // Get Sen's Rigidbody2D component.
        facingRight = true;
        senCollider = GetComponent<Collider2D>(); // Get Sen's BoxCollider2D component.
        anim = gameObject.GetComponent<Animator>();
        axe = gameObject.transform.Find("Axe").gameObject;
        axeBC2D = axe.GetComponent<BoxCollider2D>();
        axeBC2D.enabled = false;
        bow = gameObject.transform.Find("Bow").gameObject;
        bowScript = bow.GetComponent<Bow>();
        bow.SetActive(false);
        activeInventorySlot1 = InventorySystem.instance.gameObject.transform.Find("Inventory").transform.Find("ItemsParent").transform.Find("ActiveItemSlots").transform.Find("InventorySlot1").GetComponent<InventorySlot>();
		activeInventorySlot2 = InventorySystem.instance.gameObject.transform.Find("Inventory").transform.Find("ItemsParent").transform.Find("ActiveItemSlots").transform.Find("InventorySlot2").GetComponent<InventorySlot>();
        weaponSlot = GameObject.Find("Canvas").transform.Find("HealthBar").transform.Find("Base").transform.Find("WeaponSlot").GetComponent<WeaponSlot>();
        currentWeapon = axe;
        if (currentWeapon == axe)
        {
            weaponSlot.UpdateWeaponSlotImage(currentWeapon, axe.GetComponent<Axe>().currentDurability);

        }
        else if (currentWeapon == bow)
        {
            weaponSlot.UpdateWeaponSlotImage(currentWeapon, bow.GetComponent<Bow>().currentDurability);

        }
        bud = GameObject.Find("Bud").gameObject.GetComponent<Bud>();
	}

    void Update()
    {
		HandleInput(); // Handles player input.
	}

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("Walk", Mathf.Abs(horizontal));
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
                anim.SetTrigger("Jump");
            }
            else if (!isGrounded)
            {
                if (doubleJumpUnlocked)
                {
                    if (doubleJump <= 0)
                    {
                        senRigidbody.velocity = Vector2.zero;
						senRigidbody.AddForce(new Vector2(0, jumpForce / doubleJumpForceDivider));
                        anim.SetTrigger("DoubleJump");
                        doubleJump = doubleJump + 1;
					}
                }
            }
        }
    }

 //   private void HandleMovement_METHOD2 (float horizontal)
 //   {
 //       Vector3 moveVector = Vector3.zero;
 //       moveVector.x = Input.GetAxis("Horizontal");
 //       moveVector.z = 0;
 //       transform.Translate(moveVector * movementSpeed * Time.deltaTime);

	//	if (isGrounded || movementControlInAir)
	//    {
	//        senRigidbody.velocity = new Vector2(horizontal * movementSpeed, senRigidbody.velocity.y);
	//		//anim.SetTrigger("Jump");
	//	}

	//		if (jump)
	//    {
	//        if (isGrounded)
	//        {
	//            isGrounded = false;
	//            senRigidbody.AddForce(new Vector2(0, jumpForce));
	//            //anim.SetTrigger("Jump");
	//        }
	//        else if (!isGrounded)
	//        {
	//            if (doubleJumpUnlocked)
	//            {
	//                if (doubleJump <= 0)
	//                {
	//                    senRigidbody.velocity = Vector2.zero;
	//                    senRigidbody.AddForce(new Vector2(0, jumpForce / doubleJumpForceDivider));
	//                    //anim.SetTrigger("Jump");
	//                    doubleJump = doubleJump + 1;
	//                }
	//            }
	//        }
	//    }

	//}

    // Handles player input.
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && axe.activeSelf & isGrounded)
        {
            anim.SetTrigger("MeleeAttack");
            axeBC2D.enabled = true;
        }

		if (Input.GetMouseButtonUp(0) && axe.activeSelf & isGrounded)
		{
            axeBC2D.enabled = false;
		}

        if (Input.GetMouseButtonDown(0) && bow.activeSelf & isGrounded)
        {
            anim.SetTrigger("BowAttack");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
			jump = true;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetMouseButtonDown(1))
        {
            if (isGrounded)
            {
                anim.SetTrigger("DodgeRight");
            }
        }

		if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonDown(1))
		{
            if (isGrounded)
            {
                anim.SetTrigger("DodgeLeft");
            }
		}

        if (Input.GetKeyDown(KeyCode.S))
        {
			if (isTouchingDropPlatform)
			{
                senCollider.isTrigger = true;
			}
        }

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            bow.SetActive(false);
            axe.SetActive(true);
            currentWeapon = axe;
            weaponSlot.UpdateWeaponSlotImage(currentWeapon, axe.GetComponent<Axe>().currentDurability);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
            axe.SetActive(false);
			bow.SetActive(true);
            currentWeapon = bow;
            weaponSlot.UpdateWeaponSlotImage(currentWeapon, bow.GetComponent<Bow>().currentDurability);
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
            activeInventorySlot1.UseItem();
            bud.UseActiveAbility();
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			activeInventorySlot2.UseItem();
            bud.UseActiveAbility();
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
        senCollider.enabled = false;
        senRigidbody.AddForce(dodgeRightForce);
        StartCoroutine(ReactivateDamageCollider());
        //anim.SetTrigger("Dodge");
    }

	// Sen's dodge (left) mechanic.
	private void DodgeLeft()
	{
        anim.SetTrigger("Dodge");
        senCollider.enabled = false;
        senRigidbody.AddForce(dodgeLeftForce);
        StartCoroutine(ReactivateDamageCollider());
		//anim.SetTrigger("Dodge");
	}

    IEnumerator ReactivateDamageCollider ()
    {
        yield return new WaitForSeconds(reactivateColliderAfterTime);
        senCollider.enabled = true;
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

    public void RepairWeapons ()
    {
        if (axe.activeSelf)
        {
            axe.GetComponent<Axe>().RepairWeapon();
            bow.SetActive(true);
            bow.GetComponent<Bow>().RepairWeapon();
            bow.SetActive(false);
        }
        else if (bow.activeSelf)
        {
            bow.GetComponent<Bow>().RepairWeapon();
            axe.SetActive(true);
            axe.GetComponent<Axe>().RepairWeapon();
            axe.SetActive(false);
        }

        Notification.instance.Display("!", "WEAPONS REPAIRED", "All weapons repaired!", "All weapons have been restored.", "Weapon damage restored to original amounts.", 3.0f);
    }

    private void ShootArrow ()
    {
        bowScript.ShootArrow();
    }
}
