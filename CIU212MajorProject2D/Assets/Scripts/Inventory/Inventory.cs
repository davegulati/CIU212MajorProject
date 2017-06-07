using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private GameObject panel_ShopContainer;
    private GameManager gameManager;

	// Use this for initialization
	void Start () 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        panel_ShopContainer = GameObject.Find("Panel_ShopContainer");
        panel_ShopContainer.SetActive(false);
	}

    public void ShopClicked ()
    {
        panel_ShopContainer.SetActive(true);
        gameManager.isInventoryContainerVisible = false;
        gameObject.SetActive(false);
    }
}
