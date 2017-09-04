using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPowder : MonoBehaviour {

	private GameObject sen;
    private GameObject itemCanvas;
    private float notificationDuration = 3.0f;
	private float activationRange = 0.8f;

    private void Awake()
    {
        sen = GameObject.Find("Sen");
        itemCanvas = transform.Find("ItemCanvas").gameObject;
        itemCanvas.SetActive(false);
    }

    private void Update () 
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange)
		{
            itemCanvas.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                Notification.instance.Display("!", "ITEM UNLOCKED", "Explosive Arrows", "Chance of firing explosive arrows.", "Press 'I' to access your inventory.", notificationDuration);
				UnlockExplosiveArrows();
            }
		}
        else 
        {
            itemCanvas.SetActive(false);
        }
	}

    private void UnlockExplosiveArrows ()
    {
        sen.transform.Find("Bow").GetComponent<Bow>().explosiveArrowsUnlocked = true;
        Destroy(gameObject);
    }
}
