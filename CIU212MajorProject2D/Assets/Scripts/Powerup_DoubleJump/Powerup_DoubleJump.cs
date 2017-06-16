using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_DoubleJump : MonoBehaviour {

    private GameObject sen;
    private float activationRange = 0.8f;

    private void Awake()
    {
        sen = GameObject.Find("Sen");
    }

	private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetKeyDown(KeyCode.E))
		{
			UnlockDoubleJump();
		}
	}

    private void UnlockDoubleJump()
    {
		sen.GetComponent<PlayerCharacterController>().doubleJumpUnlocked = true;
		Destroy(gameObject);
    }
}
