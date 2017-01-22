using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PickUpSpawner : MonoBehaviour
{
  public Transform Player;
  public GameObject PickUpPrefab;
  public float minDistFromSub = 10.0f;
  public float maxDistFromSub = 20.0f;
  public float maxAngleFromSubDownVector = 45.0f;

  private System.Type[] PlayerEffects;

  private float SpawnIntervalTime = 20.0f;
  private float Timer = 0.0f;

  // Use this for initialization
  void Start ()
  {
    PlayerEffects = new System.Type[1];
    PlayerEffects[0] = typeof(IncreaseSonarRadius);

    Assert.IsTrue(PlayerEffects.Length > 0, "No player effects in pickup spawner!");
    foreach(System.Type type in PlayerEffects)
    {
      Assert.IsNotNull(type, "Type not found!");
    }

    Timer = SpawnIntervalTime;
  }
	
	// Update is called once per frame
	void Update ()
  {
    Timer -= Time.deltaTime;
    if(Timer < 0.0f)
    {
      print("Spawning a pickup");

      Vector2 spawnPosition = Random.Range(minDistFromSub, maxDistFromSub) * Vector3.down + Player.position;

      System.Type type = PlayerEffects[Random.Range(0, PlayerEffects.Length)];
      PlayerEffect pickUpEffect = System.Activator.CreateInstance(type) as PlayerEffect;
      
      GameObject newPickup = Instantiate(PickUpPrefab, spawnPosition, Quaternion.identity);
      newPickup.GetComponent<PickUp>().SetEffect(pickUpEffect);

      Timer = SpawnIntervalTime;
    }
  }
}
