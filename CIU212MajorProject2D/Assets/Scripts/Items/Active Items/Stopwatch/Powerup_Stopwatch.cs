using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Stopwatch : MonoBehaviour {

    private GameObject sen;
    private GameObject itemCanvas;
    private bool activated = false;
	private float activationRange = 0.8f;
    private float notificationDuration = 3.0f;
    private float normalTimeScale = 1.0f;
    private float slowMotionTimeScale = 0.4f;
    private float resetAfterSeconds = 3.0f;

    private void Awake()
    {
        sen = GameObject.Find("Sen");
        itemCanvas = transform.Find("ItemCanvas").gameObject;
        itemCanvas.SetActive(false);
    }

    private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange)
		{
            itemCanvas.SetActive(true);
            if (Input.GetButtonDown("Interact") && !activated)
            {
                Notification.instance.Display("!", "ITEM OBTAINED", "Stopwatch", "Slow down time for a few seconds.", "Press 'I' to access your inventory.", notificationDuration);
				StartCoroutine(SlowMotion());
            }
		}
        else 
        {
            itemCanvas.SetActive(false);
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
