using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour {

    private GameObject sen;
    private Animator anim;
    private Rigidbody2D rb;
    private float chaseRange = 5.0f;
    private float rotationRange = 6.0f;
    private float speed = 8.0f;

    private GameObject[] rangedEnemies;

    // Attack variables
    private GameObject attackAlert;
    private float attackDelay = 1.0f;
    private float attackDamage = 20.0f;
    private float attackDistance = 2.5f;
    private bool canAttack = true;
    private float attackCooldown = 1.5f;

    // Stun variables
    private bool isStunned = false;
    private float stunTime = 2.0f;
    private Color normalColor = new Color(255, 255, 255);
    private Color stunnedColor = new Color(0, 0, 255);

    // Patrol variables
    private bool inZone = false;
    private GameObject[] patrolPoints;
    private bool senNoticed = false;
    private int currentPatrolPoint = 0;
    private float patrolFinishDistance = 1.0f;

    // sound 
    private AudioSource source;
    public AudioClip attackSound;
    public AudioClip stunSound;

	void Awake () 
    {
        source = GetComponent<AudioSource>();

        sen = GameObject.Find("Sen");
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
		Physics2D.IgnoreCollision(sen.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		rangedEnemies = GameObject.FindGameObjectsWithTag("RangedEnemy");
		foreach (GameObject rangedEnemy in rangedEnemies)
		{
			Physics2D.IgnoreCollision(rangedEnemy.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}
        attackAlert = gameObject.transform.Find("AttackAlert").gameObject;
        attackAlert.SetActive(false);
	}
	
	// Update is called once per frame
	private void FixedUpdate () 
    {
        float distanceToSen = Vector2.Distance(transform.position, sen.transform.position);
		if (distanceToSen < chaseRange && !isStunned)
		{
            rb.velocity = Vector3.zero;
            senNoticed = true;
		}
        else if (distanceToSen > chaseRange)
        {
            senNoticed = false;
        }

		if (inZone && !senNoticed && !isStunned)
		{
            // Patrol
            anim.SetBool("EnemyWalk", true);
			Vector2 position = new Vector2(transform.position.x, transform.position.y);
			Vector2 currentWPPosition = new Vector2(patrolPoints[currentPatrolPoint].transform.position.x, 0);
            //transform.position = Vector2.MoveTowards(position, currentWPPosition, speed * Time.deltaTime);
            Vector2 direction = currentWPPosition - position;
            direction.Normalize();
            rb.AddForce((direction) * speed);
			if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].transform.position) < patrolFinishDistance)
            {
                currentPatrolPoint++;
                rb.velocity = Vector3.zero;
                //Vector3 vectorToTarget = currentWPPosition - position;
                //float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                //Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }
            }

            // The commented code underneath flips the sprite to face the movement of the gameobject. IT WORKS!!

			//Vector2 spriteDirection = currentWPPosition - position;
			//if (spriteDirection.x < 0)
			//{
			//	gameObject.GetComponent<SpriteRenderer>().flipX = true;
			//}
			//else if (spriteDirection.x > 0)
			//{
			//	gameObject.GetComponent<SpriteRenderer>().flipX = false;
			//}


             //The code underneath rotates the gameobject itself towards where its moving (not just flip the sprite). IT WORKS!
			Vector3 vectorToTarget = currentWPPosition - position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
            //gameObject.GetComponent<GroundEnemyHealth>().FlipHealthBarCanvas();

		}

        if (!inZone && !senNoticed && !isStunned)
        {
            anim.SetBool("EnemyWalk", false);
            //anim.SetBool("EnemyIdle", true);
        }

        if (senNoticed && !isStunned && distanceToSen > attackDistance)
        {
            // Chase Sen
            anim.SetBool("EnemyWalk", true);
			Vector2 position = new Vector2(transform.position.x, transform.position.y);
			Vector2 senPosition = new Vector2(sen.transform.position.x, 0);
            Vector2 chaseDirection = senPosition - position;
            chaseDirection.Normalize();
			//transform.position = Vector2.MoveTowards(position, senPosition, speed * Time.deltaTime);
            rb.AddForce((chaseDirection) * speed);

            if (distanceToSen > rotationRange)
            {
                Vector3 vectorToTarget = senPosition - position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
            }
        }

        // Attack Sen
        if (distanceToSen < attackDistance)
        {
            rb.velocity = Vector3.zero;
            Attack();
        }
	}

    private void Attack ()
    {
        if (canAttack)
        {
            attackAlert.SetActive(true);
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            Vector2 senPosition = new Vector2(sen.transform.position.x, 0);
            Vector3 vectorToTarget = senPosition - position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
            anim.SetTrigger("EnemyAttack");
            canAttack = false;
            StartCoroutine(AttackCooldown());
            source.PlayOneShot(attackSound);
        }
    }

    public void InflictDamage ()
    {
        attackAlert.SetActive(false);
        float distanceToSen = Vector2.Distance(transform.position, sen.transform.position);
        if (distanceToSen < attackDistance)
        {
            sen.GetComponent<PlayerHealth>().PlayerTakeDamage(attackDamage);
        }
    }

    IEnumerator AttackCooldown ()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void Stun ()
    {
        isStunned = true;
        GetComponent<SpriteRenderer>().color = stunnedColor;
        StartCoroutine(StunTimer());
        source.PlayOneShot(stunSound);
    }

    IEnumerator StunTimer ()
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        GetComponent<SpriteRenderer>().color = normalColor;
    }

    public void FindPatrolPoints (GameObject patrolZone)
    {
        patrolPoints = new GameObject[patrolZone.transform.childCount];
		for (int i = 0; i < patrolZone.transform.childCount; i++)
		{
			patrolPoints[i] = patrolZone.transform.GetChild(i).gameObject;
//            Debug.Log(patrolZone.transform.GetChild(i).gameObject.name);
		}
    }

    public void JoinedZone ()
    {
        inZone = true;
    }

    public void LeftZone()
	{
        inZone = false;
	}
}
