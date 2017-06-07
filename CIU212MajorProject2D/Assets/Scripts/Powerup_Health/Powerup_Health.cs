﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Health : MonoBehaviour {

    private float healthAwarded = 20.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().PlayerReceiveHealth(healthAwarded);
            Destroy(gameObject);
        }
    }
}
