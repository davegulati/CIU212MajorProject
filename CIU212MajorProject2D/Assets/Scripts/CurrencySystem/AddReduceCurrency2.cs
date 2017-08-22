using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddReduceCurrency2 : MonoBehaviour
{
    private currencySystem currency;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        currency = GameObject.Find("Canvas").transform.Find("MoneyPanel").transform.Find("MoneyCounter").GetComponent<currencySystem>();

        player = GameObject.Find("Sen");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.CompareTag("Money"))
            {
                Destroy(gameObject);
                currency.addMoney(100);
            }
        }
    }
}
