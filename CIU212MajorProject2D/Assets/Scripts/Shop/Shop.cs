using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	private GameObject panel_InventoryContainer;
	private GameManager gameManager;

	// Use this for initialization
	void Start () 
    {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		panel_InventoryContainer = GameObject.Find("Panel_InventoryContainer");
		//panel_InventoryContainer.SetActive(false);		
	}
	
	// Update is called once per frame
	void Update () 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();	
	}

    public void InventoryClicked ()
    {
		gameManager.ToggleInventory();
		gameManager.ToggleShop();
    }
}
