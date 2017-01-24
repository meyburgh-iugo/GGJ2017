using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish_selfshining : MonoBehaviour
{
  SpriteRenderer sprite;
  private float alpha = 0.0f;
  private float maxAlpha = 0.15f;
  private Color color;
  private Transform player;

  // Use this for initialization
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    sprite = GetComponent<SpriteRenderer>();
    color = sprite.color;
    sprite.color = new Color(color.r, color.g, color.b, alpha);
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    alpha = Mathf.Clamp((alpha + collision.collider.GetComponent<SpriteRenderer>().color.a) / 2, alpha, maxAlpha);
    sprite = GetComponent<SpriteRenderer>();
    sprite.color = new Color(color.r, color.g, color.b, alpha);
  }

  private void Update()
  {
    float deepAlpha = Mathf.Lerp(0.2f, 0.0f, Mathf.Abs(player.position.y) / 80.0f);

    alpha = Mathf.Max(deepAlpha, Mathf.Sin(Time.time)* 0.5f + 0.1f);
    sprite.color = new Color(color.r, color.g, color.b, alpha);
  }
}
