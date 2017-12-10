﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{

    private GameObject sen;

    private Animator anim;
    private Rigidbody2D rb;
    private float chaseRange = 5.0f;
    private float rotationRange = 6.0f;
    private float speed = 2.0f;

    private GameObject[] rangedEnemies;
    public bool IsGroundEnemy = true;

    // Attack variables
    private GameObject attackAlert;
    private float attackDelay = 1.0f;
    private float attackDamage = 10.0f;
    private float attackDistance = 2.5f;
    private bool canAttack = true;
    private float attackCooldown = 0.5f;

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
    private float patrolFinishDistance = 3.0f;

    // sound 
    private AudioSource source;
    public AudioClip attackSound;
    public AudioClip stunSound;



    //On Awake
    void Awake()
    {
        source = GetComponent<AudioSource>();   //Audio player

        sen = GameObject.Find("Sen");                   //Find Sen (the player)
        anim = gameObject.GetComponent<Animator>();     //get the animator on the enemy
        rb = gameObject.GetComponent<Rigidbody2D>();    //Get the rigid body on the enemy
        rb.drag = 4;                                    //drag force on the enemy value 
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
    private void FixedUpdate()
    {
        float distanceToSen = Vector2.Distance(transform.position, sen.transform.position);         //calculates the distance from the enemy to the player
        if (distanceToSen < chaseRange && !isStunned)                                               //if the distance between the player and enemy is less the the chase Range and the enemy is stunned
        {
            rb.velocity = Vector3.zero;                                                             //get the rigid body velocity
            senNoticed = true;                                                                      //the enemy has located the player because he is within range

        }
        else if (distanceToSen > chaseRange)
        {
            senNoticed = false;                                                                     //Now the player is out of range
        }

        if (inZone && !senNoticed && !isStunned)
        {
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].transform.position) > patrolFinishDistance)
            {
                // Patrol
                anim.SetBool("EnemyWalk", true);
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                Vector2 currentWPPosition = new Vector2(patrolPoints[currentPatrolPoint].transform.position.x, 0);
                //transform.position = Vector2.MoveTowards(position, currentWPPosition, speed * Time.deltaTime);
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
            Vector2 senPosition = new Vector2(sen.transform.position.x, transform.position.y);
            Vector2 chaseDirection = senPosition - position;
            chaseDirection.Normalize();
            if(IsGroundEnemy == true)
            {
                transform.position = Vector2.MoveTowards(position, senPosition, speed * Time.deltaTime);

            }
            if (IsGroundEnemy == false)
            {
                transform.position = Vector2.MoveTowards(position, sen.transform.position, speed * Time.deltaTime);

            }
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
        if (distanceToSen < chaseRange) //attackDistance
        {
            rb.velocity = Vector3.zero;
            Attack();
        }
    }

    private void Attack()
    {
        if (canAttack)
        {
            attackAlert.SetActive(true);
            Vector2 position = new Vector2(transform.position.x, transform.position.y); //find the enemies position
            Vector2 senPosition = new Vector2(sen.transform.position.x, 0);             //find Sens position
            Vector3 vectorToTarget = senPosition - position;                            //get the direction in 3d space
            Vector3 vectorToTargetFlattened = new Vector3(vectorToTarget.x, vectorToTarget.y, 0);   //flatten the 3d vector to 2d space
            float angle = Mathf.Atan2(vectorToTargetFlattened.y, vectorToTargetFlattened.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
            if(Vector2.Distance(position, senPosition) > 0)
            {
                //Rotation fixer
                if (angle <= -90)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (angle >= -90)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                
            }
            

            anim.SetTrigger("EnemyAttack");
            canAttack = false;
            StartCoroutine(AttackCooldown());
            source.PlayOneShot(attackSound);
        }
    }

    public void InflictDamage()
    {
        attackAlert.SetActive(false);
        float distanceToSen = Vector2.Distance(transform.position, sen.transform.position);
        if (distanceToSen < attackDistance)
        {
            sen.GetComponent<PlayerHealth>().PlayerTakeDamage(attackDamage);
        }
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
        StartCoroutine(StunTimer());
        source.PlayOneShot(stunSound);
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




//if (inZone && !senNoticed && !isStunned)
//{
//         // Patrol
//         anim.SetBool("EnemyWalk", true);
//Vector2 position = new Vector2(transform.position.x, transform.position.y);
//Vector2 currentWPPosition = new Vector2(patrolPoints[currentPatrolPoint].transform.position.x, 0);
//         //transform.position = Vector2.MoveTowards(position, currentWPPosition, speed * Time.deltaTime);
//         Vector2 direction = currentWPPosition - position;
//         direction.Normalize();
//         rb.AddForce((direction) * speed);
//if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].transform.position) < patrolFinishDistance)
//{
//    currentPatrolPoint++;
//    rb.velocity = Vector3.zero;
//    //Vector3 vectorToTarget = currentWPPosition - position;
//    //float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
//    //Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
//    //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
//    if (currentPatrolPoint >= patrolPoints.Length)
//    {
//        currentPatrolPoint = 0;
//    }
//}

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
//Vector3 vectorToTarget = currentWPPosition - position;
//float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
//Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
//transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
//gameObject.GetComponent<GroundEnemyHealth>().FlipHealthBarCanvas();

//}