using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private GameObject itemInfoPopup;
    private GameObject itemPurchasedPopup;
    private GameObject generatedItem;

    private void Awake()
    {
        itemInfoPopup = transform.Find("ItemInfoPopup").gameObject;
        itemInfoPopup.SetActive(false);
		itemPurchasedPopup = transform.Find("ItemPurchasedPopup").gameObject;
		itemPurchasedPopup.SetActive(false);
        GenerateItem();
    }

    public void AddItem (string name, string description, int price)
    {
        gameObject.transform.Find("ItemButton").transform.Find("Icon").transform.Find("Text_Price").GetComponent<Text>().text = "$" + price.ToString();
        gameObject.transform.Find("ItemInfoPopup").transform.Find("Text_Name").GetComponent<Text>().text = name;
        gameObject.transform.Find("ItemInfoPopup").transform.Find("Text_Description").GetComponent<Text>().text = description;
        gameObject.transform.Find("ItemButton").transform.Find("Icon").GetComponent<Image>().sprite = generatedItem.GetComponent<SpriteRenderer>().sprite;
    }

    private void GenerateItem()
    {
        generatedItem = GameObject.Find("LiveShop").GetComponent<LiveShop>().GenerateItems();
        AddItem(generatedItem.name, generatedItem.name + " description...", Random.Range(1, 10));
    }

    public void OnPointerEnter(PointerEventData eventData) // On mouseover enter
	{
        itemInfoPopup.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData) // On mouseover exit
	{
		itemInfoPopup.SetActive(false);
        itemPurchasedPopup.SetActive(false);
	}


	public void BuyItem()
    {
        Debug.Log("Bought item in " + gameObject.name);
        itemInfoPopup.SetActive(false);
        itemPurchasedPopup.SetActive(true);
    }
}
