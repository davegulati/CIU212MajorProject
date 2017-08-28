using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveShop : MonoBehaviour {

    //[SerializeField]
    public GameObject[] allItems;
    private GameObject generatedItem;
    private int index;

    private int item;

    private void Start()
    {
        GenerateItems();
    }

    public GameObject GenerateItems()
    {
		index = Random.Range(0, allItems.Length);
		generatedItem = allItems[index];
        return generatedItem;
    }
}
