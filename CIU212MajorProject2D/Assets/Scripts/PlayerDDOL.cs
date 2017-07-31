using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDDOL : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        if (player.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
