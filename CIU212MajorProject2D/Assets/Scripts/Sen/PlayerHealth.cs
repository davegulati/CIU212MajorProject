using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float playerHealth = 100.0f;
    private float maxPlayerHealth = 100.0f;
    private Slider slider_Health;

	// Use this for initialization
	void Start () 
    {
        slider_Health = GameObject.Find("Slider_Health").GetComponent<Slider>();
        slider_Health.value = playerHealth / 100;
	}

    public void PlayerTakeDamage(float damage)
    {
        playerHealth = playerHealth - damage;
        slider_Health.value = playerHealth / 100;
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void PlayerReceiveHealth (float health)
    {
        playerHealth = playerHealth + health;
        slider_Health.value = playerHealth / maxPlayerHealth;
        if (playerHealth > 100.0f)
        {
            playerHealth = 100.0f;
        }
    }
}
