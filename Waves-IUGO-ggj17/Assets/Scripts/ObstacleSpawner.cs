using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

  public GameObject[] primitives;
  private int objectCount = 60;
  private int counter = 0;
  public Transform player;
  private int maxDistance = 20;
  private List<GameObject> obstacles;
  private float maxSpeed = 0.1f;
  private float maxSpin = 0.5f;
  private float minScale = 1;
  private float maxScale = 10;

  public GameObject[] prefab_fishes;
  private int fishCount = 15;
  private List<GameObject> fishes;
  // Use this for initialization
  void Start ()
  {
    obstacles = new List<GameObject>();
    fishes = new List<GameObject>();

    for (int i = 0; i < objectCount; i++)
    {
      SpawnerARandomObstacle();
    }

    for (int i = 0; i < fishCount; i++)
    {
      SpawnerARandomFish();
    }

    InvokeRepeating ("SpawnerARandomObstacle", 1.0f, 5.0f);
  }
	
  void SpawnerARandomObstacle()
  {
    var go = Instantiate(primitives[Random.Range(0, primitives.Length)], new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance)), Quaternion.identity);
    float slc = Random.Range(minScale, maxScale);
    go.transform.localScale = new Vector3(slc, slc, 1);
    go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    go.transform.parent = transform;

    var body = go.GetComponent<Rigidbody2D> ();
    body.velocity = new Vector2(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed));
    body.angularVelocity = Random.Range(-maxSpin, maxSpin);

    body.drag = 0;
    body.angularDrag = 0;

    obstacles.Add(go);
  }

  void SpawnerARandomFish()
  {
    var go = Instantiate(prefab_fishes[Random.Range(0, prefab_fishes.Length)], new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance)), Quaternion.identity);
    float slc = Random.Range(0.5f, 1.5f);
    go.transform.localScale = new Vector3(slc, slc, 1);
    go.transform.parent = transform;

    var body = go.GetComponent<Rigidbody2D>();
    body.AddForce(new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f)), ForceMode2D.Impulse);

    fishes.Add(go);
  }

  void Update()
  {
    ++counter;
    int index = counter % objectCount;
    
    if ((obstacles[index].transform.position - player.position).magnitude > maxDistance)
    {
      if (Random.Range(0.0f, 1.0f) < 0.8f)
      {
        obstacles[index].transform.position = new Vector2(player.position.x + Random.Range(-maxDistance / 2, maxDistance / 2), player.position.y - (maxDistance / 2) - Random.Range(maxDistance / 2, 0));
      }
      else
      {
        obstacles[index].transform.position = new Vector3(player.position.x + Random.Range(-maxDistance / 2, maxDistance / 2), player.position.y + (maxDistance / 2) + Random.Range(0, maxDistance / 2));
      }
      float slc = Random.Range(1, 10);
      obstacles[index].transform.localScale = new Vector3(slc, slc, 1);
      obstacles[index].transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
      obstacles[index].transform.parent = transform;
    }

    index = counter % fishCount;
    if ((fishes[index].transform.position - player.position).magnitude > maxDistance)
    {
      Rigidbody2D body = fishes[index].GetComponent<Rigidbody2D>();

      if (Random.Range(0.0f, 1.0f) < 0.8f)
      {
        fishes[index].transform.position = new Vector2(player.position.x + Random.Range(-maxDistance / 2, maxDistance / 2), player.position.y - (maxDistance / 2) - Random.Range(maxDistance / 2, 0));
      }
      else
      {
        fishes[index].transform.position = new Vector3(player.position.x + Random.Range(-maxDistance / 2, maxDistance / 2), player.position.y + (maxDistance / 2) + Random.Range(0, maxDistance / 2));
      }
      float slc = Random.Range(0.5f, 1.5f);
      obstacles[index].transform.localScale = new Vector3(slc, slc, 1);
      obstacles[index].transform.parent = transform;
      body.AddForce(new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
    }
  }
}
