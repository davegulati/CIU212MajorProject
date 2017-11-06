using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToCamY : MonoBehaviour {

	void FixedUpdate () 
    {
        Vector3 camPosition = Camera.main.transform.position;
        transform.position = camPosition;
	}
}