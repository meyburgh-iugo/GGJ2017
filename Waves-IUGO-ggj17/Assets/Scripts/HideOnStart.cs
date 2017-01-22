using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStart : MonoBehaviour
{
  SpriteRenderer sprite;
  private float alpha = 0.0f;
  private float maxAlpha = 0.15f;
  public static float lifeSpan = 1.0f;
  private Vector3 color = new Vector3(1,1,1);
  private Transform player;
  private float abyssStart = 80.0f;

  // Use this for initialization
  void Start ()
  {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    sprite = GetComponent<SpriteRenderer>();
    sprite.color = new Color (color.x, color.y, color.z, alpha);

    abyssStart = Camera.main.GetComponent<PlayerCamera>().GetAbyssStart();

  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    alpha = Mathf.Clamp ((alpha + collision.collider.GetComponent<SpriteRenderer> ().color.a) / 2, alpha, maxAlpha);
    sprite = GetComponent<SpriteRenderer>();
    sprite.color = new Color (color.x, color.y, color.z, alpha);
  }

  private void Update()
  {
    float deepAlpha = Mathf.Lerp(0.4f, 0.0f, Mathf.Abs(player.position.y) / abyssStart);

    alpha = Mathf.Max(deepAlpha, Mathf.Clamp (alpha - Time.deltaTime * 0.25f / lifeSpan, 0, maxAlpha));
    sprite.color = new Color(color.x, color.y, color.z, alpha);
  }
}
