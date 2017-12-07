using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    // Animation variables
    private Animator anim;

    [HideInInspector]
    public float fireRate = 1;
    [SerializeField]
    private LayerMask whatToHit;

    private float timeToFire = 0;
    private Transform firePoint;

    // Durability
    private float maxDurability = 100.0f;
    private float currentDurability;
    private float minDurability = 5.0f;
    private float deductDurabilityAmount = 3.0f;

    // Normal arrow
    [SerializeField]
    private Transform normalArrow;

    // Explosive arrow
    [HideInInspector]
    public bool explosiveArrowsUnlocked = false;
	[SerializeField]
	private Transform explosiveArrow;
    private int explosionChance;
	private int explosionChanceMin = 1;
    private int explosionChanceMax = 10;

    // Steel Shot arrow
    [HideInInspector]
    public bool steelShotArrowsUnlocked = false;
    [SerializeField]
    private Transform steelShotArrow;

    [HideInInspector]
    public float base_DamageAmount = 30.0f;
    [HideInInspector]
    public float current_DamageAmount = 30.0f;

	// Colors
	private Color normalColor = new Color(255, 255, 255);
	private Color enhancedColor = new Color(0, 255, 0);

	private void Awake () 
    {
        anim = gameObject.transform.parent.GetComponent<Animator>();
        firePoint = gameObject.transform.Find("FirePoint");
        currentDurability = maxDurability;
	}

 //   void Update () 
 //   {
 //       if (Input.GetMouseButtonDown(0))
 //       {
 //           if (fireRate == 0)
 //           {
 //               anim.SetTrigger("BowAttack");
 //           }
 //       }
 //       else
 //       {
 //           if (Input.GetMouseButton(0) && Time.time > timeToFire)
 //           {
	//			timeToFire = Time.time + 1 / fireRate;
 //               anim.SetTrigger("BowAttack");
 //           }
 //       }
	//}

    public void ShootArrow ()
    {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        if (explosiveArrowsUnlocked) // Explosive arrows
        {
            explosionChance = Random.Range(explosionChanceMin, explosionChanceMax);
			if (explosionChance == 6)
			{
				Transform spawmedArrow = Instantiate(explosiveArrow, firePoint.position, firePoint.rotation);
				spawmedArrow.GetComponent<ExplosiveArrow>().current_DamageAmount = current_DamageAmount;
                DeductDurability(deductDurabilityAmount);
			}
            else
            {
				Transform spawmedArrow = Instantiate(normalArrow, firePoint.position, firePoint.rotation);
				spawmedArrow.GetComponent<Arrow>().current_DamageAmount = current_DamageAmount;
                DeductDurability(deductDurabilityAmount);
            }
        }
        else  // Normal arrows
        {
			Transform spawmedArrow = Instantiate(normalArrow, firePoint.position, firePoint.rotation);
			spawmedArrow.GetComponent<Arrow>().current_DamageAmount = current_DamageAmount;
            DeductDurability(deductDurabilityAmount);
        }
    }

    public void EnhanceWeaponStats_VitaminC_Pill (float multiplier)
    {
		GetComponent<SpriteRenderer>().color = enhancedColor;
		current_DamageAmount = base_DamageAmount * multiplier;
    }

	public void EnhanceWeaponStats_PowerAura (float multiplier)
	{
		current_DamageAmount *= multiplier;
	}

	public void ResetWeaponStats()
	{
        GetComponent<SpriteRenderer>().color = normalColor;
        current_DamageAmount = base_DamageAmount;
	}

    private void DeductDurability(float amount)
    {
        currentDurability = currentDurability - amount;
        // Update weapon durability UI
        current_DamageAmount *= currentDurability / 100;
        if (currentDurability > maxDurability)
        {
            currentDurability = maxDurability;
        }

        if (currentDurability < minDurability)
        {
            currentDurability = minDurability;
        }
    }

    public void RepairWeapon()
    {
        currentDurability = maxDurability;
        current_DamageAmount = base_DamageAmount;
        // Play repair sound
        // Update weapon durability UI
        if (currentDurability > maxDurability)
        {
            currentDurability = maxDurability;
        }
    }
}
