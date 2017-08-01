using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private float destroyAfterSeconds = 3.0f;

    [HideInInspector]
    public int speed = 10;

    [HideInInspector]
    public float damage = 0;

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
		Destroy(gameObject, destroyAfterSeconds);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<GroundEnemy>() != null)
            {
                collision.gameObject.GetComponent<GroundEnemyHealth>().DamageEnemy(40);
                Destroy(gameObject);
            }
        }
    }
}
