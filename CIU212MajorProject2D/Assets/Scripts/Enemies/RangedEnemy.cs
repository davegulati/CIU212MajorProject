﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

	private GameObject sen;
    private Animator anim;
    private Rigidbody2D rb;
    private float fleeRange = 2.5f;
	private float speed = 2.0f;

    private GameObject[] groundEnemies;

	// Attack variables
    private GameObject attackAlert;
    private float attackDelay = 1.0f;
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
        //Audio Source
        source = GetComponent<AudioSource>();

        //Establish the variables
		sen = GameObject.Find("Sen");                   //Finds the player "Sen"
        anim = gameObject.GetComponent<Animator>();     //Gets the animator component of the enemy
        rb = gameObject.GetComponent<Rigidbody2D>();    //Gets the rigid body of the enemy
        rb.drag = 4;                                    //Set drag to 4
        Physics2D.IgnoreCollision(sen.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());    //ignore collisions on the player
        firePoint = gameObject.transform.Find("FirePoint"); //Finds a game object called fire point...

		groundEnemies = GameObject.FindGameObjectsWithTag("GroundEnemy");   //Ignores all collisions of other enemies...
		foreach (GameObject groundEnemy in groundEnemies)
		{
			Physics2D.IgnoreCollision(groundEnemy.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}

        attackAlert = gameObject.transform.Find("AttackAlert").gameObject;  //Alert game object on the enemy
        attackAlert.SetActive(false);   //Turns it off at start untill found
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

        if (inZone && !senNoticed && !isStunned)
        {
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].transform.position) > patrolFinishDistance)
            {
                // Patrol
                anim.SetBool("EnemyWalk", true);
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                Vector2 currentWPPosition = new Vector2(patrolPoints[currentPatrolPoint].transform.position.x, 0);
                transform.position = Vector2.MoveTowards(position, currentWPPosition, speed * Time.deltaTime);
                Vector2 direction = currentWPPosition - position;
                direction.Normalize();
                rb.AddForce((direction) * speed);
            }
            else if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].transform.position) < patrolFinishDistance)
            {
                // Rotate
                rb.velocity = Vector3.zero;
                currentPatrolPoint++;
                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                Vector2 currentWPPosition = new Vector2(patrolPoints[currentPatrolPoint].transform.position.x, 0);
                Vector3 vectorToTarget = currentWPPosition - position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, 360 * Time.deltaTime);
                              
            }
        }
   //     if (distanceToSen > attackRange) // Patrol
   //     {
   //         senNoticed = false;
			//if (inZone && !senNoticed && !isStunned)
			//{
			//	// Patrol
   //             anim.SetBool("EnemyWalk", true);
			//	Vector2 position = new Vector2(transform.position.x, transform.position.y);
			//	Vector2 currentWPPosition = new Vector2(patrolPoints[currentPatrolPoint].transform.position.x, 0);
			//	transform.position = Vector2.MoveTowards(position, currentWPPosition, speed * Time.deltaTime);
			//	if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].transform.position) < patrolFinishDistance)
			//	{
			//		currentPatrolPoint++;
			//		if (currentPatrolPoint >= patrolPoints.Length)
			//		{
			//			currentPatrolPoint = 0;
			//		}
			//	}
			//}
        //}

        if (!inZone && !senNoticed && !isStunned)
        {
            anim.SetBool("EnemyWalk", false);
        }

        if (senNoticed && !isStunned && Vector2.Distance(transform.position, sen.transform.position) > 0.75f)// Attack Sen
		{
			Attack();
		}
	}















	private void Attack()
	{
        if (canAttack)
        {
            attackAlert.SetActive(true);
            canAttack = false;
            anim.SetTrigger("EnemyAttack");
            transform.position = Vector2.MoveTowards(transform.position, sen.transform.position, speed * Time.deltaTime);
        }
	}

    public void LaunchProjectile()
    {
        attackAlert.SetActive(false);
        Vector2 firePointPosition = new Vector2(transform.position.x, transform.position.y);
        Instantiate(arrow, firePointPosition, firePoint.rotation);
        source.PlayOneShot(attackSound);
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
