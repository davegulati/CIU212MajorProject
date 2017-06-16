using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_DoubleJump : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
            collision.gameObject.GetComponent<PlayerCharacterController>().doubleJumpUnlocked = true;
			Destroy(gameObject);
		}
	}
}
