using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour {

	public static ItemsManager instance;

    protected GameObject sen;

    // Live shop variables.
	public GameObject[] allItemsArray;
	public List<GameObject> allItemsList;
	private GameObject[] itemSlots = new GameObject[4];
	private GameObject generatedItem;
	private int index;

	private void Awake()
    {
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of ItemsManager found.");
			return;
		}
        else 
        {
            instance = this;
        }

        sen = GameObject.Find("Sen");

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

	private void GenerateNewItem()
	{
		index = Random.Range(0, allItemsList.Count);
		generatedItem = allItemsList[index];
		allItemsList.RemoveAt(index);
	}

    public void Use(Item item)
    {
        if (item.itemID == 0)
        {
            Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
			allItemsArray[0].GetComponent<Powerup_Potion>().enabled = true;
			allItemsArray[0].GetComponent<Powerup_Potion>().Use();
        }

		if (item.itemID == 1)
		{
			Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
			allItemsArray[1].GetComponent<Powerup_Stopwatch>().enabled = true;
			allItemsArray[1].GetComponent<Powerup_Stopwatch>().Use();
		}
    }
}
