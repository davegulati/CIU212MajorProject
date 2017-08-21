using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelShotArrowItem : MonoBehaviour
{
    private GameObject sen;
	private float activationRange = 0.8f;
	private float amountToIncreaseBy = 30.0f;

	private void Awake()
	{
		sen = GameObject.Find("Sen");
	}

	private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetKeyDown(KeyCode.R))
		{
			UnlockSteelShotArrows();
		}
	}

	private void UnlockSteelShotArrows()
	{
		sen.transform.Find("Bow").GetComponent<Bow>().steelShotArrowsUnlocked = true;
		Destroy(gameObject);
	}
}
