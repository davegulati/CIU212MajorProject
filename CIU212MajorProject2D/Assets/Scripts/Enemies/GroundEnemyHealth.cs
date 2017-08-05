using System.Collections;
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

	private CoinLoot coinLoot;

	private void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
		coinLoot = GameObject.Find("LootManager").GetComponent<CoinLoot>();
	}

    public void DamageEnemy (int damage)
    {
        currentHealth = currentHealth - damage;
        // Update enemy health slider.
        if (currentHealth <= 0)
        {
			//coinLoot.calculateLoot();
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
		StartCoroutine(FlashDamageColor());
		yield return new WaitForSeconds(hurtTime);
		isHurting = false;
	}
	private IEnumerator FlashDamageColor()
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