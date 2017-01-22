using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEffect
{
  public float Duration = 0.0f;

  public abstract void OnStartEffect(GameObject Player);
  public abstract void OnStopEffect(GameObject Player);
}
