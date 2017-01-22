using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSonarRadius : PlayerEffect
{
  private float radiusIncrease = 2.0f; 

	// Use this for initialization
	void Start ()
  {
		
	}
	
	// Update is called once per frame
	void Update ()
  {
		
	}

  public override void OnStartEffect(GameObject Player)
  {
    PhotonSpawner ps = Player.GetComponent<PhotonSpawner>();
    ps.photonLifeSpan += radiusIncrease / ps.photonSpeed;
  }

  public override void OnStopEffect(GameObject Player)
  {
    PhotonSpawner ps = Player.GetComponent<PhotonSpawner>();
    ps.photonLifeSpan -= radiusIncrease / ps.photonSpeed;
  }
}
