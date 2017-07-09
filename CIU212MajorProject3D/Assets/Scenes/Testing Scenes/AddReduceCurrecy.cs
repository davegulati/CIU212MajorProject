using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddReduceCurrecy : MonoBehaviour
{
    public GameObject player;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
     
    }

    public void OnMouseDown()
    {
        player.GetComponent<currencySystem>().addMoney(10);
    }

    void OnTriggerEnter()
    {
        if (gameObject.CompareTag("Money"))
        {
            gameObject.SetActive(false);
            player.GetComponent<currencySystem>().addMoney(20);
        }
    }
}
