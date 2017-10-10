using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Potion : MonoBehaviour {

	private float healthAwarded;
    private float healthDivider = 2.0f;

    public void Use()
	{
        healthAwarded = PlayerCharacterController.instance.gameObject.GetComponent<PlayerHealth>().maxPlayerHealth / healthDivider;
		PlayerCharacterController.instance.gameObject.GetComponent<PlayerHealth>().PlayerReceiveHealth(healthAwarded);
	}
}
