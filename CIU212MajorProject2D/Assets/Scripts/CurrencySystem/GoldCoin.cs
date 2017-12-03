using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int value = Random.Range(20, 50);
            ItemsManager.instance.ReceiveGold(value);
            Destroy(gameObject);
        }
    }
}
