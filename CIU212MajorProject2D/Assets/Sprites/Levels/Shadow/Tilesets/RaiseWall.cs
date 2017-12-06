using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseWall : MonoBehaviour
{
    public GameObject wall;
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other)
    {
		if(other.gameObject.tag == "Player")
        {
            wall.SetActive(true);
        }
	}
}
