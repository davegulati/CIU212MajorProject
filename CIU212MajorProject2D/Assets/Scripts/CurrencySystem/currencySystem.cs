using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currencySystem : MonoBehaviour
{

    public int money;

    private Text moneyCounter;

	// Use this for initialization
	void Start ()
    {
        moneyCounter = GameObject.Find("MoneyCounter").GetComponent<Text>();
        money = 20;
        moneyCounter.text = "Money: " + money.ToString();
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
           // Debug.Log("Not Enough Money");
        }

        else
        {
            money -= moneyToSubtract;
            moneyCounter.text = "Money: " + money.ToString();
        }
    }
}
