using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_BreakerBlade : MonoBehaviour
{
	
	private GameObject sen;
	private float activationRange = 0.8f;


	void Start ()
	{
		sen = GameObject.Find("Sen");
	}
	

	private void Update ()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetKeyDown(KeyCode.E))
		{
			CollectItem();
		}
	}

	private void CollectItem()
	{
		Destroy(gameObject);
	}
}