using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetVolume : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 respawnPoint = GameObject.Find("RespawnPoint").transform.position;
            collision.gameObject.transform.position = respawnPoint;
        }
    }
}
