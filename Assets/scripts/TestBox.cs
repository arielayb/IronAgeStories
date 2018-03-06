using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour {

	public GameObject box;

	private PlayerControl playerControl;
	private PolygonCollider2D playerAttack;
	private GameObject playerAttackBox;

	// Use this for initialization
	void Start () {
		box.GetComponent<CircleCollider2D>();
		playerAttackBox = GameObject.FindGameObjectWithTag("playerAttack");
		playerAttack = playerAttackBox.GetComponent<PolygonCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D other) {		
		if(other.gameObject.tag == "playerAttack")
			Debug.Log("It works!");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
