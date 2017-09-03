﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
	[Header("Main Settings")]
	public string itemName = "New item";
    public string itemDescription = "Item description";
    public Sprite itemSprite = null;

    [Header("Item Type - Select ONLY one.")]
	public bool activeItem = false;
    public bool consumableItem = false;
    public bool passiveItem = false;

    [Header("Usage Settings")]
    public string onUseDescription = "When item is used";

	[Header("Inventory Settings")]
	public bool canBeStored = true;

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }
}
