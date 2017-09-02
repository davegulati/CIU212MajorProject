using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
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
				Notification.instance.Display("!", "ITEM UNLOCKED", "Stun", "Stun the nearest enemy for a few seconds.", "Press 'I' to access your inventory.", notificationDuration);
				UnlockStunning();
            }
        }
        else
        {
            itemCanvas.SetActive(false);
        }
	}

	private void UnlockStunning()
	{
        sen.GetComponent<PlayerCharacterController>().stunUnlocked = true;
		Destroy(gameObject);
	}
}