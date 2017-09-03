using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    Item item;
    private Image icon;
    private Button removeButton;

    private void Awake()
    {
        icon = gameObject.transform.Find("ItemButton").transform.Find("Icon").GetComponent<Image>();
        removeButton = transform.Find("RemoveButton").GetComponent<Button>();
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
        InventorySystem.instance.Remove(item);
    }

    public void UseItem ()
    {
        if (item != null)
        {
            if (item.consumableItem)
            {
				Notification.instance.Display("!", "ITEM USED", item.itemName, item.onUseDescription, null, 3.0f);
				item.Use();
                InventorySystem.instance.Remove(item);
            }
        }
    }
}
