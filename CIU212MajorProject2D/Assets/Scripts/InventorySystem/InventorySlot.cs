using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [HideInInspector]
    public Item item;
    private Image icon;
	private Color useColor = new Color(0, 255, 0, 0.5f);
	private Color cooldownColor = new Color(0, 0, 255, 0.5f);
    private Button removeButton;
    private Button itemButton;
    private GameObject itemInfoPopup;
    private Text itemNameText;
    private Text itemDescriptionText;

    private void Awake()
    {
        icon = gameObject.transform.Find("ItemButton").transform.Find("Icon").GetComponent<Image>();
		removeButton = transform.Find("RemoveButton").GetComponent<Button>();
        itemButton = gameObject.transform.Find("ItemButton").GetComponent<Button>();
        itemInfoPopup = transform.Find("ItemInfoPopup").gameObject;
        itemNameText = itemInfoPopup.transform.Find("Text_Name").GetComponent<Text>();
		itemDescriptionText = itemInfoPopup.transform.Find("Text_Description").GetComponent<Text>();
		itemInfoPopup.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemDescriptionText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UpdateSlot();
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
        Notification.instance.Display("!", "ITEM DROPPED", item.itemName, null, null, 3.0f);
        itemInfoPopup.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemDescriptionText.gameObject.SetActive(false);
        item.Drop();
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

			if (item.activeItem)
			{
				item.Use();
                itemButton.interactable = false;
                removeButton.gameObject.SetActive(false);
                //StartCoroutine(DisplayUseProgress(item));
				itemInfoPopup.SetActive(false);
				itemNameText.gameObject.SetActive(false);
				itemDescriptionText.gameObject.SetActive(false);
                //itemButton.interactable = false;
				InventorySystem.instance.Use(item);
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

    public void UpdateSlot ()
    {
		if (item != null)
		{
			if (item.beingUsed)
			{
				itemButton.interactable = false;
				removeButton.gameObject.SetActive(false);
			}
			else if (!item.beingUsed)
			{
				itemButton.interactable = true;
				removeButton.gameObject.SetActive(true);
			}
		}
    }
}
