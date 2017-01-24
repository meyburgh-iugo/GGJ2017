using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerEffectSlot : MonoBehaviour
{
  private PlayerEffect Effect;
  private float Timer = 0.0f;

  // Use this for initialization
  void Start()
  {

  }

  public bool Empty()
  {
    return Effect == null;
  }

  public void Fill(PlayerEffect NewEffect)
  {
    Assert.IsNull(Effect, "Player already has effect.");
    Timer = 0.0f;
    Effect = NewEffect;
    Effect.OnStartEffect(this.gameObject);
  }

  // Update is called once per frame
  void Update()
  {
    if (!Empty())
    {
      Timer += Time.deltaTime;
      if (Timer > Effect.Duration)
      {
        Effect.OnStopEffect(this.gameObject);
        Effect = null;
      }
    }
  }
}
