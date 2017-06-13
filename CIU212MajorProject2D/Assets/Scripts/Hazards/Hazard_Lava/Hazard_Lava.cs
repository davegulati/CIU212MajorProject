using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Lava : MonoBehaviour {

    private float damageAmount = 10.0f;
    private bool canDamage = true;
    private float damageCooldownTime = 1.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canDamage)
            {
                collision.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(damageAmount);
                canDamage = false;
                StartCoroutine (DamageCooldown());
                Debug.Log("Player collided and was damaged!");
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("collision stay called"); 
        if (collision.gameObject.tag == "Player")
        {
            if (canDamage)
            {
				collision.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(damageAmount);
				canDamage = false;
                StartCoroutine(DamageCooldown());
                Debug.Log("Player is staying and was damaged!");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canDamage = true;
            Debug.Log("Player left!");
        }
    }

    IEnumerator DamageCooldown ()
    {
        yield return new WaitForSeconds(damageCooldownTime);
        canDamage = true;
    }
}
