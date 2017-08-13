using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolScript : MonoBehaviour {

	public Transform[] patrolpoints;
	int currentPoint;
	public float speed= 0.5f;


	// Use this for initialization
	void Start () {
		StartCoroutine ("Patrol");	
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	IEnumerable Patrol ()
	{
		while (true) {

			if (transform.position.x == patrolpoints [currentPoint].position.x) 
			
			{
				currentPoint++;
			}		

			if (currentPoint >= patrolpoints.Length)
			{
				currentPoint=0;
			}

			transform.position=Vector2.MoveTowards(transform.position,new Vector2(patrolpoints[currentPoint].position.x,transform.position.y),speed);



			yield return null;


					}


	}

}
