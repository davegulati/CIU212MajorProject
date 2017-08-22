using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Potion : MonoBehaviour {

	private GameObject sen;
	private float activationRange = 0.8f;
    private float notificationDuration = 3.0f;
	private float healthAwarded;
    private float healthDivider = 2.0f;

	private void Awake()
	{
		sen = GameObject.Find("Sen");
	}

	private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetButtonDown("Interact"))
		{
			Notification.instance.Display("!", "Obtained Potion!", "Press 'I' to access your inventory.", notificationDuration);
			AwardHealth();
		}
	}

	private void AwardHealth()
	{
        healthAwarded = sen.GetComponent<PlayerHealth>().maxPlayerHealth / healthDivider;
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(healthAwarded);
		Destroy(gameObject);
	}
}
