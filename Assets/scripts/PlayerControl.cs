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

	//block animation states
	const int state_blockUp    = 12;
	const int state_blockDown  = 13;
	const int state_blockRight = 14;
	const int state_blockLeft  = 15;

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
	float coolDownTimer = .25f;
	bool coolDownAttack;
	bool stopMoving;
	float timer;

	//player button controller
	bool attackButton;
	bool blockButton;

	//player sound effects
	public AudioSource swordSlash;


//	enum playerDirection{
//		playerFaceUp,
//		playerFaceDown,
//		playerFaceLeft,
//		playerFaceRight
//	};

//	enum playeranimation{
//		state_walkDown    = 0,
//		state_walkRight   = 1,
//		state_walkUp      = 2,
//		state_walkLeft    = 3,
//		state_idleUp      = 4,
//		state_idleDown    = 5,
//		state_idleRight   = 6,
//		state_idleLeft    = 7,
//		state_attackUp    = 10,
//		state_attackDown  = 8,
//		state_attackRight = 9,
//		state_attackLeft  = 11
//	};
//
//	playeranimation playerAnim;

	private Vector3 movement;

	// Use this for initialization
	void Start () {
		animate = GetComponent<Animator>();
		swordSlash = GetComponent<AudioSource>();
	
	}

	void playerMovement(){
		
		//player's movement
		if(Input.GetKey(KeyCode.UpArrow) || 
			(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
			|| (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))){
			playerFaceUp    = true;	
			playerFaceRight = false;	
			playerFaceLeft  = false;
			playerFaceDown  = false;
			if(!stopMoving){
				player.transform.Translate(0, 1 * Time.deltaTime * playerSpeed, 0);
				animation(state_walkUp);				
			}
		}else if(Input.GetKeyUp(KeyCode.UpArrow) || 
			(Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.RightArrow))
			|| (Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.LeftArrow))){	
			animation(state_idleUp);
		}

		if(Input.GetKey(KeyCode.DownArrow) ||
			(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
			|| (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))){
			playerFaceDown  = true;
			playerFaceUp    = false;	
			playerFaceRight = false;	
			playerFaceLeft  = false;
			if(!stopMoving){
				player.transform.Translate(0, -1 * Time.deltaTime * playerSpeed, 0);
				animation(state_walkDown);
			}
		}else if(Input.GetKeyUp(KeyCode.DownArrow) || 
			(Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKeyUp(KeyCode.RightArrow))
			|| (Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKeyUp(KeyCode.LeftArrow))){	
			animation(state_idleDown);
		}

		if(Input.GetKey(KeyCode.LeftArrow) || 
			(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
			|| (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))){			
			playerFaceLeft  = true;
			playerFaceRight = false;
			playerFaceDown  = false;
			playerFaceUp    = false;
			if(!stopMoving){				
				player.transform.Translate(-1 * Time.deltaTime * playerSpeed, 0, 0);
				animation(state_walkLeft);
			}
		}else if(Input.GetKeyUp(KeyCode.LeftArrow) || 
			(Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKeyUp(KeyCode.UpArrow))
			|| (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKeyUp(KeyCode.DownArrow))){
			animation(state_idleLeft);
		}

		if(Input.GetKey(KeyCode.RightArrow) || 
			(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
			|| (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))){			
			playerFaceRight = true;	
			playerFaceLeft  = false;
			playerFaceDown  = false;
			playerFaceUp    = false;
			if(!stopMoving){				
				player.transform.Translate(1 * Time.deltaTime * playerSpeed, 0, 0);		
				animation(state_walkRight);		
			}
		}else if(Input.GetKeyUp(KeyCode.RightArrow) || 
			(Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKeyUp(KeyCode.UpArrow))
			|| (Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKeyUp(KeyCode.DownArrow))){
				animation(state_idleRight);
		}
	}

	void playerControl(){
		//player's control over the world using 
		//specific keys/buttons in the game.

		attackButton = Input.GetKeyDown(KeyCode.Z);

		if(attackButton)
			swordSlash.Play();
			
		//player's attack directions
		if(playerFaceUp && attackButton){
			playerAttackUp = true;
			stopMoving = true;
			if(playerAttackUp && !coolDownAttack){
				animate.Play("roguePlayer_attackUp");
				playerAttackUp = false;
				Invoke("attackCoolDown", coolDownTimer);
				animation(state_idleUp);
				coolDownAttack = true;
			}
		}
			
		if(playerFaceDown && attackButton){
			playerAttackDown = true;
			stopMoving = true;
			if(playerAttackDown && !coolDownAttack){
				animate.Play("roguePlayer_attackDown");
				playerAttackDown = false;	
				Invoke("attackCoolDown", coolDownTimer);
				animation(state_idleDown);
				coolDownAttack = true;
			}
		}
	
		if(playerFaceLeft && attackButton){
			playerAttackLeft = true;
			stopMoving = true;
			if(playerAttackLeft && !coolDownAttack){
				animate.Play("roguePlayer_attackLeft");
				playerAttackLeft = false;
				Invoke("attackCoolDown", coolDownTimer);
				animation(state_idleLeft);
				coolDownAttack = true;
			}
		}

		if(playerFaceRight && attackButton){
			playerAttackRight = true;
			stopMoving = true;
			if(playerAttackRight && !coolDownAttack){
				animate.Play("roguePlayer_attackRight");
				playerAttackRight = false;
				Invoke("attackCoolDown", coolDownTimer);
				animation(state_idleRight);
				coolDownAttack = true;
			}
		}	
	
		//player's block directions
		if(playerFaceUp && Input.GetKey(KeyCode.X)){
			stopMoving = true;
			animate.Play("roguePlayer_blockUp");

		}else if(playerFaceUp && Input.GetKeyUp(KeyCode.X)){
			animation(state_idleUp);
			stopMoving = false;
		}
	
		if(playerFaceDown && Input.GetKey(KeyCode.X)){
			stopMoving = true;
			animate.Play("roguePlayer_blockDown");

		}else if(playerFaceDown && Input.GetKeyUp(KeyCode.X)){
			animation(state_idleDown);
			stopMoving = false;
		}

		if(playerFaceLeft && Input.GetKey(KeyCode.X)){
			stopMoving = true;
			animate.Play("roguePlayer_blockLeft");

		}else if(playerFaceLeft && Input.GetKeyUp(KeyCode.X)){
			animation(state_idleLeft);
			stopMoving = false;
		}

		if(playerFaceRight && Input.GetKey(KeyCode.X)){
			stopMoving = true;
			animate.Play("roguePlayer_blockRight");

		}else if(playerFaceRight && Input.GetKeyUp(KeyCode.X)){
			animation(state_idleRight);
			stopMoving = false;
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

//			case 8:
//			animate.SetInteger("state", state_attackDown);
//			break;
//
//			case 9:
//			animate.SetInteger("state", state_attackRight);
//			break;
//
//			case 10:
//			animate.SetInteger("state", state_attackUp);
//			break;
//
//			case 11:
//			animate.SetInteger("state", state_attackLeft);
//			break;
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
		//playerControl();
		playerMovement();
	}
}
