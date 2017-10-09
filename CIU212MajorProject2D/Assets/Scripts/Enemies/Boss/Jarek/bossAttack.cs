using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    private GameObject sen;

    //place holder boss health for testing
    private float Jarek_BossHealth = 100;
    private float attackCooldown = 6.0f;
    private float attackDamage = 10.0f;

    public GameObject Poison_Launcher;
    public GameObject Poison;
    private float Poison_Forward_Force = 90.0f;

    // Use this for initialization
    void Start()
    {
        sen = GameObject.Find("Sen");

        StartCoroutine("AttackPattern");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator AttackPattern()
    {
        while (Jarek_BossHealth > 100.0f)
        {
            SlashAttack();
            yield return new WaitForSeconds(attackCooldown);
            {

                while (Jarek_BossHealth > 80.0f)
                {
                    PoisonLaunch();
                    yield return new WaitForSeconds(attackCooldown);
                }

                while (Jarek_BossHealth > 60.0f)
                {
                    StabAttack();
                    yield return new WaitForSeconds(attackCooldown);
                }

                if (Jarek_BossHealth > 0.0f)
                {
                    Destroy(this.gameObject);
                    yield return null;
                }
            }
        }
        yield return null;
    }

    void SlashAttack()
    {
        
    }

    void PoisonLaunch()
    {
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Poison, Poison_Launcher.transform.position, Poison_Launcher.transform.rotation) as GameObject;

        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        Temporary_RigidBody.AddForce(transform.forward * Poison_Forward_Force);

        Destroy(Temporary_Bullet_Handler, 3.0f);

    }

    void StabAttack()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sen.GetComponent<PlayerHealth>().PlayerTakeDamage(attackDamage);
        }
    }
}
