using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour {

	public GameObject box;
	private Rigidbody boxrb;

	private PlayerControl playerControl;
	private GameObject playerAttackBox;
	private float attackThrust = 51f;

	// Use this for initialization
	void Start () {
		boxrb = box.GetComponent<Rigidbody>();
		playerAttackBox = GameObject.FindGameObjectWithTag("Player");
		playerControl = playerAttackBox.GetComponent<PlayerControl>();
	}

	void OnTriggerEnter3D(Collider other) {			
		if(other.gameObject.tag == "playerAttack" && playerControl.playerFaceRight){
			Debug.Log("It works! right side");			
			boxrb.AddForce(Vector3.right * attackThrust);
		}

		if(other.gameObject.tag == "playerAttack" && playerControl.playerFaceLeft){
			Debug.Log("It works! left side");			
			boxrb.AddForce(Vector3.left * attackThrust);
		}

		if(other.gameObject.tag == "playerAttack" && playerControl.playerFaceUp){
			Debug.Log("It works! up side");			
			boxrb.AddForce(Vector3.forward * attackThrust);
		}

		if(other.gameObject.tag == "playerAttack" && playerControl.playerFaceDown){
			Debug.Log("It works! down side");			
			boxrb.AddForce(Vector3.back * attackThrust);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
