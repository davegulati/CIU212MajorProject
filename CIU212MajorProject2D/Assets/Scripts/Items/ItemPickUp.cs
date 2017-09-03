using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : InteractableItem {

    public Item item;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }

    public override void Interact()
    {
        PickUp();
    }

    private void PickUp()
    {
        if (item.canBeStored)
        {
			Debug.Log("Picking up " + item.itemName + ("."));
			bool wasPickedUp = InventorySystem.instance.Add(item);
			if (wasPickedUp)
			{
				Destroy(gameObject);
			}
        }
        else if (item.consumableItem)
        {
            item.Use();
            Destroy(gameObject);
        }
    }
}
