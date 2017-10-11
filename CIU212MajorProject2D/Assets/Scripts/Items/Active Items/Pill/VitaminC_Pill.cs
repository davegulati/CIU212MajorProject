using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaminC_Pill : InteractableItem {
	
    private GameObject sen;
    private GameObject itemCanvas;

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

    //private void Awake()
    //{
    //    sen = GameObject.Find("Sen");
    //}

    public void Use()
	{
        readyForUse = false;
        GetComponent<SpriteRenderer>().color = useColor;
        sen = GameObject.Find("Sen");
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