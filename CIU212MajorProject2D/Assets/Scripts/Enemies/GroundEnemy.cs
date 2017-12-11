using System.Collections;
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
    private float attackDistance = 5.0f;
    private float attackDistanceRanged = 5.0f;
    private bool canAttack = true;
    private float attackCooldown = 0.5f;
    public GameObject projectile;
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
        if (!IsGroundEnemy)
        {
            firePoint = gameObject.transform.Find("FirePoint");
        }
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
                
                if(vectorToTarget.x < transform.position.x)
                    {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                if (vectorToTarget.x > transform.position.x)
                    {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    }

            }
        }



        if (!inZone && !senNoticed && !isStunned)
        {
            anim.SetBool("EnemyWalk", false);
            //anim.SetBool("EnemyIdle", true);
        }

        if (IsGroundEnemy)
        {
            if (senNoticed && !isStunned && distanceToSen < attackDistance)
            {
                // Chase Sen
                anim.SetBool("EnemyWalk", true);
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                Vector2 senPosition = new Vector2(sen.transform.position.x, transform.position.y);
                Vector2 chaseDirection = senPosition - position;
                chaseDirection.Normalize();
                if (IsGroundEnemy == true)
                {
                    transform.position = Vector2.MoveTowards(position, senPosition, speed * Time.deltaTime);

                }
                if (IsGroundEnemy == false)
                {
                    transform.position = Vector2.MoveTowards(position, sen.transform.position, speed * Time.deltaTime);

                }
                rb.AddForce((chaseDirection) * speed);

            }
        }

        if (!IsGroundEnemy)
        {
            if (senNoticed && !isStunned && distanceToSen < attackDistanceRanged)
            {
                // Chase Sen
                anim.SetBool("EnemyWalk", true);
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                Vector2 senPosition = new Vector2(sen.transform.position.x, transform.position.y);
                Vector2 chaseDirection = senPosition - position;
                chaseDirection.Normalize();
                if (IsGroundEnemy == true)
                {
                    transform.position = Vector2.MoveTowards(position, senPosition, speed * Time.deltaTime);
                    canAttack = true;
                }
                if (IsGroundEnemy == false)
                {
                    transform.position = Vector2.MoveTowards(position, sen.transform.position, speed * Time.deltaTime);
                    canAttack = true;
                }
                rb.AddForce((chaseDirection) * speed);

            }
        }

        // Attack Sen
        if (distanceToSen < attackDistance)
        {
            if (IsGroundEnemy) //attackDistance
            {
                rb.velocity = Vector3.zero;
                Attack_Ground();
            }
        }

        if (distanceToSen < attackDistanceRanged)
        {
            if (!IsGroundEnemy)
            {
                rb.velocity = Vector3.zero;
                Attack_Ranged();
            }
        }
    }

    private void Attack_Ground()
    {
        if (canAttack)
        {
            attackAlert.SetActive(true);
            Vector2 position = new Vector2(transform.position.x, transform.position.y); //find the enemies position
            Vector2 senPosition = new Vector2(sen.transform.position.x, 0);             //find Sens position
            
            if (Vector2.Distance(position, senPosition) > 0)
            {
                if (senPosition.x < position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (senPosition.x > position.x)
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

    private void Attack_Ranged()
    {
        if (canAttack)
        {
            attackAlert.SetActive(true);
            Vector2 position = new Vector2(transform.position.x, transform.position.y); //find the enemies position
            Vector2 senPosition = new Vector2(sen.transform.position.x, 0);             //find Sens position

            if (Vector2.Distance(position, senPosition) > 0)
            {
                if (senPosition.x < position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (senPosition.x > position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            anim.SetTrigger("EnemyAttack");
            canAttack = false;
        }
    }

    public void LaunchProjectile()
    {
        attackAlert.SetActive(false);
        Vector2 firePointPosition = new Vector2(transform.position.x, transform.position.y);
        if (projectile != null)
        {
            Instantiate(projectile, firePointPosition, firePoint.rotation);
            source.PlayOneShot(attackSound);
            StartCoroutine(AttackCooldown());
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