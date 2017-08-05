using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Lava : MonoBehaviour {

    private int senDamageAmount = 20;
    private Vector2 senForce = new Vector2(0, 200);

    private int groundEnemyDamageAmount = 20;
    private Vector2 groundEnemyForce = new Vector2(0, 200);

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(senDamageAmount);
			collision.gameObject.GetComponent<Rigidbody2D>().AddForce(senForce);
		}

		if (collision.gameObject.tag == "GroundEnemy")
		{
            if (collision.gameObject.GetComponent<GroundEnemyHealth>() != null)
            {
                collision.gameObject.GetComponent<GroundEnemyHealth>().DamageEnemy(groundEnemyDamageAmount);
            }
			collision.gameObject.GetComponent<Rigidbody2D>().AddForce(groundEnemyForce);
		}
	}
}
