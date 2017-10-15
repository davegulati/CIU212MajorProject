using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Item UI Variables
    private Image activeItemPopup_Icon;
    private Image activeItemPopup_Radial;

    // Powerup_Stopwatch (Active)
    private float powerup_Stopwatch_NormalTimeScale = 1.0f;
    private float powerup_Stopwatch_SlowMotionTimeScale = 0.4f;
    private float powerup_Stopwatch_UseTime = 3.0f;
    private float powerup_Stopwatch_CooldownTime = 12.5f;
    private bool using_Powerup_Stopwatch = false;

	// Powerup_Potion (Active)
    private float powerup_Potion_HealthAwarded;
    private float powerup_Potion_HealthDivider = 2.0f;

    // Powerup_VitaminC_Pill (Active)
    private float powerup_VitaminC_Pill_DamageMultiplier = 1.2f;
    private int powerup_VitaminC_Pill_UseTime = 5;
    private int powerup_VitaminC_Pill_CooldownTime = 8;
    private bool powerup_VitaminC_Pill_ReadyForUse = true;

	// Powerup_Health (Consumable)
	private float powerup_Health_HealthAwarded = 20.0f;

    // Powerup_MechanicalBoots (Passive)
    private float powerup_MechanicalBoots_SpeedBoost = 10.0f;

    // Powerup_PowerAura (Passive)
    private float powerup_PowerAura_DamageMultiplier = 1.15f;

    // Powerup_HSP (Passive)
    private float powerup_HSP_HealthToIncreaseBy = 30.0f;

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

        activeItemPopup_Icon = GameObject.Find("Canvas").transform.Find("ActiveItemPopup1").transform.Find("Icon").GetComponent<Image>();
        activeItemPopup_Radial = GameObject.Find("Canvas").transform.Find("ActiveItemPopup1").transform.Find("Radial").GetComponent<Image>();
        activeItemPopup_Icon.gameObject.transform.parent.gameObject.SetActive(false);
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

    public void Use (Item item)
    {
        if (item.itemID == 0)
        {
            UsePowerup_Potion(item);
        }

		if (item.itemID == 1)
		{
            StartCoroutine(UsePowerup_Stopwatch(item));
		}

		if (item.itemID == 2)
		{
			// LIGHTING ROD
		}

		if (item.itemID == 3)
		{
            UsePowerup_VitaminC_Pill(item);
		}

		if (item.itemID == 4)
		{
            UsePowerup_Health(item);
		}

		if (item.itemID == 5)
		{
            UsePowerup_MaxHealth(item);
		}

		if (item.itemID == 6)
		{
            UsePowerup_ArrowPowder(item);
		}

		if (item.itemID == 7)
		{
            UsePowerup_MechanicalBoots(item);
		}

		if (item.itemID == 8)
		{
            UsePowerup_PowerAura(item);
		}

        if (item.itemID == 9)
        {
            UsePowerup_HSP(item);
        }

		if (item.itemID == 10)
		{
            UsePowerup_SteelShot(item);
		}
    }

	public void Drop (Item item)
	{
		if (item.itemID == 0)
		{
			DropPowerup_Potion();
		}

		if (item.itemID == 1)
		{
			DropPowerup_Stopwatch();
		}

		if (item.itemID == 2)
		{
			// DROP LIGHTING ROD
		}

		if (item.itemID == 3)
		{
			DropPowerup_VitaminC_Pill();
		}

		if (item.itemID == 4)
		{
			DropPowerup_Health();
		}

		if (item.itemID == 5)
		{
			DropPowerup_MaxHealth();
		}

		if (item.itemID == 6)
		{
			DropPowerup_ArrowPowder();
		}

		if (item.itemID == 7)
		{
			DropPowerup_MechanicalBoots();
		}

		if (item.itemID == 8)
		{
			DropPowerup_PowerAura();
		}

		if (item.itemID == 9)
		{
			DropPowerup_HSP();
		}

		if (item.itemID == 10)
		{
			DropPowerup_SteelShot();
		}
	}

	private void UsePowerup_Potion(Item item)
	{
		powerup_Potion_HealthAwarded = sen.GetComponent<PlayerHealth>().maxPlayerHealth / powerup_Potion_HealthDivider;
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(powerup_Potion_HealthAwarded);
	}

    private void DropPowerup_Potion ()
    {
        
    }

	IEnumerator UsePowerup_Stopwatch(Item item)
	{
        if (!item.beingUsed)
        {
            item.beingUsed = true;
            StartCoroutine(StartActiveAbilityUseTimer(item));
			//Time.timeScale = powerup_Stopwatch_SlowMotionTimeScale;
            yield return new WaitForSeconds(item.useTime);
			Time.timeScale = powerup_Stopwatch_NormalTimeScale;
        }
	}

    IEnumerator Powerup_Stopwatch_Cooldown ()
    {
        yield return new WaitForSeconds(powerup_Stopwatch_CooldownTime);
        using_Powerup_Stopwatch = false;
    }

    private void DropPowerup_Stopwatch ()
    {
        Time.timeScale = powerup_Stopwatch_NormalTimeScale;
    }

	private void UsePowerup_VitaminC_Pill(Item item)
	{
        if (powerup_VitaminC_Pill_ReadyForUse)
        {
			powerup_VitaminC_Pill_ReadyForUse = false;
			sen.transform.Find("Axe").GetComponent<Axe>().EnhanceWeaponStats_VitaminC_Pill(powerup_VitaminC_Pill_DamageMultiplier);
			sen.transform.Find("Bow").GetComponent<Bow>().EnhanceWeaponStats_VitaminC_Pill(powerup_VitaminC_Pill_DamageMultiplier);
			StartCoroutine(Powerup_VitaminC_Pill_ResetWeaponStats());
        }
	}

	IEnumerator Powerup_VitaminC_Pill_ResetWeaponStats()
	{
		yield return new WaitForSeconds(powerup_VitaminC_Pill_UseTime);
		sen.transform.Find("Axe").GetComponent<Axe>().ResetWeaponStats();
		sen.transform.Find("Bow").GetComponent<Bow>().ResetWeaponStats();
		StartCoroutine(Powerup_VitaminC_Pill_Cooldown());
	}

	IEnumerator Powerup_VitaminC_Pill_Cooldown()
	{
		yield return new WaitForSeconds(powerup_VitaminC_Pill_CooldownTime);
		powerup_VitaminC_Pill_ReadyForUse = true;
	}

    private void DropPowerup_VitaminC_Pill ()
    {
        
    }

    private void UsePowerup_Health (Item item)
    {
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(powerup_Health_HealthAwarded);
	}

    private void DropPowerup_Health ()
    {
        
    }

    private void UsePowerup_MaxHealth (Item item)
    {
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(sen.GetComponent<PlayerHealth>().maxPlayerHealth);
	}

    private void DropPowerup_MaxHealth ()
    {
        
    }

	private void UsePowerup_ArrowPowder (Item item)
	{
		sen.transform.Find("Bow").GetComponent<Bow>().explosiveArrowsUnlocked = true;
	}

	private void DropPowerup_ArrowPowder()
	{

	}

    private void UsePowerup_MechanicalBoots (Item item)
    {
        sen.GetComponent<PlayerCharacterController>().movementSpeed = powerup_MechanicalBoots_SpeedBoost;
    }

    private void DropPowerup_MechanicalBoots ()
    {
        
    }

    private void UsePowerup_PowerAura (Item item)
    {
        sen.transform.Find("Bow").GetComponent<Bow>().EnhanceWeaponStats_PowerAura(powerup_PowerAura_DamageMultiplier);
        sen.transform.Find("Axe").GetComponent<Axe>().EnhanceWeaponStats_PowerAura(powerup_PowerAura_DamageMultiplier);
    }

    private void DropPowerup_PowerAura ()
    {
        
    }

    private void UsePowerup_HSP (Item item)
    {
        sen.GetComponent<PlayerHealth>().IncreaseMaxHealth(powerup_HSP_HealthToIncreaseBy);
    }

    private void DropPowerup_HSP ()
    {
        
    }

    private void UsePowerup_SteelShot (Item item)
    {
		sen.transform.Find("Bow").GetComponent<Bow>().steelShotArrowsUnlocked = true;
	}

    private void DropPowerup_SteelShot ()
    {
        
    }

    IEnumerator StartActiveAbilityUseTimer (Item item)
    {
        float useTime = 0;
		activeItemPopup_Icon.gameObject.transform.parent.gameObject.SetActive(true);
		activeItemPopup_Icon.sprite = item.itemSprite;
        activeItemPopup_Radial.sprite = item.itemSprite;
        activeItemPopup_Radial.fillAmount = 0;
        activeItemPopup_Radial.color = Color.green;
        while (useTime < item.useTime)
        {
            useTime = useTime + 1;
            activeItemPopup_Radial.fillAmount = useTime / item.useTime;
            yield return new WaitForSeconds(1);
        }

        StartCoroutine(StartActiveAbilityCooldownTimer(item));
    }

    IEnumerator StartActiveAbilityCooldownTimer (Item item)
    {
		float cooldownTime = 0;
		activeItemPopup_Radial.fillAmount = 1;
        activeItemPopup_Radial.color = Color.blue;
		while (cooldownTime < item.cooldownTime)
		{
			cooldownTime = cooldownTime + 1;
            // Update UI
            activeItemPopup_Radial.fillAmount = cooldownTime / item.cooldownTime;
			yield return new WaitForSeconds(1);
		}

        item.beingUsed = false;
    }
}
