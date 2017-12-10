using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    // Damage amounts
    [HideInInspector]
    public float base_DamageAmount = 40;
    [HideInInspector]
    public float current_DamageAmount = 40;

    // Durability
    private float maxDurability = 100.0f;
    private float currentDurability;
    private float minDurability = 5.0f;
    private float groundEnemyDurability = 1.0f;
    private float rangedEnemyDurability = 1.0f;

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

        currentDurability = maxDurability;
    }

    public void EnhanceWeaponStats_VitaminC_Pill(float multiplier)
    {
        GetComponent<SpriteRenderer>().color = enhancedColor;
        current_DamageAmount = base_DamageAmount * multiplier;
    }

    public void EnhanceWeaponStats_PowerAura(float multiplier)
    {
        current_DamageAmount *= multiplier;
    }

    public void ResetWeaponStats()
    {
        GetComponent<SpriteRenderer>().color = normalColor;
        current_DamageAmount = base_DamageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GroundEnemy")
        {
            if (collision.gameObject.GetComponent<GroundEnemyHealth>() != null)
            {
                collision.gameObject.GetComponent<GroundEnemyHealth>().DamageEnemy(current_DamageAmount);
                DeductDurability(groundEnemyDurability);
            }
        }

        if (collision.gameObject.tag == "RangedEnemy")
        {
            if (collision.gameObject.GetComponent<RangedEnemyHealth>() != null)
            {
                collision.gameObject.GetComponent<RangedEnemyHealth>().DamageEnemy(current_DamageAmount);
                DeductDurability(rangedEnemyDurability);
            }
        }

        if (collision.gameObject.tag == "Boss")
        {
            if (collision.gameObject.GetComponent<bossAttack>() != null)
            {
                collision.gameObject.GetComponent<bossAttack>().DamageEnemy(current_DamageAmount);
                DeductDurability(rangedEnemyDurability);
            }
        }
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
