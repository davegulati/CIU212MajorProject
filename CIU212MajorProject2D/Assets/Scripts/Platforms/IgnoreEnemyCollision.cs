using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreEnemyCollision : MonoBehaviour {

    private GameObject[] groundEnemies;

    private void Awake()
    {
        groundEnemies = GameObject.FindGameObjectsWithTag("GroundEnemy");
        foreach (GameObject groundEnemy in groundEnemies)
        {
            Physics2D.IgnoreCollision(groundEnemy.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }
}
