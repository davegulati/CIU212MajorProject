using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

	// Use this for initialization
	void Awake () 
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        if (player.Length > 1)
        {
            Destroy(gameObject);
        }

		GameObject[] canvas = GameObject.FindGameObjectsWithTag("Canvas");
		if (canvas.Length > 1)
		{
			Destroy(gameObject);
		}

        DontDestroyOnLoad(gameObject);
	}
}
