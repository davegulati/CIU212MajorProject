using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Stopwatch : MonoBehaviour {

    private GameObject sen;
    private bool activated = false;
	private float activationRange = 0.8f;
    private float normalTimeScale = 1.0f;
    private float slowMotionTimeScale = 0.4f;
    private float resetAfterSeconds = 3.0f;

    private void Awake()
    {
        sen = GameObject.Find("Sen");
    }

    private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetButtonDown("Interact") && !activated)
		{
            StartCoroutine(SlowMotion());
		}
	}

    IEnumerator SlowMotion ()
    {
        Time.timeScale = slowMotionTimeScale;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(resetAfterSeconds);
        Time.timeScale = normalTimeScale;
        Destroy(gameObject);
    }
}
