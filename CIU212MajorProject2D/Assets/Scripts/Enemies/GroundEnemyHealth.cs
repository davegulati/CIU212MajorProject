using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundEnemyHealth : MonoBehaviour {
    
    private Animator anim;
    public float maxHealth;
    private float currentHealth = 100;
    private bool isHurting = false;
	private SpriteRenderer spriteRenderer;
	private int hurtTime = 2;
    private float flashDelay = 0.1f;
	private Color isHurtingColor = new Color(255, 0, 0);
	private Color normalColor = new Color(255, 255, 255);
    private Slider healthSlider;

	private void Awake () 
    {
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		healthSlider = transform.Find("HealthBarCanvas").transform.Find("HealthBarSlider").GetComponent<Slider>();
        healthSlider.value = currentHealth / maxHealth;
	}

    public void DamageEnemy (float damage)
    {
        currentHealth = currentHealth - damage;
        healthSlider.value = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            anim.SetTrigger("EnemyDeath");
        }
        else
        {
            anim.SetTrigger("EnemyTakeDamage");
            StartCoroutine(TimerDamageColor());
        }
    }

    public void DestroyEnemy ()
    {
        ItemsManager.instance.InstantiateGoldCoin(gameObject.transform.position);
        Destroy(gameObject);
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