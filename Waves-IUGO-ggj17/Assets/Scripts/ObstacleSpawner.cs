using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

  public GameObject[] primitives;
  public int objectCount = 200;
  public int density = 15;

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
    var go = Instantiate(primitives[Random.Range(0, primitives.Length)], new Vector2(Random.Range(-objectCount/ density, objectCount/ density), Random.Range(-objectCount/ density, objectCount/ density)), Quaternion.identity);
    go.transform.localScale = new Vector3(Random.Range(0.25f, 10), Random.Range(0.25f, 10), 1);
    go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    go.transform.parent = transform;
    Rigidbody2D body = go.AddComponent<Rigidbody2D> ();
    body.AddForce (new Vector2 (Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
    body.mass = 1000000;
  }
}
