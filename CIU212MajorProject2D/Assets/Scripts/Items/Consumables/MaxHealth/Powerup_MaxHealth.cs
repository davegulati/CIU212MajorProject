using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_MaxHealth : MonoBehaviour {
	
    private GameObject sen;
    private GameObject itemCanvas;
	private float activationRange = 0.8f;
    private float notificationDuration = 3.0f;

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
            if (Input.GetButtonDown("Interact"))
            {
				Notification.instance.Display("!", "Obtained: Max Health!", "Press 'I' to access your inventory.", notificationDuration);
				AwardHealth();
            }
		}
        else 
        {
            itemCanvas.SetActive(false);
        }
	}

	private void AwardHealth()
	{
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(sen.GetComponent<PlayerHealth>().maxPlayerHealth);
		Destroy(gameObject);
	}
}
