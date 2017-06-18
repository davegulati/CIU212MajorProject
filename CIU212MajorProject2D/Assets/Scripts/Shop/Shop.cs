using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    private GameObject sen;

    private GameObject panel_ShopContainer;
    public bool isShopContainerVisible = false;
    private float activationRange = 0.8f;

	// Use this for initialization
	void Awake () 
    {
        sen = GameObject.Find("Sen");
		panel_ShopContainer = GameObject.Find("Panel_ShopContainer");
		panel_ShopContainer.SetActive(isShopContainerVisible);
	}
	
	// Update is called once per frame
	void Update () 
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange && Input.GetKeyDown(KeyCode.E))
		{
            ToggleShop();
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
