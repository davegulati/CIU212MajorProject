using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    private Animator anim;
    private float baseMaxPlayerHealth = 100.0f;
    private float currentPlayerHealth = 100.0f;
    [HideInInspector]
    public float maxPlayerHealth = 100.0f;
    private Image healthFill;
    private SpriteRenderer spriteRenderer;
    private bool isHurting = false;
    private int hurtTime = 2;
    private float flashDelay = 0.1f;
    private Color isHurtingColor = new Color(255, 0, 0);
    private Color normalColor = new Color(255, 255, 255);

	void Awake () 
    {
        anim = gameObject.GetComponent<Animator>();
        healthFill = GameObject.Find("Canvas").transform.Find("HealthBar").transform.Find("Base").transform.Find("HealthFill").GetComponent<Image>();
        healthFill.fillAmount = currentPlayerHealth / maxPlayerHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void PlayerTakeDamage(float damage)
    {
        currentPlayerHealth = currentPlayerHealth - damage;
        healthFill.fillAmount = currentPlayerHealth / maxPlayerHealth;
        if (currentPlayerHealth <= 0)
        {
            SceneManager.LoadScene("SafeZoneGreybox");
            Destroy(gameObject);
        }
        else
        {
            anim.SetTrigger("TakeDamage");
            StartCoroutine(TimerDamageColor());
        }
    }

    private IEnumerator TimerDamageColor ()
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

    public void PlayerReceiveHealth (float health)
    {
        currentPlayerHealth = currentPlayerHealth + health;
        healthFill.fillAmount = currentPlayerHealth / maxPlayerHealth;
        if (currentPlayerHealth > maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }
        anim.SetTrigger("Heal");
    }

	public void PlayerSetHealth(float health)
	{
		currentPlayerHealth = health;
		healthFill.fillAmount = currentPlayerHealth / maxPlayerHealth;
		if (currentPlayerHealth > maxPlayerHealth)
		{
			currentPlayerHealth = maxPlayerHealth;
		}
	}

    public void IncreaseMaxHealth (float amountToIncreaseBy)
    {
        maxPlayerHealth = maxPlayerHealth + amountToIncreaseBy;
		if (currentPlayerHealth > maxPlayerHealth)
		{
			currentPlayerHealth = maxPlayerHealth;
		}
        healthFill.fillAmount = currentPlayerHealth / maxPlayerHealth;
    }

	public void ResetMaxHealth()
	{
		maxPlayerHealth = baseMaxPlayerHealth;
		if (currentPlayerHealth > maxPlayerHealth)
		{
			currentPlayerHealth = maxPlayerHealth;
		}
        healthFill.fillAmount = currentPlayerHealth / maxPlayerHealth;
	}
}
