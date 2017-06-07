using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSen : MonoBehaviour {

    private Transform sen;
    private float finishDistance;

	// Use this for initialization
	void Start () 
    {
        sen = GameObject.Find("Sen").GetComponent<Transform>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //float distance;
        //if ()
	}
}
