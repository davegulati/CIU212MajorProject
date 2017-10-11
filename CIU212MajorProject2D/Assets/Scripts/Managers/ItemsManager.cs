using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour {

	public static ItemsManager instance;

    protected GameObject sen;

    // LIVE SHOP VARIABLES:
	public GameObject[] allItemsArray;
	public List<GameObject> allItemsList;
	private GameObject[] itemSlots = new GameObject[4];
	private GameObject generatedItem;
	private int index;

    // ITEM VARIABLES:

    // Powerup_Stopwatch (Active)
	private float normalTimeScale = 1.0f;
	private float slowMotionTimeScale = 0.4f;
	private float resetAfterSeconds = 3.0f;

	// Powerup_Potion (Active)
    private float powerup_Potion_HealthAwarded;
	private float healthDivider = 2.0f;

    // Powerup_VitaminC_Pill (Active)
    private float damageMultiplier = 1.2f;
    private int useTime = 5;
    private int cooldownTime = 8;
    private bool readyForUse = true;

	// Powerup_Health (Consumable)
	private float powerup_Health_HealthAwarded = 20.0f;



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

        sen = GameObject.Find("Sen");

		itemSlots[0] = GameObject.Find("LiveShop").transform.Find("Canvas_LiveShop").transform.Find("BG").transform.Find("ItemSlotsBG").transform.Find("ItemSlot1").gameObject;
		itemSlots[1] = GameObject.Find("LiveShop").transform.Find("Canvas_LiveShop").transform.Find("BG").transform.Find("ItemSlotsBG").transform.Find("ItemSlot2").gameObject;
		itemSlots[2] = GameObject.Find("LiveShop").transform.Find("Canvas_LiveShop").transform.Find("BG").transform.Find("ItemSlotsBG").transform.Find("ItemSlot3").gameObject;
		itemSlots[3] = GameObject.Find("LiveShop").transform.Find("Canvas_LiveShop").transform.Find("BG").transform.Find("ItemSlotsBG").transform.Find("ItemSlot4").gameObject;
        AddItemsToSlots();
    }

	public void AddItemsToSlots()
	{
		foreach (GameObject itemSlot in itemSlots)
		{
			GenerateNewItem();
			itemSlot.GetComponent<ItemSlot>().AddItem(generatedItem);
		}
	}

	private void GenerateNewItem()
	{
		index = Random.Range(0, allItemsList.Count);
		generatedItem = allItemsList[index];
		allItemsList.RemoveAt(index);
	}

    public void Use(Item item)
    {
        if (item.itemID == 0)
        {
            Powerup_Potion();
        }

		if (item.itemID == 1)
		{
            StartCoroutine(Powerup_Stopwatch());
		}

		if (item.itemID == 2)
		{
			// LIGHTING ROD
		}

		if (item.itemID == 3)
		{
			Powerup_VitaminC_Pill();
		}

		if (item.itemID == 4)
		{
			Powerup_Health();
		}

		if (item.itemID == 5)
		{
			Powerup_MaxHealth();
		}
    }

	IEnumerator Powerup_Stopwatch()
	{
		Time.timeScale = slowMotionTimeScale;
		//GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(resetAfterSeconds);
		Time.timeScale = normalTimeScale;
	}

    private void Powerup_Potion ()
    {
		powerup_Potion_HealthAwarded = sen.GetComponent<PlayerHealth>().maxPlayerHealth / healthDivider;
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(powerup_Potion_HealthAwarded);
    }

	private void Powerup_VitaminC_Pill()
	{
        if (readyForUse)
        {
			readyForUse = false;
			sen.transform.Find("Axe").GetComponent<Axe>().EnhanceWeaponStats_VitaminC_Pill(damageMultiplier);
			sen.transform.Find("Bow").GetComponent<Bow>().EnhanceWeaponStats_VitaminC_Pill(damageMultiplier);
			StartCoroutine(Powerup_VitaminC_Pill_ResetWeaponStats());
        }
	}

	IEnumerator Powerup_VitaminC_Pill_ResetWeaponStats()
	{
		yield return new WaitForSeconds(useTime);
		sen.transform.Find("Axe").GetComponent<Axe>().ResetWeaponStats();
		sen.transform.Find("Bow").GetComponent<Bow>().ResetWeaponStats();
		StartCoroutine(Powerup_VitaminC_Pill_Cooldown());
	}

	IEnumerator Powerup_VitaminC_Pill_Cooldown()
	{
		yield return new WaitForSeconds(cooldownTime);
		readyForUse = true;
	}

    private void Powerup_Health ()
    {
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(powerup_Health_HealthAwarded);
	}

    private void Powerup_MaxHealth ()
    {
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(sen.GetComponent<PlayerHealth>().maxPlayerHealth);
	}
}
