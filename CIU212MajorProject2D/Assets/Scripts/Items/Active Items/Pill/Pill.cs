using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour {
	
    private GameObject sen;
	private float activationRange = 0.8f;

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
	}

	private void Update()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
        if (distance < activationRange && Input.GetButtonDown("Interact") && readyForUse)
		{
			UsePill();
		}
	}

	private void UsePill()
	{
        readyForUse = false;
        GetComponent<SpriteRenderer>().color = useColor;
		sen.transform.Find("Axe").GetComponent<Axe>().EnhanceWeaponStats_Pill(damageMultiplier);
        sen.transform.Find("Bow").GetComponent<Bow>().EnhanceWeaponStats_Pill(damageMultiplier);
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