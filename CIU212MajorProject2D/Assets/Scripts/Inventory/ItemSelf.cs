using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelf : MonoBehaviour
{

    public int ID;
    public int count;
    public int value;

    //private Inventory;

    //private void Start()
    //{
    //    Inventory search = gameObject.GetComponent<Inventory>();
    //    search.SearchForSameItem()
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.CompareTag("ShopItem") && Input.GetKeyDown(KeyCode.E))
    //    {
    //        ItemSelf newitem = other.transform.GetComponent<ItemSelf>();
    //        //add new variables in itemself and update here for item stats
    //        SearchForSameItem(data.items[newitem.ID], newitem.count, newitem.health);
    //        UpdateInventory();
    //        Destroy(other.transform.gameObject);
    //    }
    //}
}
