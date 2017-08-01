using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyHealth : MonoBehaviour {

    private int currentHealth = 100;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void DamageEnemy (int damage)
    {
        currentHealth = currentHealth - damage;
        // Update enemy health slider.
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}