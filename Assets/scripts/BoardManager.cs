using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 8;
	public int rows = 8;

	public Count wallcount = new Count(5, 9);
	public GameObject exit;

	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] outerwallTilesX;
	public GameObject[] outerwallTilesY;
	public GameObject[] enemyTiles;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	void InitializeList()
	{
		gridPositions.Clear();

		for(int x = 1; x < columns - 1; x++)
		{
			for(int y = 1; y < rows - 1; y++)
			{
				gridPositions.Add(new Vector3(x, y, 0f));
			}
		}

	}

	void BoardSetup()
	{
		boardHolder = new GameObject("Board").transform;
	
		for(int x = -1; x < columns + 1; x++)
		{
			for(int y = -1; y < rows + 1; y++)
			{
				GameObject toInstantiate = floorTiles[Random.Range (0, floorTiles.Length)];
				if (x == -1 || x == columns) //|| y == -1 || y == rows)
				{
					toInstantiate = outerwallTilesX[Random.Range(0, outerwallTilesX.Length)];
				}else if( y == -1 || y == rows)
				{
					toInstantiate = outerwallTilesY[Random.Range(0, outerwallTilesY.Length)];
				}
			
				GameObject instance = (GameObject)Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity );
			
				instance.transform.SetParent(boardHolder);
			}
		}

	}

	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);
		return randomPosition;
	}

	void LayoutObjectRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range(minimum, maximum + 1);

		for(int i = 0; i < objectCount; ++i)
		{
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
			Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}

	}

	public void SetupScene(int level)
	{
		BoardSetup();
		InitializeList();
		LayoutObjectRandom(wallTiles, wallcount.minimum, wallcount.maximum);
		//int enemyCount = (int)Mathf.Log(level, 2f);
		//LayoutObjectRandom(enemyTiles, enemyCount, enemyCount);
	
		Instantiate(exit, new Vector3(columns - 1, rows, 0f), Quaternion.identity);

	}
}
