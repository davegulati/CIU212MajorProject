using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Active Item", menuName = "Items/Active Item")]
public class ActiveItem : Item {

	[Header("Active Item Settings")]
	public float useTime = 5.0f;
	public float cooldownTime = 5.0f;

    public override void Use()
    {
        base.Use();
        Debug.Log("Test lol");
    }
}
