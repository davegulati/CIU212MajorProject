﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

	private GameObject sen;
	private int rotationOffset = 0;

    [HideInInspector]
    public float fireRate = 1;
    [SerializeField]
    private LayerMask whatToHit;

    private float timeToFire = 0;
    private Transform firePoint;

    [SerializeField]
    private Transform arrow;

	// Use this for initialization
	private void Awake () 
    {
        sen = GameObject.Find("Sen");
        firePoint = gameObject.transform.Find("FirePoint");

	}
	
	// Update is called once per frame
	void Update () 
    {
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		difference.Normalize();

		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		if (sen.transform.localScale.x < 0)
		{
			rotationOffset = 180;
		}
		else if (sen.transform.localScale.x > 0)
		{
			rotationOffset = 0;
		}
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

        if (Input.GetMouseButtonDown(0))
        {
            if (fireRate == 0)
            {
                ShootArrow();
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                ShootArrow();
            }
                
        }
	}

    private void ShootArrow ()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 200, whatToHit);
        Instantiate(arrow, firePoint.position, firePoint.rotation);
    }
}