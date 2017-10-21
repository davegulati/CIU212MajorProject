using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemPopup : MonoBehaviour {

    private InventorySlot[] activeInventorySlots = new InventorySlot[2];

    private Item slotItem = null;
	private Image activeItemPopup_Icon;
	private Image activeItemPopup_Radial;
	private Color useColor = new Color(0, 255, 0, 0.5f);
	private Color cooldownColor = new Color(0, 0, 255, 0.5f);

	// Use this for initialization
	void Start () 
    {
		activeItemPopup_Icon = gameObject.transform.Find("ActiveItemPopup").transform.Find("Icon").GetComponent<Image>();
		activeItemPopup_Radial = gameObject.transform.Find("ActiveItemPopup").transform.Find("Radial").GetComponent<Image>();
        gameObject.transform.Find("ActiveItemPopup").gameObject.SetActive(false);
        activeInventorySlots[0] = GameObject.Find("Canvas").transform.Find("InventoryManager").transform.Find("Inventory").transform.Find("ItemsParent").transform.Find("ActiveItemSlots").transform.Find("InventorySlot1").GetComponent<InventorySlot>();
		activeInventorySlots[1] = GameObject.Find("Canvas").transform.Find("InventoryManager").transform.Find("Inventory").transform.Find("ItemsParent").transform.Find("ActiveItemSlots").transform.Find("InventorySlot2").GetComponent<InventorySlot>();
	}

    public bool InitiateActiveAbility (Item item)
    {
        if (slotItem != null)
        {
            return false;
        }
        gameObject.transform.Find("ActiveItemPopup").gameObject.SetActive(true);
        StartCoroutine(StartActiveAbilityUseTimer(item));
        slotItem = item;
        return true;
    }

	IEnumerator StartActiveAbilityUseTimer(Item item)
	{
		float useTime = 0;
		activeItemPopup_Icon.gameObject.transform.parent.gameObject.SetActive(true);
		activeItemPopup_Icon.sprite = item.itemSprite;
		activeItemPopup_Radial.sprite = item.itemSprite;
		activeItemPopup_Radial.fillAmount = 0;
		activeItemPopup_Radial.color = useColor;
		while (useTime < item.useTime)
		{
			useTime = useTime + 1;
			activeItemPopup_Radial.fillAmount = useTime / item.useTime;
			yield return new WaitForSeconds(1);
		}

		StartCoroutine(StartActiveAbilityCooldownTimer(item));
	}

	IEnumerator StartActiveAbilityCooldownTimer(Item item)
	{
		float cooldownTime = item.cooldownTime;
		activeItemPopup_Radial.fillAmount = 1;
		activeItemPopup_Radial.color = cooldownColor;
		while (cooldownTime > 0)
		{
			cooldownTime = cooldownTime - 1;
			// Update UI
			activeItemPopup_Radial.fillAmount = cooldownTime / item.cooldownTime;
			yield return new WaitForSeconds(1);
		}

        slotItem = null;
        item.beingUsed = false;
        foreach (InventorySlot activeInventorySlot in activeInventorySlots)
        {
            activeInventorySlot.UpdateSlot();
        }
        gameObject.transform.Find("ActiveItemPopup").gameObject.SetActive(false);
	}
}
