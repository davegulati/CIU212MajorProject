using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalBoots : MonoBehaviour {

	private GameObject sen;
	private float activationRange = 0.8f;
    private float senNewSpeed = 15.0f; // Default speed for Sen is 12.5.
    private float notificationDuration = 3.0f;

	private void Awake()
	{
		sen = GameObject.Find("Sen");
	}

	private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetButtonDown("Interact"))
		{
			Notification.instance.Display("!", "Obtained Mechanical Boots!", "Press 'I' to access your inventory.", notificationDuration);
			GiveSpeedBoost();
		}
	}

	private void GiveSpeedBoost ()
	{
        sen.GetComponent<PlayerCharacterController>().movementSpeed = senNewSpeed;
        Destroy(gameObject);
	}
}