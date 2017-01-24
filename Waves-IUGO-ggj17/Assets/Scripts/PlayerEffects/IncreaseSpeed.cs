using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : PlayerEffect
{
  float speedFactor = 1.75f;

  public override Color PickUpTint()
  {
    return Color.yellow;
  }

  public override void OnStartEffect(GameObject Player)
  {
    Duration = 3.0f;
    Player.GetComponent<Movement>().speed *= speedFactor;
  }

  public override void OnStopEffect(GameObject Player)
  {
    Player.GetComponent<Movement>().speed /= speedFactor;
  }
}
