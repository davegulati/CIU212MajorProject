﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
	private Loot loot;

	private GameObject sen;
	private float activationRange = 1.4f;

	// Use this for initialization
	void Start ()
	{
		loot = GameObject.Find("LootManager").GetComponent<Loot>();

		sen = GameObject.Find("Sen");
	}
	
	// Update is called once per frame
	private void Update ()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetKeyDown(KeyCode.R))
		{
			loot.calculateLoot();

			OpenChestAnimation();
		}
	}

	private void OpenChestAnimation()
	{
		gameObject.SetActive(false);
	}
}

