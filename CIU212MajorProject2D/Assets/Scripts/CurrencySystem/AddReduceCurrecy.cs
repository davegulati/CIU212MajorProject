using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddReduceCurrecy : MonoBehaviour
{
    public currencySystem currency;

	// Use this for initialization
	void Start ()
    {
	}

    void OnTriggerEnter2D()
    {
        if (gameObject.CompareTag("Money"))
        {
            Destroy(gameObject);
            currency.addMoney(100);
        }
    }
}
