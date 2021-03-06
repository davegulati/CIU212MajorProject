﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GroundEnemy")
        {
            if (collision.gameObject.GetComponent<GroundEnemy>() != null)
            {
                collision.gameObject.GetComponent<GroundEnemy>().FindPatrolPoints(gameObject);
                collision.gameObject.GetComponent<GroundEnemy>().JoinedZone();

            }
        }

		if (collision.gameObject.tag == "RangedEnemy")
		{
			if (collision.gameObject.GetComponent<RangedEnemy>() != null)
			{
				collision.gameObject.GetComponent<RangedEnemy>().FindPatrolPoints(gameObject);
				collision.gameObject.GetComponent<RangedEnemy>().JoinedZone();

			}
		}
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "GroundEnemy")
		{
			if (collision.gameObject.GetComponent<GroundEnemy>() != null)
			{
                collision.gameObject.GetComponent<GroundEnemy>().LeftZone();
			}
		}

		if (collision.gameObject.tag == "RangedEnemy")
		{
			if (collision.gameObject.GetComponent<RangedEnemy>() != null)
			{
				collision.gameObject.GetComponent<RangedEnemy>().LeftZone();
			}
		}
	}
}