using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemPopup : MonoBehaviour {

    private InventorySlot[] activeInventorySlots = new InventorySlot[2];

    [HideInInspector]
    public Item slotItem = null;
    private Image activeItem_Icon;
    private Image activeItem_Fill;
	private Color useColor = new Color(0, 255, 0, 1f);
	private Color cooldownColor = new Color(0, 0, 255, 1f);

	// Use this for initialization
	void Start () 
    {
		activeItem_Icon = gameObject.transform.Find("Icon").GetComponent<Image>();
        activeItem_Icon.gameObject.SetActive(false);
		activeItem_Fill = gameObject.transform.Find("Fill").GetComponent<Image>();
        activeItem_Fill.gameObject.SetActive(false);
        //gameObject.transform.Find("ActiveItemPopup").gameObject.SetActive(false);
        activeInventorySlots[0] = GameObject.Find("Canvas").transform.Find("InventoryManager").transform.Find("Inventory").transform.Find("ItemsParent").transform.Find("ActiveItemSlots").transform.Find("InventorySlot1").GetComponent<InventorySlot>();
		activeInventorySlots[1] = GameObject.Find("Canvas").transform.Find("InventoryManager").transform.Find("Inventory").transform.Find("ItemsParent").transform.Find("ActiveItemSlots").transform.Find("InventorySlot2").GetComponent<InventorySlot>();
	}

    public void AddItem(Item item)
    {
        slotItem = item;
        activeItem_Icon.gameObject.SetActive(true);
        activeItem_Icon.sprite = item.itemSprite;
    }

    public bool InitiateActiveAbility (Item item)
    {
        if (item != slotItem)
        {
            return false;
        }
        //gameObject.transform.Find("Fill").gameObject.SetActive(true);
        StartCoroutine(StartActiveAbilityUseTimer(item));
        slotItem = item;
        return true;
    }

	IEnumerator StartActiveAbilityUseTimer(Item item)
	{
		float useTime = 0;
		//activeItem_Icon.gameObject.transform.parent.gameObject.SetActive(true);
        activeItem_Icon.gameObject.SetActive(true);
		activeItem_Icon.sprite = item.itemSprite;
        activeItem_Fill.gameObject.SetActive(true);
		//activeItem_Fill.sprite = item.itemSprite;
		activeItem_Fill.fillAmount = 0;
		activeItem_Fill.color = useColor;
		while (useTime < item.useTime)
		{
			useTime = useTime + 1;
			activeItem_Fill.fillAmount = useTime / item.useTime;
			yield return new WaitForSeconds(1);
		}

		StartCoroutine(StartActiveAbilityCooldownTimer(item));
	}

	IEnumerator StartActiveAbilityCooldownTimer(Item item)
	{
		float cooldownTime = item.cooldownTime;
		activeItem_Fill.fillAmount = 1;
		activeItem_Fill.color = cooldownColor;
		while (cooldownTime > 0)
		{
			cooldownTime = cooldownTime - 1;
			// Update UI
			activeItem_Fill.fillAmount = cooldownTime / item.cooldownTime;
			yield return new WaitForSeconds(1);
		}

        //slotItem = null;
        item.beingUsed = false;
        foreach (InventorySlot activeInventorySlot in activeInventorySlots)
        {
            activeInventorySlot.UpdateSlot();
        }
        //gameObject.transform.Find("ActiveItemPopup").gameObject.SetActive(false);
        gameObject.transform.Find("Fill").gameObject.SetActive(false);
	}

    public void RemoveItem()
    {
        slotItem = null;
        activeItem_Icon.sprite = null;
        activeItem_Icon.gameObject.SetActive(false);
        //activeItem_Fill.sprite = null;
        activeItem_Fill.gameObject.SetActive(false);
    }
}
