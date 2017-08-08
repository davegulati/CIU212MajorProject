using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddReduceCurrecy2 : MonoBehaviour
{
    private GameObject player;

	// Use this for initialization
	void Start ()
    {
		player = GameObject.Find ("Sen");
	}

//    public void OnMouseDown()
//    {
//        player.GetComponent<currencySystem>().addMoney(10);
//    }

    void OnTriggerEnter2D()
    {
        if (gameObject.CompareTag("Money"))
        {
            gameObject.SetActive(false);
            player.GetComponent<currencySystem>().addMoney(100);
        }
    }
}
