using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarekPoisonAttack : MonoBehaviour
{
    private GameObject sen;
    private float attackDamage = 5.0f;

    void Start()
    {
        sen = GameObject.Find("Sen");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sen.GetComponent<PlayerHealth>().PlayerTakeDamage(attackDamage);
        }
    }

}
