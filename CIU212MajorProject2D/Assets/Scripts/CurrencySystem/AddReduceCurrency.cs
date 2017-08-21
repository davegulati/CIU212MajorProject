using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddReduceCurrency : MonoBehaviour
{
    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Sen");
	}

    void OnTriggerEnter2D()
    {
        if (gameObject.CompareTag("Money"))
        {
            Destroy(gameObject);
            player.GetComponent<currencySystem>().addMoney(50);
        }
    }
}
