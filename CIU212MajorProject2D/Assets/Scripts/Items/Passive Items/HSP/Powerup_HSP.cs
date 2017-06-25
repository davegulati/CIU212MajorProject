using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_HSP : MonoBehaviour {

	private GameObject sen;
	private float activationRange = 0.8f;
	private float amountToIncreaseBy = 30.0f;

	private void Awake()
	{
		sen = GameObject.Find("Sen");
	}
	
	private void Update () 
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetKeyDown(KeyCode.E))
		{
			IncreaseMaxHealth();
		}		
	}

	private void IncreaseMaxHealth()
	{
		sen.GetComponent<PlayerHealth>().IncreaseMaxHealth(amountToIncreaseBy);
		Destroy(gameObject);
	}
}
