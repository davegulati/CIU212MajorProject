using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPowder : MonoBehaviour {

	private GameObject sen;
	private float activationRange = 0.8f;
	private float senNewSpeed = 10.0f; // Default speed for Sen is 5.

    private void Awake()
    {
        sen = GameObject.Find("Sen");
    }

    private void Update () 
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetButtonDown("Interact"))
		{
			UnlockExplosiveArrows();
		}
	}

    private void UnlockExplosiveArrows ()
    {
        sen.transform.Find("Bow").GetComponent<Bow>().explosiveArrowsUnlocked = true;
        Destroy(gameObject);
    }
}
