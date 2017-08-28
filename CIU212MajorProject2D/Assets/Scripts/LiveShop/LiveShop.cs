using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveShop : MonoBehaviour {
    
    public GameObject[] allItems;
    public GameObject[] itemSlots;
    private GameObject generatedItem;
    private int index;

    private void Awake()
    {
        GenerateItems();
    }

    public void GenerateItems()
    {
        foreach (GameObject itemSlot in itemSlots)
        {
			index = Random.Range(0, allItems.Length);
			generatedItem = allItems[index];
            itemSlot.GetComponent<ItemSlot>().AddItem(generatedItem);
        }
    }
}