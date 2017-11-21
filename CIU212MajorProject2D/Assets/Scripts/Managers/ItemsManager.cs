using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour {

	public static ItemsManager instance;

    protected GameObject sen;

    [HideInInspector]
    public int playerCurrency_Gold = 0;
    [HideInInspector]
    public int playerCurrency_Dye = 0;

    // LIVE SHOP VARIABLES:
	public List<Item> allItemsList;
	private GameObject[] itemSlots = new GameObject[4];
	private Item generatedItem;
	private int index;

    // ITEM VARIABLES:

    // Item UI Variables
    private ActiveItemPopup activeItemPopup1;
    private ActiveItemPopup activeItemPopup2;

    // Powerup_Stopwatch (Active)
    private float powerup_Stopwatch_NormalTimeScale = 1.0f;
    private float powerup_Stopwatch_SlowMotionTimeScale = 0.4f;

    // Powerup_LightningRod (Active)
    private GameObject[] groundEnemies;
    private GameObject[] rangedEnemies;
    private GameObject closestEnemy;
    private GameObject closestGE;
    private GameObject closestRE;
    private float stunRange = 6.0f;

	// Powerup_Potion (Active)
    private float powerup_Potion_HealthAwarded;
    private float powerup_Potion_HealthDivider = 2.0f;

    // Powerup_VitaminC_Pill (Active)
    private float powerup_VitaminC_Pill_DamageMultiplier = 1.2f;

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

        activeItemPopup1 = GameObject.Find("Canvas").transform.Find("ActiveItemPopup1_Parent").GetComponent<ActiveItemPopup>();
		activeItemPopup2 = GameObject.Find("Canvas").transform.Find("ActiveItemPopup2_Parent").GetComponent<ActiveItemPopup>();
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
			UsePowerup_LightningRod(item);
		}

		if (item.itemID == 3)
		{
            StartCoroutine(UsePowerup_VitaminC_Pill(item));
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
			DropPowerup_Potion(item);
		}

		if (item.itemID == 1)
		{
			DropPowerup_Stopwatch(item);
		}

		if (item.itemID == 2)
		{
			DropPowerup_LightningRod(item);
		}

		if (item.itemID == 3)
		{
			DropPowerup_VitaminC_Pill(item);
		}

		if (item.itemID == 4)
		{
			DropPowerup_Health(item);
		}

		if (item.itemID == 5)
		{
			DropPowerup_MaxHealth(item);
		}

		if (item.itemID == 6)
		{
			DropPowerup_ArrowPowder(item);
		}

		if (item.itemID == 7)
		{
			DropPowerup_MechanicalBoots(item);
		}

		if (item.itemID == 8)
		{
			DropPowerup_PowerAura(item);
		}

		if (item.itemID == 9)
		{
			DropPowerup_HSP(item);
		}

		if (item.itemID == 10)
		{
			DropPowerup_SteelShot(item);
		}
	}

	private void UsePowerup_Potion(Item item)
	{
        item.beingUsed = true;
		powerup_Potion_HealthAwarded = sen.GetComponent<PlayerHealth>().maxPlayerHealth / powerup_Potion_HealthDivider;
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(powerup_Potion_HealthAwarded);
	}

    private void DropPowerup_Potion (Item item)
    {
        item.beingUsed = false;
        Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
	}

	IEnumerator UsePowerup_Stopwatch(Item item)
	{
        if (!item.beingUsed)
        {
            item.beingUsed = true;
            ActivateActiveAbility(item);
			Time.timeScale = powerup_Stopwatch_SlowMotionTimeScale;
            yield return new WaitForSeconds(item.useTime);
			Time.timeScale = powerup_Stopwatch_NormalTimeScale;
        }
	}

    private void DropPowerup_Stopwatch (Item item)
    {
        Time.timeScale = powerup_Stopwatch_NormalTimeScale;
        item.beingUsed = false;
		Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

    private void UsePowerup_LightningRod (Item item)
    {
        if (!item.beingUsed)
        {
            FindAndStunClosestEnemy(item);
        }
    }

    private void FindAndStunClosestEnemy(Item item)
    {
        closestEnemy = null;
        FindClosestGroundEnemy();
        FindClosestRangedEnemy();
        if (closestGE != null && closestRE != null)
        {
            float distanceGE = Vector2.Distance(sen.transform.position, closestGE.transform.position);
            float distanceRE = Vector2.Distance(sen.transform.position, closestRE.transform.position);
            if (distanceGE < distanceRE)
            {
                closestEnemy = closestGE;
                if (distanceGE <= stunRange)
                {
                    closestEnemy.GetComponent<GroundEnemy>().Stun();
                    ActivateActiveAbility(item);
                    item.beingUsed = true;
                }
            }
            else if (distanceRE < distanceGE)
            {
                closestEnemy = closestRE;
                if (distanceRE <= stunRange)
                {
                    closestEnemy.GetComponent<RangedEnemy>().Stun();
                    ActivateActiveAbility(item);
                    item.beingUsed = true;
                }
            }
        }
        else if (closestGE == null && closestRE != null) // If only RE can be found.
        {
            float distanceRE = Vector2.Distance(sen.transform.position, closestRE.transform.position);
            closestEnemy = closestRE;
            if (distanceRE <= stunRange)
            {
                closestEnemy.GetComponent<RangedEnemy>().Stun();
                ActivateActiveAbility(item);
                item.beingUsed = true;
            }
        }

        else if (closestRE == null && closestGE != null) // If only GE can be found.
        {
            float distanceGE = Vector2.Distance(sen.transform.position, closestGE.transform.position);
            closestEnemy = closestGE;
            if (distanceGE <= stunRange)
            {
                closestEnemy.GetComponent<GroundEnemy>().Stun();
                ActivateActiveAbility(item);
                item.beingUsed = true;
            }
        }

        else if (closestGE == null & closestRE == null) // If neither can be found.
        {
            Debug.Log("Could not stun any enemies because no enemies were found!");
        }
    }

    private GameObject FindClosestGroundEnemy()
    {
        groundEnemies = GameObject.FindGameObjectsWithTag("GroundEnemy");
        float distance = Mathf.Infinity;
        Vector3 position = sen.transform.position;
        foreach (GameObject groundEnemy in groundEnemies)
        {
            Vector3 diff = groundEnemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestGE = groundEnemy;
                distance = curDistance;
            }
        }
        return closestGE;
    }

    private GameObject FindClosestRangedEnemy()
    {
        rangedEnemies = GameObject.FindGameObjectsWithTag("RangedEnemy");
        float distance = Mathf.Infinity;
        Vector3 position = sen.transform.position;
        foreach (GameObject rangedEnemy in rangedEnemies)
        {
            Vector3 diff = rangedEnemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestRE = rangedEnemy;
                distance = curDistance;
            }
        }
        return closestRE;
    }

    private void DropPowerup_LightningRod(Item item)
    {
        item.beingUsed = false;
        Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
        Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

	IEnumerator UsePowerup_VitaminC_Pill(Item item)
	{
        if (!item.beingUsed)
        {
			item.beingUsed = true;
            ActivateActiveAbility(item);
			sen.transform.Find("Axe").GetComponent<Axe>().EnhanceWeaponStats_VitaminC_Pill(powerup_VitaminC_Pill_DamageMultiplier);
			sen.transform.Find("Bow").GetComponent<Bow>().EnhanceWeaponStats_VitaminC_Pill(powerup_VitaminC_Pill_DamageMultiplier);
			yield return new WaitForSeconds(item.useTime);
			sen.transform.Find("Axe").GetComponent<Axe>().ResetWeaponStats();
			sen.transform.Find("Bow").GetComponent<Bow>().ResetWeaponStats();
        }
	}

    private void DropPowerup_VitaminC_Pill (Item item)
    {
		sen.transform.Find("Axe").GetComponent<Axe>().ResetWeaponStats();
		sen.transform.Find("Bow").GetComponent<Bow>().ResetWeaponStats();
        item.beingUsed = false;
		Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

    private void UsePowerup_Health (Item item)
    {
        item.beingUsed = true;
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(powerup_Health_HealthAwarded);
	}

    private void DropPowerup_Health (Item item)
    {
		item.beingUsed = false;
		Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

    private void UsePowerup_MaxHealth (Item item)
    {
        item.beingUsed = true;
		sen.GetComponent<PlayerHealth>().PlayerReceiveHealth(sen.GetComponent<PlayerHealth>().maxPlayerHealth);
	}

    private void DropPowerup_MaxHealth (Item item)
    {
		item.beingUsed = false;
		Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

	private void UsePowerup_ArrowPowder (Item item)
	{
        item.beingUsed = true;
		sen.transform.Find("Bow").GetComponent<Bow>().explosiveArrowsUnlocked = true;
	}

	private void DropPowerup_ArrowPowder(Item item)
	{
        sen.transform.Find("Bow").GetComponent<Bow>().explosiveArrowsUnlocked = false;
		item.beingUsed = false;
		Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
	}

    private void UsePowerup_MechanicalBoots (Item item)
    {
        item.beingUsed = true;
        sen.GetComponent<PlayerCharacterController>().movementSpeed = powerup_MechanicalBoots_SpeedBoost;
    }

    private void DropPowerup_MechanicalBoots (Item item)
    {
		sen.GetComponent<PlayerCharacterController>().movementSpeed = sen.GetComponent<PlayerCharacterController>().baseMovementSpeed;
        item.beingUsed = false;
		Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

    private void UsePowerup_PowerAura (Item item)
    {
        item.beingUsed = true;
        sen.transform.Find("Bow").GetComponent<Bow>().EnhanceWeaponStats_PowerAura(powerup_PowerAura_DamageMultiplier);
        sen.transform.Find("Axe").GetComponent<Axe>().EnhanceWeaponStats_PowerAura(powerup_PowerAura_DamageMultiplier);
    }

    private void DropPowerup_PowerAura (Item item)
    {
		sen.transform.Find("Bow").GetComponent<Bow>().ResetWeaponStats();
		sen.transform.Find("Axe").GetComponent<Axe>().ResetWeaponStats();
        item.beingUsed = false;
		Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

    private void UsePowerup_HSP (Item item)
    {
        if (!item.beingUsed)
        {
			item.beingUsed = true;
			sen.GetComponent<PlayerHealth>().IncreaseMaxHealth(powerup_HSP_HealthToIncreaseBy);
        }
    }

    private void DropPowerup_HSP (Item item)
    {
        sen.GetComponent<PlayerHealth>().ResetMaxHealth();
        item.beingUsed = false;
		Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

    private void UsePowerup_SteelShot (Item item)
    {
		sen.transform.Find("Bow").GetComponent<Bow>().steelShotArrowsUnlocked = true;
	}

    private void DropPowerup_SteelShot (Item item)
    {
        sen.transform.Find("Bow").GetComponent<Bow>().steelShotArrowsUnlocked = false;
        item.beingUsed = false;
		Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
		Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

    private void UsePowerup_PocketSniper(Item item)
    {
        item.beingUsed = true;
        sen.GetComponent<PlayerCharacterController>().pocketSniperUnlocked = true;
    }

    private void DropPowerup_PocketSniper(Item item)
    {
        sen.GetComponent<PlayerCharacterController>().pocketSniperUnlocked = false;
        item.beingUsed = false;
        Instantiate(item.itemPrefab, sen.transform.position, Quaternion.identity);
        Notification.instance.DisplaySmallNotification("Item dropped: " + item.itemName);
    }

    private void ActivateActiveAbility (Item item)
    {
        if (!activeItemPopup1.InitiateActiveAbility(item))
        {
            activeItemPopup2.InitiateActiveAbility(item);
        }
    }
}
