using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Undefined Item")]
public class Item : ScriptableObject
{
    [Header("Main Settings")]
    public int itemID = 0;
	public string itemName = "New item";
    public string itemDescription = "Item description";
    public Sprite itemSprite = null;
    public GameObject itemPrefab;

    [Header("Item Type - Select ONLY one.")]
	public bool activeItem = false;
    public bool consumableItem = false;
    public bool passiveItem = false;

    [Header("Item usage Settings")]
    public string onUseDialogue = "When item is used";

	[Header("Inventory Settings")]
	public bool canBeStored = true;

    public virtual void Use()
    {
        ItemsManager.instance.Use(this);
    }

    public virtual void Drop()
    {
        ItemsManager.instance.Drop(this);
    } 
}
