using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDamageHitBox : MonoBehaviour
{

    private GameObject sen;
    private bossAttack BA;

    void Start()
    {
        sen = GameObject.Find("Sen");
    }

    void OnTriggerEnter2D(Collider2D other)
    {   

        if (other.gameObject.tag == "Player")
        {

            sen.GetComponent<PlayerHealth>().PlayerTakeDamage(BA.attackDamage);
        }
    }
}
