using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour {

	public static ItemsManager instance;

    public GameObject vitaminCPill;

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
    }

    public void Use(Item item)
    {
        if (item.itemID == 0)
        {
            
        }
    }
}
