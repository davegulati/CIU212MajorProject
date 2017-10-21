using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    private float baseMaxPlayerHealth = 100.0f;
    private float currentPlayerHealth = 100.0f;
    [HideInInspector]
    public float maxPlayerHealth = 100.0f;
    private Slider slider_Health;
    private SpriteRenderer spriteRenderer;
    private bool isHurting = false;
    private int hurtTime = 2;
    private float flashDelay = 0.1f;
    private Color isHurtingColor = new Color(255, 0, 0);
    private Color normalColor = new Color(255, 255, 255);

	void Awake () 
    {
        slider_Health = GameObject.Find("Slider_Health").GetComponent<Slider>();
        slider_Health.value = currentPlayerHealth / 100;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void PlayerTakeDamage(float damage)
    {
        currentPlayerHealth = currentPlayerHealth - damage;
        slider_Health.value = currentPlayerHealth / 100;
        if (currentPlayerHealth <= 0)
        {
            SceneManager.LoadScene("SafeZoneGreybox");
            Destroy(gameObject);
        }
        else
        {
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
        slider_Health.value = currentPlayerHealth / maxPlayerHealth;
        if (currentPlayerHealth > maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }
    }

	public void PlayerSetHealth(float health)
	{
		currentPlayerHealth = health;
		slider_Health.value = currentPlayerHealth / maxPlayerHealth;
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
        slider_Health.value = currentPlayerHealth / maxPlayerHealth;
    }

	public void ResetMaxHealth()
	{
		maxPlayerHealth = baseMaxPlayerHealth;
		if (currentPlayerHealth > maxPlayerHealth)
		{
			currentPlayerHealth = maxPlayerHealth;
		}
        slider_Health.value = currentPlayerHealth / maxPlayerHealth;
	}
}
