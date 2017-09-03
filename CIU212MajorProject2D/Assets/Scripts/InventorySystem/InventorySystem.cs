using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

    public static InventorySystem instance;
    public List<Item> items = new List<Item>();
    private int space = 15;

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
        if (items.Count >= space)
        {
            Debug.Log("Not enough room!");
            return false;
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
