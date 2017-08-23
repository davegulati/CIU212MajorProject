using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaminC_Pill : MonoBehaviour {
	
    private GameObject sen;
    private GameObject itemCanvas;
	private float activationRange = 0.8f;
    private float notificationDuration = 3.0f;

    // Damage multiplier.
    private float damageMultiplier = 1.2f;

    // Use time - How long the effect lasts.
    private int useTime = 5;

    // Pill cooldown time.
    private int cooldownTime = 8;

	// Pill cooldown state.
	private bool readyForUse = true;

	// Colors
	private Color normalColor = new Color(255, 255, 255);
	private Color useColor = new Color(0, 255, 0);
    private Color cooldownColor = new Color(255, 0, 0);

	private void Awake()
	{
		sen = GameObject.Find("Sen");
        itemCanvas = transform.Find("ItemCanvas").gameObject;
        itemCanvas.SetActive(false);
	}

	private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
        if (distance < activationRange)
		{
            itemCanvas.SetActive(true);
            if (Input.GetButtonDown("Interact") & readyForUse)
            {
				Notification.instance.Display("!", "ITEM OBTAINED", "Vitamin C Pill", "Increased damage for certain amount of time.", "Press 'I' to access your inventory.", notificationDuration);
				UsePill();
            }
		}
        else 
        {
            itemCanvas.SetActive(false);
        }
	}

	private void UsePill()
	{
        readyForUse = false;
        GetComponent<SpriteRenderer>().color = useColor;
		sen.transform.Find("Axe").GetComponent<Axe>().EnhanceWeaponStats_VitaminC_Pill(damageMultiplier);
        sen.transform.Find("Bow").GetComponent<Bow>().EnhanceWeaponStats_VitaminC_Pill(damageMultiplier);
		StartCoroutine(ResetWeaponStats());
	}

	IEnumerator ResetWeaponStats()
	{
		yield return new WaitForSeconds(useTime);
		sen.transform.Find("Axe").GetComponent<Axe>().ResetWeaponStats();
		sen.transform.Find("Bow").GetComponent<Bow>().ResetWeaponStats();
        StartCoroutine(PillCooldown());
	}

    IEnumerator PillCooldown ()
    {
        GetComponent<SpriteRenderer>().color = cooldownColor;
        yield return new WaitForSeconds(cooldownTime);
        readyForUse = true;
        GetComponent<SpriteRenderer>().color = normalColor;
    }
}