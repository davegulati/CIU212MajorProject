using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDDOL: MonoBehaviour {

	// Use this for initialization
	void Awake () 
    {
		GameObject[] canvas = GameObject.FindGameObjectsWithTag("Canvas");
		if (canvas.Length > 1)
		{
			Destroy(gameObject);
		}
	}
}
