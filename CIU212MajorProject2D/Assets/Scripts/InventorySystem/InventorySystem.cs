using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

    public static InventorySystem instance;
    public List<Item> items = new List<Item>();
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
			if (items.Count >= space_ActiveItems)
			{
				Debug.Log("Not enough room!");
				return false;
			}
        }
        else if (item.passiveItem)
        {
			if (items.Count >= space_PassiveItems)
			{
				Debug.Log("Not enough room!");
				return false;
			}
        }

        items.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
    }
}
