using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorialChest : MonoBehaviour
{
	private TutorialLoot tutorialLoot;

	private GameObject sen;
	private float activationRange = 1.4f;

	// Use this for initialization
	void Start ()
	{
		tutorialLoot = GameObject.Find ("LootManager").GetComponent<TutorialLoot>();

		sen = GameObject.Find("Sen");
	}
	
	// Update is called once per frame
	private void Update ()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetKeyDown(KeyCode.E))
		{
			tutorialLoot.CalculateLoot();

			OpenChestAnimation();
		}
	}

	private void OpenChestAnimation()
	{
		gameObject.SetActive(false);
	}
}

