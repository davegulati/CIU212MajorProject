using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSpin : MonoBehaviour
{
    public float speed = 5f;


    void Update()
    {
        transform.Rotate(Vector3.forward * speed);
    }
}
