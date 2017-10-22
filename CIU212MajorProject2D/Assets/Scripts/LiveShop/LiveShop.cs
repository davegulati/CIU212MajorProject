using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveShop : MonoBehaviour {
    
    public GameObject[] allItems;
    public List<Item> allItemsList;
    private GameObject[] itemSlots = new GameObject[4];

    private Item generatedItem;
    private int index;

    private void Awake()
    {
        itemSlots[0] = GameObject.Find("LiveShop").transform.Find("Canvas_LiveShop").transform.Find("BG").transform.Find("ItemSlotsBG").transform.Find("ItemSlot1").gameObject;
		itemSlots[1] = GameObject.Find("LiveShop").transform.Find("Canvas_LiveShop").transform.Find("BG").transform.Find("ItemSlotsBG").transform.Find("ItemSlot2").gameObject;
		itemSlots[2] = GameObject.Find("LiveShop").transform.Find("Canvas_LiveShop").transform.Find("BG").transform.Find("ItemSlotsBG").transform.Find("ItemSlot3").gameObject;
		itemSlots[3] = GameObject.Find("LiveShop").transform.Find("Canvas_LiveShop").transform.Find("BG").transform.Find("ItemSlotsBG").transform.Find("ItemSlot4").gameObject;
		AddItemsToSlots();
    }

    public void AddItemsToSlots()
    {
        foreach (GameObject itemSlot in itemSlots)
        {
            GenerateNewItem();
            itemSlot.GetComponent<ItemSlot>().AddItem(generatedItem);
        }
    }

    private void GenerateNewItem ()
    {
		index = Random.Range(0, allItemsList.Count);
		generatedItem = allItemsList[index];
        allItemsList.RemoveAt(index);
    }
}