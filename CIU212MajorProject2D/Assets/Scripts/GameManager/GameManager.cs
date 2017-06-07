using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Inventory variables
    public bool isInventoryContainerVisible = false;
    private GameObject panel_InventoryContainer;

	//Inventory variables
	public bool isShopContainerVisible = false;
	private GameObject panel_ShopContainer;

    // Use this for initialization
    void Start()
    {
        panel_InventoryContainer = GameObject.Find("Panel_InventoryContainer");
        panel_InventoryContainer.SetActive(isInventoryContainerVisible);

		panel_ShopContainer = GameObject.Find("Panel_ShopContainer");
		panel_ShopContainer.SetActive(isShopContainerVisible);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleInventory()
    {
        isInventoryContainerVisible = !isInventoryContainerVisible;
        panel_InventoryContainer.SetActive(isInventoryContainerVisible);

        isShopContainerVisible = false;
        panel_ShopContainer.SetActive(isShopContainerVisible);
    }

	public void ToggleShop()
	{
		isShopContainerVisible = !isShopContainerVisible;
		panel_ShopContainer.SetActive(isShopContainerVisible);

        isInventoryContainerVisible = false;
        panel_InventoryContainer.SetActive(isInventoryContainerVisible);
	}
}
