using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour {

    private GameObject sen;
    private float chaseRange = 5.0f;
    private float speed = 3.0f;

	void Awake () 
    {   
        sen = GameObject.Find("Sen");
	}
	
	// Update is called once per frame
	private void Update () 
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < chaseRange)
		{
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            Vector2 senPosition = new Vector2(sen.transform.position.x, 0);
            transform.position = Vector2.MoveTowards(position, senPosition, speed * Time.deltaTime);
		}
	}
}
