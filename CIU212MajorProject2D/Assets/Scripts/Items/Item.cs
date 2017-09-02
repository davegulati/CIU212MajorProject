using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
	[Header("Main Settings")]
	public string itemName = "New Item";
    public Sprite itemSprite = null;

    [Header("Item Type - Select ONLY one.")]
	public bool activeItem = false;
    public bool consumableItem = false;
    public bool passiveItem = false;

	[Header("Inventory Settings")]
	public bool canBeStored = true;
}
