using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private GameObject itemInfoPopup;
    [HideInInspector]
    public Item generatedItem;

    private void Awake()
    {
        itemInfoPopup = transform.Find("ItemInfoPopup").gameObject;
        itemInfoPopup.SetActive(false);
    }

    public void AddItem (Item item)
    {
        generatedItem = item;
        gameObject.transform.Find("ItemButton").transform.Find("Icon").transform.Find("Text_Price").GetComponent<Text>().text = "$" + Random.Range(1, 10).ToString();
        gameObject.transform.Find("ItemInfoPopup").transform.Find("Text_Name").GetComponent<Text>().text = item.itemName;
        gameObject.transform.Find("ItemInfoPopup").transform.Find("Text_Description").GetComponent<Text>().text = item.itemName + " description";
        gameObject.transform.Find("ItemButton").transform.Find("Icon").GetComponent<Image>().sprite = item.itemSprite;
    }

    public void OnPointerEnter(PointerEventData eventData) // On mouseover enter
	{
        if (itemInfoPopup != null)
        {
            itemInfoPopup.SetActive(true);
        }
	}

	public void OnPointerExit(PointerEventData eventData) // On mouseover exit
	{
        if (itemInfoPopup != null)
        {
			itemInfoPopup.SetActive(false);
		}
	}


	private void BuyItem()
    {
        if (ItemsManager.instance.playerCurrency_Gold >= generatedItem.itemPrice)
        {
			if (InventorySystem.instance.Add(generatedItem))
			{
				Notification.instance.Display("!", "ITEM PURCHASED", generatedItem.name, generatedItem.name + " description", "Press 'I' to access your inventory", 3.0f);
				RemoveItemFromSlot();
			}
        }
        else
        {
			Notification.instance.Display("!", "INSUFFICIENT FUNDS!", "Not enough gold!", "You do not have enough gold!", "", 3.0f);
		}
    }

    private void RemoveItemFromSlot()
    {
		generatedItem = null;
		gameObject.transform.Find("ItemButton").transform.Find("Icon").GetComponent<Image>().sprite = null;
		gameObject.transform.Find("ItemButton").transform.Find("Icon").GetComponent<Image>().enabled = false;
		gameObject.transform.Find("ItemButton").GetComponent<Button>().interactable = false;
        gameObject.transform.Find("ItemButton").transform.Find("Icon").transform.Find("Text_Price").GetComponent<Text>().enabled = false;
		Destroy(itemInfoPopup);
    }
}
