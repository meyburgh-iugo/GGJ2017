using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PickUpSpawner : MonoBehaviour
{
  public GameObject Player;
  public PlayerEffect[] PlayerEffects;

  private float SpawnIntervalTime = 20.0f;

  // Use this for initialization
  void Start ()
  {

    Assert.IsTrue(PlayerEffects.Length > 0, "No player effects in pickup spawner!");

  }
	
	// Update is called once per frame
	void Update ()
  {
		
	}
}
