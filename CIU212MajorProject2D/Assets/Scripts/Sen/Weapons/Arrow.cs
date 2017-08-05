using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private float destroyAfterSeconds = 3.0f;

    [HideInInspector]
    public int speed = 10;

    // Damage Amounts
    [HideInInspector]
    public int damageAmount_GroundEnemy = 40;

	void Update ()
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
                collision.gameObject.GetComponent<GroundEnemyHealth>().DamageEnemy(damageAmount_GroundEnemy);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);
    }
}
