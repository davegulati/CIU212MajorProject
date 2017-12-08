using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadVolcano : MonoBehaviour
{
	private GameObject sen;
	public GameObject ui;
	private float activationRange = 3.5f;

    private GameManager gameManager;

	// Use this for initialization
	void Start ()
	{
		sen = GameObject.Find("Sen");
//		ui = GameObject.Find("LoadJungle");
	}

	// Update is called once per frame
	void Update ()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange)
		{
			ui.gameObject.SetActive(true);
		}

		if (distance > activationRange)
		{
			ui.gameObject.SetActive(false);
		}

		if (distance < activationRange && Input.GetKeyDown(KeyCode.W))
		{
            SceneManager.LoadScene("VolcanoGreybox");
		}
	}
}
