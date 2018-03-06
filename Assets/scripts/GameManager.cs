using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	private BoardManager boardScript;

	private int level = 3;

	void Awake()
	{
		if(instance == null)
		{
			instance  = this;
		}else if(instance != this)
		{
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		boardScript = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame()
	{
		boardScript.SetupScene(level);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
