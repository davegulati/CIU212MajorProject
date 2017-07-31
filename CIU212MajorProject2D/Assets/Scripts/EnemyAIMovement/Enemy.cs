using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private CoinLoot coinLoot;

	[System.Serializable]
	public class EnemyStats
	{
		public int Health = 100;
	}

	public EnemyStats stats = new EnemyStats();

	void Start()
	{
		coinLoot = GameObject.Find("LootManager").GetComponent<CoinLoot>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{

			DamageEnemy(100);
		}
	}
	public void DamageEnemy(int damage)
	{
		stats.Health -= damage;
		if (stats.Health <= 0)
		{
			coinLoot.calculateLoot();

			Destroy(gameObject);
		}
	}
}