using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PickUpSpawner : MonoBehaviour
{
  public Transform Player;
  public GameObject PickUpPrefab;
  private System.Type[] PlayerEffects;

  private float SpawnIntervalTime = 20.0f;

  // Use this for initialization
  void Start ()
  {
    PlayerEffects = new System.Type[1];
    PlayerEffects[0] = System.Type.GetType("IncreaseSonarRadius");

    Assert.IsTrue(PlayerEffects.Length > 0, "No player effects in pickup spawner!");
    foreach(System.Type type in PlayerEffects)
    {
      Assert.IsNotNull(type, "Type not found!");
    }
  }
	
	// Update is called once per frame
	void Update ()
  {

    
  }
}
