using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour {

    private GameObject sen;
    private float chaseRange = 5.0f;
    private float speed = 3.0f;

    // Stun variables
    private bool isStunned = false;
    private float stunTime = 1.5f;
    private Color normalColor = new Color(255, 255, 255);
    private Color stunnedColor = new Color(0, 0, 255);

    // Patrol variables
    private bool inZone = false;
    private GameObject[] patrolPoints;
    private bool senNoticed = false;
    private int currentPatrolPoint = 0;
    private float patrolFinishDistance = 1.0f;

	void Awake () 
    {   
        sen = GameObject.Find("Sen");
	}
	
	// Update is called once per frame
	private void Update () 
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < chaseRange && !isStunned)
		{
            senNoticed = true;
		}
        else if (distance > chaseRange)
        {
            senNoticed = false;
        }

		if (inZone && !senNoticed && !isStunned)
		{
			// Patrol
			Vector2 position = new Vector2(transform.position.x, transform.position.y);
			Vector2 currentWPPosition = new Vector2(patrolPoints[currentPatrolPoint].transform.position.x, 0);
			transform.position = Vector2.MoveTowards(position, currentWPPosition, speed * Time.deltaTime);
			if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].transform.position) < patrolFinishDistance)
            {
                currentPatrolPoint++;
                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }
            }
		}

        if (senNoticed && !isStunned)
        {
			Vector2 position = new Vector2(transform.position.x, transform.position.y);
			Vector2 senPosition = new Vector2(sen.transform.position.x, 0);
			transform.position = Vector2.MoveTowards(position, senPosition, speed * Time.deltaTime);
        }
	}

    public void Stun ()
    {
        isStunned = true;
        GetComponent<SpriteRenderer>().color = stunnedColor;
        StartCoroutine(StunTimer());
    }

    IEnumerator StunTimer ()
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        GetComponent<SpriteRenderer>().color = normalColor;
    }

    public void FindPatrolPoints (GameObject patrolZone)
    {
        patrolPoints = new GameObject[patrolZone.transform.childCount];
		for (int i = 0; i < patrolZone.transform.childCount; i++)
		{
			patrolPoints[i] = patrolZone.transform.GetChild(i).gameObject;
//            Debug.Log(patrolZone.transform.GetChild(i).gameObject.name);
		}
    }

    public void JoinedZone ()
    {
        inZone = true;
    }

    public void LeftZone()
	{
        inZone = false;
	}
}
