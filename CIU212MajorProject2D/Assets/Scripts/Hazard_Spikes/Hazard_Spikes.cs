using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Spikes : MonoBehaviour {

    private float damageAmount = 15.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(damageAmount);
        }
    }
}
