using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSafeZone : MonoBehaviour
{
	private GameObject sen;
	private float activationRange = 3.5f;

	// Use this for initialization
	void Start ()
	{
		sen = GameObject.Find("Sen");
	}

	// Update is called once per frame
	void Update ()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetKeyDown(KeyCode.W))
		{
			SceneManager.LoadScene("Safe Zone Greybox");
		}
	}
}
