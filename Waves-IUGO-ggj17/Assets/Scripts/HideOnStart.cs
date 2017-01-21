using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStart : MonoBehaviour
{
  SpriteRenderer sprite;
  private float alpha = 0.0f;
  private float maxAlpha = 0.15f;
  private float lifeSpan = 1.0f;
  private Vector3 color = new Vector3(1,1,1);

	// Use this for initialization
	void Start ()
  {
    sprite = GetComponent<SpriteRenderer>();
    sprite.color = new Color (color.x, color.y, color.z, alpha);
	}

  public void OnCollisionEnter2D(Collision2D collision)
  {
    alpha = Mathf.Clamp ((alpha + collision.collider.GetComponent<SpriteRenderer> ().color.a) / 2, alpha, maxAlpha);
    sprite = GetComponent<SpriteRenderer>();
    sprite.color = new Color (color.x, color.y, color.z, alpha);
  }

  private void Update()
  {
    alpha = Mathf.Clamp (alpha - Time.deltaTime * 1.0f / lifeSpan, 0, maxAlpha);
    sprite.color = new Color(color.x, color.y, color.z, alpha);
  }
}
