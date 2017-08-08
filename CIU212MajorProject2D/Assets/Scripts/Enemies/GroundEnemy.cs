using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour {

    private GameObject sen;
    private float chaseRange = 5.0f;
    private float speed = 3.0f;
    private bool isStunned = false;
    private int stunTime = 3;
    private Color normalColor = new Color(255, 255, 255);
    private Color stunnedColor = new Color(0, 0, 255);

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
}
