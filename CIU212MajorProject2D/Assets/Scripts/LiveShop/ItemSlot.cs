using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private GameObject itemInfoPopup;
    private GameObject generatedItem;

    private void Awake()
    {
        itemInfoPopup = transform.Find("ItemInfoPopup").gameObject;
        itemInfoPopup.SetActive(false);
    }

    public void AddItem (GameObject item)
    {
        generatedItem = item;
        gameObject.transform.Find("ItemButton").transform.Find("Icon").transform.Find("Text_Price").GetComponent<Text>().text = "$" + Random.Range(1, 10).ToString();
        gameObject.transform.Find("ItemInfoPopup").transform.Find("Text_Name").GetComponent<Text>().text = item.name;
        gameObject.transform.Find("ItemInfoPopup").transform.Find("Text_Description").GetComponent<Text>().text = item.name + " description";
        gameObject.transform.Find("ItemButton").transform.Find("Icon").GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
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
		Notification.instance.Display("!", "ITEM PURCHASED", generatedItem.name, generatedItem.name + " description", "Press 'I' to access your inventory", 3.0f);
		// Add to inventory
		RemoveItemFromSlot();
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
