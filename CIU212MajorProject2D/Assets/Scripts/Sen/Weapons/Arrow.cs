using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private float destroyAfterSeconds = 3.0f;
    private BoxCollider2D senBoxCollider2D;

    [HideInInspector]
    public int speed = 10;

    [HideInInspector]
    public float damage = 0;

    private void Awake()
    {
        senBoxCollider2D = GameObject.Find("Sen").GetComponent<BoxCollider2D>();
	}
    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
		Destroy(gameObject, destroyAfterSeconds);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(senBoxCollider2D, gameObject.GetComponent<BoxCollider2D>(), true);
            Debug.Log("Collided with player but ignored!");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
