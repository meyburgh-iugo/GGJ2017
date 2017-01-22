using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

  public GameObject[] primitives;
  public int objectCount = 50;
  private int counter = 0;
  public Transform player;
  private int maxDistance = 30;
  private List<GameObject> obstacles;

	// Use this for initialization
	void Start ()
  {
    obstacles = new List<GameObject>();

    for (int i = 0; i < objectCount; i++)
    {
      SpawnerARandomObstacle();
    }

    InvokeRepeating ("SpawnerARandomObstacle", 1.0f, 5.0f);
	}
	
  void SpawnerARandomObstacle()
  {
    var go = Instantiate(primitives[Random.Range(0, primitives.Length)], new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance)), Quaternion.identity);
    float slc = Random.Range(0.25f, 10);
    go.transform.localScale = new Vector3(slc, slc, 1);
    go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    go.transform.parent = transform;
    Rigidbody2D body = go.AddComponent<Rigidbody2D> ();
    body.AddForce (new Vector2 (Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
    body.mass = 1000000;

    obstacles.Add(go);
  }

  void Update()
  {
    ++counter;
    int index = counter % objectCount;
    
    if ((obstacles[index].transform.position - player.position).magnitude > maxDistance) {
      Rigidbody2D body = obstacles[index].GetComponent<Rigidbody2D> ();

      if (Random.Range(0.0f, 1.0f) < 0.8f)
      {
        obstacles[index].transform.position = new Vector3(player.position.x + Random.Range(-maxDistance / 2, maxDistance / 2), player.position.y - (maxDistance / 2) - Random.Range(-maxDistance / 2, 0));
      }
      else
      {
        obstacles[index].transform.position = new Vector3(player.position.x + Random.Range(-maxDistance / 2, maxDistance / 2), player.position.y + (maxDistance / 2) + Random.Range(0, maxDistance / 2));
      }
      float slc = Random.Range(0.25f, 10);
      obstacles[index].transform.localScale = new Vector3(slc, slc, 1);
      obstacles[index].transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
      obstacles[index].transform.parent = transform;
      body.AddForce(new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
      body.mass = 1000000;
    }
  }
}
