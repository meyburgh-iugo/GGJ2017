using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEffect : MonoBehaviour
{
  public float Duration = 0.0f;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public abstract void OnStartEffect(GameObject Player);
  public abstract void OnStopEffect(GameObject Player);
}
