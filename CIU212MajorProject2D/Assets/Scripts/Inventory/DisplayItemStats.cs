using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemStats : MonoBehaviour
{
    public LayerMask mask;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        int layermask = LayerMask.GetMask("UI");

        if (hit.collider)
        {
            Debug.Log(hit.transform.name);
        }
    }
}
