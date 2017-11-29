using UnityEngine;

public class InventoryUI : MonoBehaviour {

    private GameObject inventory;
    private Transform itemsParent;
    InventorySystem inventorySystem;
    InventorySlot[] activeItemSlots;
	InventorySlot[] passiveItemSlots;
    ActiveItemPopup[] activeItemPopups = new ActiveItemPopup[2];

    // Use this for initialization
	void Start () 
    {
        inventory = transform.Find("Inventory").gameObject;
        inventorySystem = InventorySystem.instance;
        inventorySystem.onItemChangedCallback += UpdateUI;
        itemsParent = gameObject.transform.Find("Inventory").transform.Find("ItemsParent");
        activeItemSlots = itemsParent.transform.Find("ActiveItemSlots").GetComponentsInChildren<InventorySlot>();
		passiveItemSlots = itemsParent.transform.Find("PassiveItemSlots").GetComponentsInChildren<InventorySlot>();
		inventory.SetActive(false);
        activeItemPopups[0] = GameObject.Find("Canvas").transform.Find("HealthBar").transform.Find("Base").transform.Find("ActiveItem1").GetComponent<ActiveItemPopup>();
        activeItemPopups[1] = GameObject.Find("Canvas").transform.Find("HealthBar").transform.Find("Base").transform.Find("ActiveItem2").GetComponent<ActiveItemPopup>();
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
        for (int i = 0; i < activeItemSlots.Length; i++)
        {
            if (i < inventorySystem.activeItems.Count)
            {
                activeItemSlots[i].AddItem(inventorySystem.activeItems[i]);
                activeItemPopups[i].AddItem(inventorySystem.activeItems[i]);
            }
            else
            {
                activeItemSlots[i].ClearSlot();
                activeItemPopups[i].RemoveItem();
            }
        }

		for (int i = 0; i < passiveItemSlots.Length; i++)
		{
			if (i < inventorySystem.passiveItems.Count)
			{
				passiveItemSlots[i].AddItem(inventorySystem.passiveItems[i]);
			}
			else
			{
				passiveItemSlots[i].ClearSlot();
			}
		}
    }
}
