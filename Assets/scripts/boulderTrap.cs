using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderTrap : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Instantiate(shot, new Vector2(shotSpawn.position.x, shotSpawn.position.y), Quaternion.identity);
	}
}
