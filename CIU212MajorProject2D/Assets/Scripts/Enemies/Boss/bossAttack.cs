using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    private GameObject sen;

    //hitboxes
    public GameObject attackHitbox1;
    public GameObject attackHitbox2;

    //place holder boss health for testing
    private float bossHealth = 100;
    public float attackCooldown = 5.0f;
    public float attackDamage = 10.0f;
    public float attackDelay = 1.5f;
    public float attackDuration = 0.3f;

    public GameObject projectileLauncher;
    public GameObject projectile;
    public float projectile_Forward_Force = 1200.0f;

    //sound
    private AudioSource source;
    public AudioClip meleeAttack1;
    public AudioClip projectileLaunch;
    public AudioClip meleeAttack2;

    //animation
     Animator anim;


    // Use this for initialization
    void Start()
    {
        sen = GameObject.Find("Sen");
        anim = GetComponent<Animator>();

        StartCoroutine("AttackPattern");
    }

    // Update is called once per frame
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    IEnumerator AttackPattern()
    {
        while (true)
        {
                if (bossHealth > 75.0f)
                {
                Debug.Log("Started");
                    yield return new WaitForSeconds(attackDelay);
                    MeleeAttack1();
                    yield return new WaitForSeconds(attackCooldown);

                    yield return new WaitForSeconds(attackDelay);
                    MeleeAttack2();
                    yield return new WaitForSeconds(attackCooldown);
                }

                if (bossHealth > 50.0f)
                {
                    yield return new WaitForSeconds(attackDelay);
                    RangedAttack();
                    yield return new WaitForSeconds(attackCooldown);

                    yield return new WaitForSeconds(attackDelay);
                    MeleeAttack1();
                    yield return new WaitForSeconds(attackCooldown);

                    yield return new WaitForSeconds(attackDelay);
                    MeleeAttack1();
                    yield return new WaitForSeconds(attackCooldown);
            }

                while (bossHealth > 25.0f)
                {
                    yield return new WaitForSeconds(attackDelay);
                    MeleeAttack2();
                    yield return new WaitForSeconds(attackCooldown);

                    yield return new WaitForSeconds(attackDelay);
                    RangedAttack();
                    RangedAttack();
                    yield return new WaitForSeconds(attackCooldown);
            }

                if (bossHealth <= 0.75f)
                {
                    anim.SetTrigger("EnemyDeath");
                    Destroy(this.gameObject);
                    yield return null;
                }
                yield return null;
        }
    }

    void MeleeAttack1()
    {
        anim.SetTrigger("EnemyAttack");
        source.PlayOneShot(meleeAttack1);
        attackHitbox1.SetActive(true);
        new WaitForSeconds(attackDuration);
        attackHitbox1.SetActive(false);
        anim.SetTrigger("EnemyAttack");
    }

    void RangedAttack()
    {
        //add attack animation
        GameObject Temporary_Bullet_Handler;
        source.PlayOneShot(projectileLaunch);
        Temporary_Bullet_Handler = Instantiate(projectile, projectileLauncher.transform.position, projectileLauncher.transform.rotation) as GameObject;

        Rigidbody2D Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody2D>();

        //transform.right for a directional shot
        Temporary_RigidBody.AddForce(sen.transform.position * projectile_Forward_Force);

        Destroy(Temporary_Bullet_Handler, 3.0f);

    }

    void MeleeAttack2()
    {
        anim.SetTrigger("EnemyAttack2");
        attackHitbox2.SetActive(true);
        source.PlayOneShot(meleeAttack2);
        new WaitForSeconds(attackDuration);
        attackHitbox2.SetActive(false);
        anim.SetTrigger("EnemyAttack2");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sen.GetComponent<PlayerHealth>().PlayerTakeDamage(attackDamage);
        }
    }
}
