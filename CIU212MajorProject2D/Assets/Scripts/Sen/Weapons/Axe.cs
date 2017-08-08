using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {

    // Damage amounts
    [HideInInspector]
    public int damageAmount_GroundEnemy = 40;

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
