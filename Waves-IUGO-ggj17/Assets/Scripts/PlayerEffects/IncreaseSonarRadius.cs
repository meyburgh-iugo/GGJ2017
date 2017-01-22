using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSonarRadius : PlayerEffect
{
  private float RadiusIncrease = 3.0f;

  public override Color PickUpTint()
  {
    return Color.green;
  }

  public override void OnStartEffect(GameObject Player)
  {
    PhotonSpawner ps = Player.GetComponent<PhotonSpawner>();
    ps.photonLifeSpan += RadiusIncrease / ps.photonSpeed;
    HideOnStart.lifeSpan = 3f;
  }

  public override void OnStopEffect(GameObject Player)
  {
    PhotonSpawner ps = Player.GetComponent<PhotonSpawner>();
    ps.photonLifeSpan -= RadiusIncrease / ps.photonSpeed;
    HideOnStart.lifeSpan = 1f;
  }
}
