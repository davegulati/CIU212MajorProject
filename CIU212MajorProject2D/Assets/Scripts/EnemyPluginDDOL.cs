using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPluginDDOL : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        GameObject[] a_ = GameObject.FindGameObjectsWithTag("A_");
        if (a_.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
