using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	// Update is called once per frame
	void Update () 
    {
		Destroy(gameObject, 4);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.name != "Arrow(Clone)")
        {
            if (collision.gameObject.GetComponent<BoxCollider2D>() != null)
            Destroy(gameObject);
        }
    }
}
