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
        Debug.Log("Picking up " + item.itemName + ("."));
        // Add to inventory
        Destroy(gameObject);
    }
}
