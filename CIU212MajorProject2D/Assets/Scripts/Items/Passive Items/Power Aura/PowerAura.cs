using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAura : MonoBehaviour {

	private GameObject sen;
	private float activationRange = 0.8f;
	private float damageMultiplier = 1.15f;
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
			Notification.instance.Display("!", "Obtained Power Aura!", "Press 'I' to access your inventory.", notificationDuration);
			IncreaseDamage();
		}
	}

	private void IncreaseDamage()
	{
        sen.transform.Find("Bow").GetComponent<Bow>().EnhanceWeaponStats_PowerAura(damageMultiplier);
        sen.transform.Find("Axe").GetComponent<Axe>().EnhanceWeaponStats_PowerAura(damageMultiplier);
		Destroy(gameObject);
	}
}