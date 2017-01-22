using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour
{
  private SpriteRenderer sprite;
  private float lifeSpan;
  private float maxLifeSpan;

  public float extraLife;

  private void Awake()
  {
    sprite = GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    lifeSpan += Time.deltaTime;

    if (lifeSpan >= maxLifeSpan)
    {
      Destroy(gameObject);
      return;
    }

    var newColor = sprite.color;
    newColor.a = 1f - (lifeSpan / maxLifeSpan);
    sprite.color = newColor;
  }

  public void Setup(float _maxLifeSpan, float _lifeSpan)
  {
    maxLifeSpan = _maxLifeSpan + extraLife;
    lifeSpan = _lifeSpan;
  }
}
