﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyHealth : MonoBehaviour {

    private int currentHealth = 100;
    private bool isHurting = false;
	private SpriteRenderer spriteRenderer;
	private int hurtTime = 2;
    private float flashDelay = 0.1f;
	private Color isHurtingColor = new Color(255, 0, 0);
	private Color normalColor = new Color(255, 255, 255);

	private void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void DamageEnemy (int damage)
    {
        currentHealth = currentHealth - damage;
        // Update enemy health slider.
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(TimerDamageColor());
        }
    }

	private IEnumerator TimerDamageColor()
	{
		isHurting = true;
		StartCoroutine(DamageColor());
		yield return new WaitForSeconds(hurtTime);
		isHurting = false;
	}
	private IEnumerator DamageColor()
	{
		isHurting = true;
		while (isHurting)
		{
			spriteRenderer.color = isHurtingColor;
			yield return new WaitForSeconds(flashDelay);
			spriteRenderer.color = normalColor;
			yield return new WaitForSeconds(flashDelay);
		}
	}
}