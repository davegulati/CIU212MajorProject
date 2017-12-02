﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    private GameObject sen;
    public GameObject attackHitbox1;
    public GameObject attackHitbox2;

    //place holder boss health for testing
    private float Jarek_BossHealth = 100;
    public float attackCooldown = 5.0f;
    public float attackDamage = 10.0f;
    public float attackDelay = 1.5f;
    public float attackDuration = 0.3f;

    public GameObject Poison_Launcher;
    public GameObject Poison;
    public float Poison_Forward_Force = 1200.0f;

    private AudioSource source;
    public AudioClip slashAttack;
    public AudioClip poisonLaunch;
    public AudioClip stabAttack;


    // Use this for initialization
    void Start()
    {
        sen = GameObject.Find("Sen");

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
                if (Jarek_BossHealth > 75.0f)
                {
                Debug.Log("Started");
                    yield return new WaitForSeconds(attackDelay);
                    SlashAttack();
                    yield return new WaitForSeconds(attackCooldown);

                    yield return new WaitForSeconds(attackDelay);
                    StabAttack();
                    yield return new WaitForSeconds(attackCooldown);
                }

                if (Jarek_BossHealth > 50.0f)
                {
                    yield return new WaitForSeconds(attackDelay);
                    PoisonLaunch();
                    yield return new WaitForSeconds(attackCooldown);

                    yield return new WaitForSeconds(attackDelay);
                    SlashAttack();
                    yield return new WaitForSeconds(attackCooldown);

                    yield return new WaitForSeconds(attackDelay);
                    SlashAttack();
                    yield return new WaitForSeconds(attackCooldown);
            }

                while (Jarek_BossHealth > 25.0f)
                {
                    yield return new WaitForSeconds(attackDelay);
                    StabAttack();
                    yield return new WaitForSeconds(attackCooldown);

                    yield return new WaitForSeconds(attackDelay);
                    PoisonLaunch();
                    yield return new WaitForSeconds(attackCooldown);
            }

                if (Jarek_BossHealth <= 1.0f)
                {
                    Destroy(this.gameObject);
                    yield return null;
                }
                yield return null;
        }
    }

    void SlashAttack()
    {
        //add attack animation
        source.PlayOneShot(slashAttack);
        attackHitbox1.SetActive(true);
        new WaitForSeconds(attackDuration);
        attackHitbox1.SetActive(false);
    }

    void PoisonLaunch()
    {
        //add attack animation
        GameObject Temporary_Bullet_Handler;
        source.PlayOneShot(poisonLaunch);
        Temporary_Bullet_Handler = Instantiate(Poison, Poison_Launcher.transform.position, Poison_Launcher.transform.rotation) as GameObject;

        Rigidbody2D Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody2D>();

        //transform.right for a directional shot
        Temporary_RigidBody.AddForce(sen.transform.position * Poison_Forward_Force);

        Destroy(Temporary_Bullet_Handler, 3.0f);

    }

    void StabAttack()
    {
        //add attack animation
        source.PlayOneShot(stabAttack);
        attackHitbox2.SetActive(true);
        new WaitForSeconds(attackDuration);
        attackHitbox2.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sen.GetComponent<PlayerHealth>().PlayerTakeDamage(attackDamage);
        }
    }
}
