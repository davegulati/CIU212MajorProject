using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    [HideInInspector]
    public float fireRate = 1;
    [SerializeField]
    private LayerMask whatToHit;

    private float timeToFire = 0;
    private Transform firePoint;

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

    [HideInInspector]
    public float default_DamageAmount = 30.0f;
    [HideInInspector]
    public float current_DamageAmount = 30.0f;

	// Colors
	private Color normalColor = new Color(255, 255, 255);
	private Color enhancedColor = new Color(0, 255, 0);

	private void Awake () 
    {
        firePoint = gameObject.transform.Find("FirePoint");
	}
	
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (fireRate == 0)
            {
                ShootArrow();
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time > timeToFire)
            {
				timeToFire = Time.time + 1 / fireRate;
                ShootArrow();
            }
        }
	}

    private void ShootArrow ()
    {
        //Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 200, whatToHit);
        if (explosiveArrowsUnlocked)
        {
            explosionChance = Random.Range(explosionChanceMin, explosionChanceMax);
			if (explosionChance == 6)
			{
				Transform spawmedArrow = Instantiate(explosiveArrow, firePoint.position, firePoint.rotation);
				spawmedArrow.GetComponent<ExplosiveArrow>().current_DamageAmount = current_DamageAmount;
			}
            else
            {
				Transform spawmedArrow = Instantiate(normalArrow, firePoint.position, firePoint.rotation);
				spawmedArrow.GetComponent<Arrow>().current_DamageAmount = current_DamageAmount;
            }
        }
        else 
        {
			Transform spawmedArrow = Instantiate(normalArrow, firePoint.position, firePoint.rotation);
			spawmedArrow.GetComponent<Arrow>().current_DamageAmount = current_DamageAmount;
        }
    }

    public void EnhanceWeaponStats_VitaminC_Pill (float multiplier)
    {
		GetComponent<SpriteRenderer>().color = enhancedColor;
		current_DamageAmount = default_DamageAmount * multiplier;
    }

	public void EnhanceWeaponStats_PowerAura (float multiplier)
	{
        default_DamageAmount *= multiplier;
		current_DamageAmount *= multiplier;
	}

	public void ResetWeaponStats()
	{
        GetComponent<SpriteRenderer>().color = normalColor;
        current_DamageAmount = default_DamageAmount;
	}
}
