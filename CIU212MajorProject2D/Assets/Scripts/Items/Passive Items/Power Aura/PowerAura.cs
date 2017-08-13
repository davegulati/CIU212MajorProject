﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAura : MonoBehaviour {

	private GameObject sen;
	private float activationRange = 0.8f;
	private float damageMultiplier = 1.15f;

	private void Awake()
	{
		sen = GameObject.Find("Sen");
	}

	private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetButtonDown("Interact"))
		{
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