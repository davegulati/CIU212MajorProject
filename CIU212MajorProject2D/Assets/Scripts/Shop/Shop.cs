using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    private GameObject sen;
    private GameObject itemCanvas;
    private GameObject panel_ShopContainer;
    private bool isShopContainerVisible = false;
    private float activationRange = 0.8f;

	// Use this for initialization
	void Awake () 
    {
        sen = GameObject.Find("Sen");
		panel_ShopContainer = GameObject.Find("Panel_ShopContainer");
		panel_ShopContainer.SetActive(isShopContainerVisible);
        itemCanvas = transform.Find("ItemCanvas").gameObject;
        itemCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange)
		{
            itemCanvas.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                ToggleShop();
            }
		}
        else
        {
            itemCanvas.SetActive(false);
        }
	}

    public void ToggleShop ()
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
    }
}
