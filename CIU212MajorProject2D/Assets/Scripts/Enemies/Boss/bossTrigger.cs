using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTrigger : MonoBehaviour
{
    private GameObject boss;
	// Use this for initialization
	void Start ()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine("Patrol");
            boss.GetComponent<bossAttack>().enabled = true;
            boss.GetComponent<bossMovementController>().enabled = false;
        }
    }
}
