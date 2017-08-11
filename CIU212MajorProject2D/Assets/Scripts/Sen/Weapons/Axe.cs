using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {

    // Damage amounts
    [HideInInspector]
    public float default_DamageAmount = 40;
    [HideInInspector]
    public float current_DamageAmount = 40;

	// Colors
	private Color normalColor = new Color(255, 255, 255);
	private Color enhancedColor = new Color(0, 255, 0);

    // Ignore collisions
    private GameObject[] hazards;
	private GameObject[] platforms;
    private GameObject[] dropPlatforms;

    private void Awake()
    {
        hazards = GameObject.FindGameObjectsWithTag("Hazard");
        foreach (GameObject hazard in hazards)
        {
            Physics2D.IgnoreCollision(hazard.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }

        platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
		{
			Physics2D.IgnoreCollision(platform.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}

		dropPlatforms = GameObject.FindGameObjectsWithTag("DropPlatform");
        foreach (GameObject dropPlatform in dropPlatforms)
		{
			Physics2D.IgnoreCollision(dropPlatform.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GroundEnemy")
        {
			if (collision.gameObject.GetComponent<GroundEnemyHealth>() != null)
			{
				collision.gameObject.GetComponent<GroundEnemyHealth>().DamageEnemy(current_DamageAmount);
			}
        }
    }
}
