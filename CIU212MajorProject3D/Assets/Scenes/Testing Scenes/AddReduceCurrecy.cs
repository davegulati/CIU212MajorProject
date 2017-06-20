using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddReduceCurrecy : MonoBehaviour
{
    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    player.GetComponent<currencySystem>().addMoney(5);
        //}

        //if (Input.GetButtonDown("Fire2"))
        //{
        //    player.GetComponent<currencySystem>().subtractMoney(5);
        //}
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
