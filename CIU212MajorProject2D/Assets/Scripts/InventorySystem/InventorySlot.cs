using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    Item item;
    private Image icon;
    private Button removeButton;
    private GameObject itemInfoPopup;
    private Text itemNameText;
    private Text itemDescriptionText;

    private void Awake()
    {
        icon = gameObject.transform.Find("ItemButton").transform.Find("Icon").GetComponent<Image>();
        removeButton = transform.Find("RemoveButton").GetComponent<Button>();
        itemInfoPopup = transform.Find("ItemInfoPopup").gameObject;
        itemNameText = itemInfoPopup.transform.Find("Text_Name").GetComponent<Text>();
		itemDescriptionText = itemInfoPopup.transform.Find("Text_Description").GetComponent<Text>();
		itemInfoPopup.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemDescriptionText.gameObject.SetActive(false);
    }

    public void AddItem (Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemSprite;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot ()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton ()
    {
        Notification.instance.Display("!", "ITEM REMOVED", item.itemName, null, null, 3.0f);
        itemInfoPopup.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemDescriptionText.gameObject.SetActive(false);
        InventorySystem.instance.Remove(item);
    }

    public void UseItem ()
    {
        if (item != null)
        {
            if (item.consumableItem)
            {
				Notification.instance.Display("!", "ITEM USED", item.itemName, item.onUseDialogue, null, 3.0f);
				item.Use();
                itemInfoPopup.SetActive(false);
                itemNameText.gameObject.SetActive(false);
                itemDescriptionText.gameObject.SetActive(false);
                InventorySystem.instance.Remove(item);
            }
        }
    }

	public void OnPointerEnter(PointerEventData eventData) // On mouseover enter
	{
		if (item != null)
		{
            if (itemInfoPopup != null)
            {
                itemInfoPopup.SetActive(true);
                itemNameText.gameObject.SetActive(true);
                itemDescriptionText.gameObject.SetActive(true);
                itemNameText.text = item.itemName;
                itemDescriptionText.text = item.itemDescription;
            }
		}
	}

	public void OnPointerExit(PointerEventData eventData) // On mouseover exit
	{
		itemInfoPopup.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemDescriptionText.gameObject.SetActive(false);
	}
}
