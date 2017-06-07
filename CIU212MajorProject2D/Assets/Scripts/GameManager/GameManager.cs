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
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Debug.Log("Tab was pressed.");
			ToggleInventory();
		}
    }

    public void ToggleInventory()
    {
        isInventoryContainerVisible = !isInventoryContainerVisible;
        panel_InventoryContainer.SetActive(isInventoryContainerVisible);
        if (isInventoryContainerVisible)
        {
            Time.timeScale = 0;
        }
        else if (!isInventoryContainerVisible)
        {
            Time.timeScale = 1;
        }
        isShopContainerVisible = false;
        panel_ShopContainer.SetActive(isShopContainerVisible);
    }

	public void ToggleShop()
	{
		isShopContainerVisible = !isShopContainerVisible;
		panel_ShopContainer.SetActive(isShopContainerVisible);
		if (isShopContainerVisible)
		{
            Time.timeScale = 0;
		}
		else if (!isShopContainerVisible)
		{
            Time.timeScale = 1;
		}
        isInventoryContainerVisible = false;
        panel_InventoryContainer.SetActive(isInventoryContainerVisible);
	}
}
