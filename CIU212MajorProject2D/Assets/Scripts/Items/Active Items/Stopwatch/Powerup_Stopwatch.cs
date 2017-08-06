using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Stopwatch : MonoBehaviour {

    private GameObject sen;
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
		if (distance < activationRange && Input.GetButtonDown("Interact"))
		{
            Time.timeScale = slowMotionTimeScale;
            StartCoroutine(ResetTimeAfterSeconds());
		}
	}

    IEnumerator ResetTimeAfterSeconds ()
    {
        yield return new WaitForSeconds(resetAfterSeconds);
        Time.timeScale = normalTimeScale;
    }
}
