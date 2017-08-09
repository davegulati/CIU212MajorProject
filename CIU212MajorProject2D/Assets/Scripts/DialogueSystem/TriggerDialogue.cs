﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
	public float activationRange;
	private GameObject sen;
	public GameObject text;

	private void Awake ()
	{
		sen = GameObject.Find("Sen");
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.SetActive(true);
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		Destroy(text);
		Destroy(gameObject);
	}

}