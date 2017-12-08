using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossAttack : MonoBehaviour
{
    private GameObject sen;
    private GameManager gameManager;

    //hitboxes
    public GameObject attackHitbox1;
    public GameObject attackHitbox2;

    //place holder boss health for testing
    private float bossHealth = 100;
    private float maxHealth = 100;
    private float Jarek_BossHealth = 100;
    private Slider healthSlider;

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
        healthSlider = transform.Find("HealthBarCanvas").transform.Find("HealthBarSlider").GetComponent<Slider>();
        Jarek_BossHealth = maxHealth;
        if (gameObject.transform.Find("HealthBarCanvas").transform.Find("HealthBarSlider").transform.Find("Handle Slide Area").gameObject != null)
        {
            Destroy(gameObject.transform.Find("HealthBarCanvas").transform.Find("HealthBarSlider").transform.Find("Handle Slide Area").gameObject);
        }

        gameObject.transform.Find("HealthBarCanvas").transform.Find("HealthBarSlider").transform.Find("Background").gameObject.GetComponent<Image>().color = Color.red;
        gameObject.transform.Find("HealthBarCanvas").transform.Find("HealthBarSlider").transform.Find("Fill Area").transform.Find("Fill").gameObject.GetComponent<Image>().color = Color.green;

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

   public void MeleeAttack1()
    {
        anim.SetTrigger("EnemyAttack");
        source.PlayOneShot(meleeAttack1);
        attackHitbox1.SetActive(true);
        new WaitForSeconds(attackDuration);
        attackHitbox1.SetActive(false);
        anim.SetTrigger("EnemyAttack");
    }

    public void RangedAttack()
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

   public void MeleeAttack2()
    {
        anim.SetTrigger("EnemyAttack2");
        attackHitbox2.SetActive(true);
        source.PlayOneShot(meleeAttack2);
        new WaitForSeconds(attackDuration);
        attackHitbox2.SetActive(false);
        anim.SetTrigger("EnemyAttack2");
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sen.GetComponent<PlayerHealth>().PlayerTakeDamage(attackDamage);
        }
    }

    public void DamageEnemy(float damage)
    {
        Jarek_BossHealth = Jarek_BossHealth - damage;
        healthSlider.value = Jarek_BossHealth / maxHealth;
        if (Jarek_BossHealth <= 0)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            anim.SetTrigger("EnemyDeath");
        }
        else
        {
            anim.SetTrigger("EnemyTakeDamage");
        }
    }

    public void LoadOutroCutscene()
    {
        gameManager.LoadScene("OutroCutscene");
    }
}
