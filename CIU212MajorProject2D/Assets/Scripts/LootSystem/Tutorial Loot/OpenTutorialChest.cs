using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorialChest : MonoBehaviour
{
	private TutorialLoot tutorialLoot;
    private ChestCoinLoot chestCoinLoot;

	public GameObject chestTutorial;
	public GameObject chestTutorialText;
	public GameObject itemTutorial;
	private GameObject sen;
	private float activationRange = 1.4f;

	// Use this for initialization
	void Start ()
	{
		tutorialLoot = GameObject.Find ("LootManager").GetComponent<TutorialLoot>();
        chestCoinLoot = GameObject.Find("LootManager").GetComponent<ChestCoinLoot>();

        sen = GameObject.Find("Sen");
	}
	
	// Update is called once per frame
	private void Update ()
	{
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetButtonDown("Interact"))
		{
			tutorialLoot.CalculateLoot();
            chestCoinLoot.calculateLoot();

			Destroy(chestTutorial);
			Destroy(chestTutorialText);
			itemTutorial.SetActive(true);

			OpenChestAnimation();
		}
	}

	private void OpenChestAnimation()
	{
		gameObject.SetActive(false);
	}
}

