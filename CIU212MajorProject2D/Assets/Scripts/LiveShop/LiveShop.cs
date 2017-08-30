using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveShop : MonoBehaviour {
    
    public GameObject[] allItems;
    public List<GameObject> allItemsList;
    public GameObject[] itemSlots;

    private GameObject generatedItem;
    private int index;

    private void Awake()
    {
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