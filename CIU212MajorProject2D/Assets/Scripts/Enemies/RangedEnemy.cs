using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

	private GameObject sen;
    private Animator anim;
    private float fleeRange = 2.5f;
	private float speed = 3.0f;

    private GameObject[] groundEnemies;

	// Attack variables
    private float attackRange = 8.0f;
	private bool canAttack = true;
	private float attackCooldown = 2.0f;
    [SerializeField]
    private Transform arrow;
    private Transform firePoint;

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

    //sound
    private AudioSource source;
    public AudioClip attackSound;
    public AudioClip stunSound;

	void Awake()
	{
        source = GetComponent<AudioSource>();

		sen = GameObject.Find("Sen");
        anim = gameObject.GetComponent<Animator>();
        Physics2D.IgnoreCollision(sen.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        firePoint = gameObject.transform.Find("FirePoint");

		groundEnemies = GameObject.FindGameObjectsWithTag("GroundEnemy");
		foreach (GameObject groundEnemy in groundEnemies)
		{
			Physics2D.IgnoreCollision(groundEnemy.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}
	}

	// Update is called once per frame
	private void Update()
	{
		float distanceToSen = Vector2.Distance(transform.position, sen.transform.position);
		//Debug.Log(distanceToSen);
		if (distanceToSen < fleeRange && !isStunned)
		{
            // Flee
		}

		if (distanceToSen > fleeRange && distanceToSen < attackRange) // Attack
		{
            senNoticed = true;
		}

        if (distanceToSen > attackRange) // Patrol
        {
            senNoticed = false;
			if (inZone && !senNoticed && !isStunned)
			{
				// Patrol
				Vector2 position = new Vector2(transform.position.x, transform.position.y);
				Vector2 currentWPPosition = new Vector2(patrolPoints[currentPatrolPoint].transform.position.x, 0);
				transform.position = Vector2.MoveTowards(position, currentWPPosition, speed * Time.deltaTime);
				if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].transform.position) < patrolFinishDistance)
				{
					currentPatrolPoint++;
					if (currentPatrolPoint >= patrolPoints.Length)
					{
						currentPatrolPoint = 0;
					}
				}
			}
        }

        if (senNoticed && !isStunned)// Attack Sen
		{
			if (canAttack)
			{
				Attack();
			}
		}
	}

	private void Attack()
	{
        //anim.SetTrigger("ATTACK TRIGGER NAME");
        source.PlayOneShot(attackSound);
		canAttack = false;
		Vector2 firePointPosition = new Vector2(transform.position.x, transform.position.y);
		Instantiate(arrow, firePointPosition, firePoint.rotation);
		StartCoroutine(AttackCooldown());
	}

	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(attackCooldown);
		canAttack = true;
	}

	public void Stun()
	{
		isStunned = true;
		GetComponent<SpriteRenderer>().color = stunnedColor;
        source.PlayOneShot(stunSound);
		StartCoroutine(StunTimer());
	}

	IEnumerator StunTimer()
	{
		yield return new WaitForSeconds(stunTime);
		isStunned = false;
		GetComponent<SpriteRenderer>().color = normalColor;
	}

	public void FindPatrolPoints(GameObject patrolZone)
	{
		patrolPoints = new GameObject[patrolZone.transform.childCount];
		for (int i = 0; i < patrolZone.transform.childCount; i++)
		{
			patrolPoints[i] = patrolZone.transform.GetChild(i).gameObject;
			//            Debug.Log(patrolZone.transform.GetChild(i).gameObject.name);
		}
	}

	public void JoinedZone()
	{
		inZone = true;
	}

	public void LeftZone()
	{
		inZone = false;
	}
}
