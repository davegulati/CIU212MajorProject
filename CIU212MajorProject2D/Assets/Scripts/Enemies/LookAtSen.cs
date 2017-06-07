using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSen : MonoBehaviour {

    private Transform sen;

	// Use this for initialization
	void Start () 
    {
        sen = GameObject.Find("Sen").GetComponent<Transform>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.LookAt(sen);
	}
}
