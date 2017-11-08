using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_FireBurstHole : MonoBehaviour {

    private float damageAmount = 15.0f;

    public void ActivateTrigger ()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DeactivateTrigger ()
    {
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}

    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(damageAmount);
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(other.gameObject.transform.localScale.x * -5000, 100));
		}
	}
}
