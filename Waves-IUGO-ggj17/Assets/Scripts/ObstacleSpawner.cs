using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

  public GameObject[] primitives;
  public int objectCount = 200;
  public int density = 10;

	// Use this for initialization
	void Start ()
  {
	  for (int i = 0; i < objectCount; i++)
    {
      SpawnerARandomObstacle();
    }  	
	}
	
	// Update is called once per frame
	void Update ()
  {
		
	}

  void SpawnerARandomObstacle()
  {
    var go = Instantiate(primitives[Random.Range(0, primitives.Length - 1)], new Vector2(Random.Range(-objectCount/ density, objectCount/ density), Random.Range(-objectCount/ density, objectCount/ density)), Quaternion.identity);
    go.transform.localScale = new Vector3(Random.Range(0.25f, 10), Random.Range(0.25f, 10), 1);
    go.transform.parent = transform;
  }
}
