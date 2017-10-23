using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

    public static InventorySystem instance;
    public List<Item> activeItems = new List<Item>();
	public List<Item> passiveItems = new List<Item>();
	private int space_ActiveItems = 2;
    private int space_PassiveItems = 4;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

	private void Awake()
	{
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventorySystem found.");
            return;
        }
		instance = this;
	}

    public bool Add (Item item)
    {
        if (item.activeItem)
        {
			if (activeItems.Count >= space_ActiveItems)
			{
				Debug.Log("Not enough room for another active item!");
                Notification.instance.Display("!", "INVENTORY FULL!", "Not enough room!", "Maximum number of active items reached!", "", 3.0f);
				return false;
			}
            else if (activeItems.Count < space_ActiveItems)
            {
                activeItems.Add(item);
				if (onItemChangedCallback != null)
				{
					onItemChangedCallback.Invoke();
				}
				return true;
            }
        }
        else if (item.passiveItem)
        {
			if (passiveItems.Count >= space_PassiveItems)
			{
				Debug.Log("Not enough room for another passive item!");
				Notification.instance.Display("!", "INVENTORY FULL!", "Not enough room!", "Maximum number of passive items reached!", "", 3.0f);
				return false;
			}
			else if (passiveItems.Count < space_PassiveItems)
			{
				passiveItems.Add(item);
				if (onItemChangedCallback != null)
				{
					onItemChangedCallback.Invoke();
				}
				return true;
			}
        }
        else if (item.consumableItem)
        {
            item.Use();
            return true;
        }
        return false;
    }

    public void Remove (Item item)
    {
        if (item.activeItem)
        {
			activeItems.Remove(item);
			if (onItemChangedCallback != null)
			{
				onItemChangedCallback.Invoke();
			}
        }
        else if (item.passiveItem)
		{
			passiveItems.Remove(item);
			if (onItemChangedCallback != null)
			{
				onItemChangedCallback.Invoke();
			}
        }
    }

	public void Use (Item item)
	{
		if (item.activeItem)
		{
			if (onItemChangedCallback != null)
			{
				onItemChangedCallback.Invoke();
			}
		}
		else if (item.passiveItem)
		{
			//passiveItems.Remove(item);
			if (onItemChangedCallback != null)
			{
				onItemChangedCallback.Invoke();
			}
		}
	}
}
