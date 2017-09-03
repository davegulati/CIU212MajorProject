using UnityEngine;

public class InventoryUI : MonoBehaviour {

    private GameObject inventory;
    private Transform itemsParent;
    InventorySystem inventorySystem;
    InventorySlot[] slots;

	// Use this for initialization
	void Start () {
        inventory = transform.Find("Inventory").gameObject;
        inventorySystem = InventorySystem.instance;
        inventorySystem.onItemChangedCallback += UpdateUI;
        itemsParent = gameObject.transform.Find("Inventory").transform.Find("ItemsParent");
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventory.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventory.SetActive(!inventory.activeSelf);
        }
	}

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventorySystem.items.Count)
            {
                slots[i].AddItem(inventorySystem.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
