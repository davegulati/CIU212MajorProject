using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalBoots : MonoBehaviour {

	private GameObject sen;
	private float activationRange = 0.8f;
    private float senNewSpeed = 15.0f; // Default speed for Sen is 12.5.

	private void Awake()
	{
		sen = GameObject.Find("Sen");
	}

	private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetButtonDown("Interact"))
		{
			GiveSpeedBoost();
		}
	}

	private void GiveSpeedBoost ()
	{
        sen.GetComponent<PlayerCharacterController>().movementSpeed = senNewSpeed;
        Destroy(gameObject);
	}
}