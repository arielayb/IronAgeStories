/**
 * The player control where the player interacts with the world
 * using specific keys.
 * 
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour {

	public GameObject player;

	public float playerSpeed = 1.0f;

	Animator animate;

	const int state_walkDown  = 0;
	const int state_walkRight = 1;
	const int state_walkUp    = 2;
	const int state_walkLeft  = 3;

	//idle states for animation
	const int state_idleUp    = 4;
	const int state_idleDown  = 5;
	const int state_idleRight = 6;
	const int state_idleLeft  = 7;

	//attack animation states
	const int state_attackUp    = 10;
	const int state_attackDown  = 8;
	const int state_attackRight = 9;
	const int state_attackLeft  = 11;

	//player's direction
	public bool playerFaceUp;
	public bool playerFaceDown;
	public bool playerFaceLeft;
	public bool playerFaceRight;

	bool playerAttackRight;
	bool playerAttackUp;
	bool playerAttackDown;
	bool playerAttackLeft;

	//player attack cool down properties
	float coolDownTimer = .13f;
	bool coolDownAttack;
	bool stopMoving;
	float timer;

//	enum playerDirection{
//		playerFaceUp,
//		playerFaceDown,
//		playerFaceLeft,
//		playerFaceRight
//	};

	enum playeranimation{
		state_walkDown    = 0,
		state_walkRight   = 1,
		state_walkUp      = 2,
		state_walkLeft    = 3,
		state_idleUp      = 4,
		state_idleDown    = 5,
		state_idleRight   = 6,
		state_idleLeft    = 7,
		state_attackUp    = 10,
		state_attackDown  = 8,
		state_attackRight = 9,
		state_attackLeft  = 11
	};

	playeranimation playerAnim;

	private Vector3 movement;

	// Use this for initialization
	void Start () {
		animate = GetComponent<Animator>();
		playerAttackRight = false;
		playerAttackUp    = false;
		playerAttackDown  = false;
		playerAttackLeft  = false;
	
	}

	void playerMovement(){
		
		//player's movement
		if(Input.GetKey(KeyCode.UpArrow) && !playerAttackUp){
			playerFaceUp    = true;	
			playerFaceRight = false;	
			playerFaceLeft  = false;
			playerFaceDown  = false;
			if(!stopMoving){
				player.transform.Translate(0, 1 * Time.deltaTime * playerSpeed, 0);
				animation(state_walkUp);				
			}
		}else if(Input.GetKeyUp(KeyCode.UpArrow)){	
			animation(state_idleUp);
		}

		if(Input.GetKey(KeyCode.DownArrow) && !playerAttackDown){
			playerFaceDown  = true;
			playerFaceUp    = false;	
			playerFaceRight = false;	
			playerFaceLeft  = false;
			if(!stopMoving){
				player.transform.Translate(0, -1 * Time.deltaTime * playerSpeed, 0);
				animation(state_walkDown);
			}
		}else if(Input.GetKeyUp(KeyCode.DownArrow)){	
			animation(state_idleDown);
		}

		if(Input.GetKey(KeyCode.LeftArrow) && !playerAttackLeft){			
			playerFaceLeft  = true;
			playerFaceRight = false;
			playerFaceDown  = false;
			playerFaceUp    = false;
			if(!stopMoving){				
				player.transform.Translate(-1 * Time.deltaTime * playerSpeed, 0, 0);
				animation(state_walkLeft);
			}
		}else if(Input.GetKeyUp(KeyCode.LeftArrow)){
			animation(state_idleLeft);
		}

		if(Input.GetKey(KeyCode.RightArrow) && !playerAttackRight){			
			playerFaceRight = true;	
			playerFaceLeft  = false;
			playerFaceDown  = false;
			playerFaceUp    = false;
			if(!stopMoving){				
				player.transform.Translate(1 * Time.deltaTime * playerSpeed, 0, 0);		
				animation(state_walkRight);		
			}
		}else if(Input.GetKeyUp(KeyCode.RightArrow)){
			animation(state_idleRight);
		}
	}

	void playerControl(){
		//player's control over the world using 
		//specific keys/buttons in the game.
		if(playerFaceUp && Input.GetKeyDown(KeyCode.Z)){
			playerAttackUp = true;
			stopMoving = true;
			if(playerAttackUp && !coolDownAttack){
				animate.Play("roguePlayer_attackUp");
				playerAttackUp = false;
				Invoke("attackCoolDown", coolDownTimer);
				coolDownAttack = true;
			}
		}
	
		if(playerFaceDown && Input.GetKeyDown(KeyCode.Z)){
			playerAttackDown = true;
			stopMoving = true;
			if(playerAttackDown && !coolDownAttack){
				animate.Play("roguePlayer_attackDown");
				playerAttackDown = false;	
				Invoke("attackCoolDown", coolDownTimer);
				coolDownAttack = true;
			}
		}
	
		if(playerFaceLeft && Input.GetKeyDown(KeyCode.Z)){
			playerAttackLeft = true;
			stopMoving = true;
			if(playerAttackLeft && !coolDownAttack){
				animate.Play("roguePlayer_attackLeft");
				playerAttackLeft = false;
				Invoke("attackCoolDown", coolDownTimer);
				coolDownAttack = true;
			}
		}

		if(playerFaceRight && Input.GetKeyDown(KeyCode.Z)){
			playerAttackRight = true;
			stopMoving = true;
			if(playerAttackRight && !coolDownAttack){
				animate.Play("roguePlayer_attackRight");
				playerAttackRight = false;
				Invoke("attackCoolDown", coolDownTimer);
				coolDownAttack = true;
			}
		}	
	}

	void animation(int states){
		switch(states){
			case 2:
			animate.SetInteger("state", state_walkUp);
			break;

			case 0:
			animate.SetInteger("state", state_walkDown);
			break;

			case 3:
			animate.SetInteger("state", state_walkLeft);
			break;

			case 1:
			animate.SetInteger("state", state_walkRight);
			break;

			case 4:
			animate.SetInteger("state", state_idleUp);
			break;

			case 5:
			animate.SetInteger("state", state_idleDown);
			break;

			case 6:
			animate.SetInteger("state", state_idleRight);
			break;

			case 7:
			animate.SetInteger("state", state_idleLeft);
			break;

			case 8:
			animate.SetInteger("state", state_attackDown);
			break;

			case 9:
			animate.SetInteger("state", state_attackRight);
			break;

			case 10:
			animate.SetInteger("state", state_attackUp);
			break;

			case 11:
			animate.SetInteger("state", state_attackLeft);
			break;
		}			
	}
		
	void attackCoolDown(){
		coolDownAttack = false;
		stopMoving = false;
	}
		
	void Update(){
		playerControl();
	}

	// Update is called once per frame
	void FixedUpdate () {
		playerMovement();
	}
}
