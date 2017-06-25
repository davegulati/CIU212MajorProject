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

	// Use this for initialization
	private void Start ()
	{
		
	}
	
	// Update is called once per frame
	private void Update ()
	{
		
	}
}
