using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

  public GameObject[] primitives;
  public int objectCount = 50;
  private int counter = 0;
  public Transform goal;
  private int maxDistance = 30;

	// Use this for initialization
	void Start ()
  {
	  for (int i = 0; i < objectCount; i++)
    {
      SpawnerARandomObstacle();
    }

    InvokeRepeating ("SpawnerARandomObstacle", 1.0f, 5.0f);
	}
	
  void SpawnerARandomObstacle()
  {
    var go = Instantiate(primitives[Random.Range(0, primitives.Length)], new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance)), Quaternion.identity);
    go.transform.localScale = new Vector3(Random.Range(0.25f, 10), Random.Range(0.25f, 10), 1);
    go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    go.transform.parent = transform;
    Rigidbody2D body = go.AddComponent<Rigidbody2D> ();
    body.AddForce (new Vector2 (Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
    body.mass = 1000000;
  }

  void Update()
  {
    ++counter;
    int index = counter % objectCount;

    if ((primitives[index].transform.position - goal.position).magnitude > maxDistance) {
      Rigidbody2D body = primitives [index].GetComponent<Rigidbody2D> ();
      body.AddForce ((goal.position - primitives[index].transform.position).normalized, ForceMode2D.Impulse);
    }
  }
}
