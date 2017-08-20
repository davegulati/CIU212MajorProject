using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
	private GameObject sen;
	private float activationRange = 0.8f;

	private void Awake()
	{
		sen = GameObject.Find("Sen");
	}

	private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetButtonDown("Interact"))
		{
			UnlockStunning();
		}
	}

	private void UnlockStunning()
	{
        sen.GetComponent<PlayerCharacterController>().stunUnlocked = true;
		Destroy(gameObject);
	}
}