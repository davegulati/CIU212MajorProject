using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
//	public float range;
//	private GameObject sen;
	public GameObject text;


//	void Start ()
//	{
//		sen = GameObject.Find("Sen");
//	}
	
	void OnTriggerEnter2D(Collider other)
	{
		text.SetActive(true);
	}

	void OnTriggerExit2D(Collider other)
	{
		Destroy(text);
		Destroy(gameObject);
	}
}
