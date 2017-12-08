using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private GameObject sen;
    private float destroyAfterSeconds = 3.0f;

    private int speed = 20;

    // Damage Amount
	[HideInInspector]
	public float current_DamageAmount = 30;

    private void Awake()
    {
        sen = GameObject.Find("Sen");
        Physics2D.IgnoreCollision(sen.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    private void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
		Destroy(gameObject, destroyAfterSeconds);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GroundEnemy")
        {
            if (collision.gameObject.GetComponent<GroundEnemyHealth>() != null)
            {
                collision.gameObject.GetComponent<GroundEnemyHealth>().DamageEnemy(current_DamageAmount);
                Destroy(gameObject);
            }
        }

		if (collision.gameObject.tag == "RangedEnemy")
		{
			if (collision.gameObject.GetComponent<RangedEnemyHealth>() != null)
			{
				collision.gameObject.GetComponent<RangedEnemyHealth>().DamageEnemy(current_DamageAmount);
				Destroy(gameObject);
			}
		}

        if (collision.gameObject.tag == "Boss")
        {
            if (collision.gameObject.GetComponent<bossAttack>() != null)
            {
                collision.gameObject.GetComponent<bossAttack>().DamageEnemy(current_DamageAmount);
                Destroy(gameObject);
            }
        }

        Destroy(gameObject);
    }
}
