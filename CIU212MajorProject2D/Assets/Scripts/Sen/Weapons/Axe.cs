using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {

    // Damage Amounts
    [HideInInspector]
    public int damageAmount_GroundEnemy = 40;

	void Update () 
    {
		if (Input.GetMouseButtonDown(0))
		{
			//Attack
		}		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GroundEnemy")
        {
			if (collision.gameObject.GetComponent<GroundEnemyHealth>() != null)
			{
				collision.gameObject.GetComponent<GroundEnemyHealth>().DamageEnemy(damageAmount_GroundEnemy);
			}
        }
    }
}
