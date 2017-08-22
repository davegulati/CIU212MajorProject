using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPowder : MonoBehaviour {

	private GameObject sen;
    private float notificationDuration = 3.0f;
	private float activationRange = 0.8f;

    private void Awake()
    {
        sen = GameObject.Find("Sen");
    }

    private void Update () 
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetButtonDown("Interact"))
		{
            Notification.instance.Display("!", "Unlocked: Explosive Arrows!", "Press 'I' to access your inventory.", notificationDuration);
			UnlockExplosiveArrows();
		}
	}

    private void UnlockExplosiveArrows ()
    {
        sen.transform.Find("Bow").GetComponent<Bow>().explosiveArrowsUnlocked = true;
        Destroy(gameObject);
    }
}
