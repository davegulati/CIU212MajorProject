using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {

    private GameObject[] rangedEnemies;
    private GameObject[] groundEnemies;

	private float destroyAfterSeconds = 3.0f;
	private int speed = 20;

	// Damage Amount
	private float current_DamageAmount = 30;

    private void Awake()
    {
		rangedEnemies = GameObject.FindGameObjectsWithTag("RangedEnemy");
		foreach (GameObject rangedEnemy in rangedEnemies)
		{
			Physics2D.IgnoreCollision(rangedEnemy.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}

        groundEnemies = GameObject.FindGameObjectsWithTag("GroundEnemy");
		foreach (GameObject groundEnemy in groundEnemies)
		{
			Physics2D.IgnoreCollision(groundEnemy.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}
    }

    private void Update()
	{
		transform.Translate(Vector3.up * Time.deltaTime * speed);
		Destroy(gameObject, destroyAfterSeconds);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if (collision.gameObject.GetComponent<PlayerHealth>() != null)
			{
				collision.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(current_DamageAmount);
				Destroy(gameObject);
			}
		}
		Destroy(gameObject);
	}
}
