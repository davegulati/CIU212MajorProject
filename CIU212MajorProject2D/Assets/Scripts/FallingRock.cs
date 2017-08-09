using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour {

    private Rigidbody2D rb;
    private GameObject sen;
    private float activationRange = 6;
    private float damage = 20;
    private bool canDamage = true;

	void Awake () 
    {
        sen = GameObject.Find("Sen");
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
	}
	
	void Update () 
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange)
		{
			InitiateFall();
		}		
	}

    private void InitiateFall()
    {
        rb.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canDamage)
        {
			collision.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(damage);
            canDamage = false;
            Destroy(gameObject);
        }

		if (collision.gameObject.tag == "Platform")
		{
			canDamage = false;
            Destroy(rb);
            Destroy(GetComponent<FallingRock>());
		}

		if (collision.gameObject.tag == "DropPlatform")
		{
			canDamage = false;
            Destroy(rb);
			Destroy(GetComponent<FallingRock>());

		}
    }
}
