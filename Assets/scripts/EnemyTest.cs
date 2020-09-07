using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour {

    public GameObject enemyTest;
    public GameObject target;

    private float speed = 2.0f;

	// Use this for initialization
	void Start () {
		
	}

    void EnemyFollow(Transform distance) {
        float xposition = distance.transform.position.x - enemyTest.transform.position.x;
        float yposition = distance.transform.position.y - enemyTest.transform.position.y;

        enemyTest.transform.Translate(xposition * Time.deltaTime * speed, yposition * Time.deltaTime * speed, 0.0f);
    }

	// Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(enemyTest.transform.position, target.transform.position);

        if (distance > 0.95)
        {
            EnemyFollow(target.transform);
        }
    }
}
