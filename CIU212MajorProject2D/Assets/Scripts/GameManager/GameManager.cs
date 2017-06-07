using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Inventory variables
    public bool isInventoryContainerVisible = true;
    private GameObject panel_InventoryContainer;

    // Use this for initialization
    void Start()
    {
        panel_InventoryContainer = GameObject.Find("Panel_InventoryContainer");
        panel_InventoryContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleInventory()
    {
        isInventoryContainerVisible = !isInventoryContainerVisible;
        panel_InventoryContainer.SetActive(isInventoryContainerVisible);
    }
}
