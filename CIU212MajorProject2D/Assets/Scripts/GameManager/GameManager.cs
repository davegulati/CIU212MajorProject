using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Inventory variables
    public bool isInventoryContainerVisible = false;
    private GameObject panel_InventoryContainer;

    // Use this for initialization
    void Start()
    {
        panel_InventoryContainer = GameObject.Find("Panel_InventoryContainer");
        panel_InventoryContainer.SetActive(isInventoryContainerVisible);
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
    }
}