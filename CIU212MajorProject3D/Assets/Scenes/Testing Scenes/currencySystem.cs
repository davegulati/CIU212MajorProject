using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencySystem : MonoBehaviour
{

    public int money;

    public Text moneyCounter;

	// Use this for initialization
	void Start ()
    {
        money = 100;
        moneyCounter.text = "Money: " + money.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void addMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        moneyCounter.text = "Money: " + money.ToString();
    }

    public void subtractMoney(int moneyToSubtract)
    {
        if(money - moneyToSubtract < 0)
        {
            Debug.Log("Not Enough Money");
        }

        else
        {
            money -= moneyToSubtract;
            moneyCounter.text = "Money: " + money.ToString();
        }
    }
}
