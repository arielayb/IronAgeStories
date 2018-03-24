using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMover : MonoBehaviour {

	Rigidbody2D rb;
	float speed = 4.0f;

	// Use this for initialization
	void Start () {
		rb 			= GetComponent<Rigidbody2D>();
		rb.velocity = transform.forward * speed; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
