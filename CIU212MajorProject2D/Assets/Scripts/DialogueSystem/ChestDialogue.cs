using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestDialogue : MonoBehaviour
{
	public float activationRange;
	private GameObject sen;
	public GameObject text;


	void Start ()
	{
		sen = GameObject.Find("Sen");
	}
	
	void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange)
		{
			text.SetActive(true);
		}
	}
}
