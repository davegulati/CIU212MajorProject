using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Item", menuName = "Items/Passive Item")]
public class PassiveItem : Item
{

    [Header("Passive Item Settings")]
    public string test = "Test String!";

	public override void Use()
	{
		base.Use();
		Debug.Log("Test passive lol");
	}
}
