using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

	[System.Serializable]
	public class DropItem
	{
		public string name;
		public GameObject item;
		public int dropRate;
	}

	public List <DropItem> LootTable = new List<DropItem>();
	public int dropChance;

	public void calculateLoot()
	{
		GameObject chest = GameObject.Find("ChestOpen");
		int calc_dropChance = Random.Range (0, 101);

		if(calc_dropChance > dropChance)
		{
			Debug.Log ("Not loot dropped.");
			return;
		}

		if (calc_dropChance <= dropChance)
		{
			int itemDrop = 0;

			for (int i = 0; i < LootTable.Count; i++)
			{
				itemDrop += LootTable [i].dropRate;
			}
			Debug.Log ("Item Drop = " + itemDrop);

			int randomValue = Random.Range (0, itemDrop);

			for (int j = 0; j < LootTable.Count; j++)
			{
				if (randomValue <= LootTable [j].dropRate)
				{
					Instantiate(LootTable[j].item, chest.transform.position, Quaternion.identity);
					return;
				}
				randomValue -= LootTable [j].dropRate;
				Debug.Log ("Random Value Decreased" + randomValue);
			}
		}
	}
}
