using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelShotArrow : MonoBehaviour
{
	private float destroyAfterSeconds = 3.0f;
	private int speed = 20;

	// Damage Amount
	[HideInInspector]
	public float current_DamageAmount = 30;

	private void Update()
	{
		transform.Translate(Vector3.up * Time.deltaTime * speed);
		Destroy(gameObject, destroyAfterSeconds);
	}

	private void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.gameObject.tag == "GroundEnemy")
		{
			if (collision.gameObject.GetComponent<GroundEnemyHealth>() != null)
			{
				collision.gameObject.GetComponent<GroundEnemyHealth>().DamageEnemy(current_DamageAmount);
			}
		}

        if (collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }

		if (collision.gameObject.tag == "DropPlatform")
		{
			Destroy(gameObject);
		}
	}
}
