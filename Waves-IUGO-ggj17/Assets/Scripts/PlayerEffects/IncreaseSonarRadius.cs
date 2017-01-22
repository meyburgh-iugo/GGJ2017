﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSonarRadius : PlayerEffect
{
  private float RadiusIncrease = 3.0f; 

  public override void OnStartEffect(GameObject Player)
  {
    PhotonSpawner ps = Player.GetComponent<PhotonSpawner>();
    ps.photonLifeSpan += RadiusIncrease / ps.photonSpeed;
  }

  public override void OnStopEffect(GameObject Player)
  {
    PhotonSpawner ps = Player.GetComponent<PhotonSpawner>();
    ps.photonLifeSpan -= RadiusIncrease / ps.photonSpeed;
  }
}
