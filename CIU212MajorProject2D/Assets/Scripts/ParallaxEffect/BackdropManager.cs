﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropManager : MonoBehaviour {

    public Sprite[] layers;

    private void Awake()
    {
        LoadLayers();
    }

    private void LoadLayers()
    {
		transform.Find("Layer1").GetComponent<BackdropLayer>().LoadLayer(layers[0]);
		transform.Find("Layer2").GetComponent<BackdropLayer>().LoadLayer(layers[1]);
		transform.Find("Layer3").GetComponent<BackdropLayer>().LoadLayer(layers[2]);
        if (layers.Length == 4)
        {
            transform.Find("Layer4").GetComponent<BackdropLayer>().LoadLayer(layers[3]);
        }
        else
        {
            transform.Find("Layer4").GetComponent<BackdropLayer>().DeactivateLayer();
        }

        //Debug.Log(layers.Length);
    }

    private void FixedUpdate () 
    {
        Vector3 camPosition = Camera.main.transform.position;
        transform.position = camPosition;
	}
}