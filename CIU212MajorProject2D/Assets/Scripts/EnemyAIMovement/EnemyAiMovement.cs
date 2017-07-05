using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiMovement : MonoBehaviour {
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;

	private Transform myTransform;

	void Awake() {
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");

		target = go.transform;

	}
}
	// Update is called once per frame
//	void Update () {

//		float distance = Vector3.Distance(target.transform.position, transform.position);

//		Debug.Log(distance);
//		if(distance < 10 && distance > 2) {
//			Debug.DrawLine(target.position, myTransform.position, Color.red);

			//look at target/rotate
//			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

			//move towards target
//			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
//			animation.Play("walk");

//		}

//		if(distance < 2) {

//			animation.Play("attack");
//		}

//		if(distance > 10) {

//			animation.Play("idle");
//		}

//	}
//}