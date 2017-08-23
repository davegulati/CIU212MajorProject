using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalBoots : MonoBehaviour {

	private GameObject sen;
    private GameObject itemCanvas;
	private float activationRange = 0.8f;
    private float senNewSpeed = 10.0f; // Default speed for Sen is 12.5.
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
                Notification.instance.Display("!", "ITEM OBTAINED", "Mechanical Boots", "Increases Sen's movement speed.", "Press 'I' to access your inventory.", notificationDuration);
				GiveSpeedBoost();
            }
		}
        else
        {
            itemCanvas.SetActive(false);
        }
	}

	private void GiveSpeedBoost ()
	{
        sen.GetComponent<PlayerCharacterController>().movementSpeed = senNewSpeed;
        Destroy(gameObject);
	}
}