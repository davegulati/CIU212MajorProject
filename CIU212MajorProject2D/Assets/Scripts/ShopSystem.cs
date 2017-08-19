using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopSystem : MonoBehaviour
{
    public Inventory inventoryScript;
    public InventoryDataBase data;
    // Use this for initialization
    void Start ()
    {
        inventoryScript.SearchForSameItem();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other)
    {
        Inventory search = gameObject.GetComponent<Inventory>();
        search.SearchForSameItem();

		if (other.transform.tag == "ShopItem" && Input.GetButtonDown("Interact"))
        {
            ItemSelf newitem = other.transform.GetComponent<ItemSelf>();
            //add new variables in itemself and update here for item stats
           //SearchForSameItem(data.items[newitem.ID], newitem.count, newitem.health);
           // UpdateInventory();
            Destroy(other.transform.gameObject);
        }
    }
}
