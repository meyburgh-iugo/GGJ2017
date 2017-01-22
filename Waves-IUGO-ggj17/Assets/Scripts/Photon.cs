using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
  private SpriteRenderer sprite;
  private Rigidbody2D rb;
  private CircleCollider2D col;
  private float alpha;
  private float lifeSpan;
  private Vector3 color;
  private float maxLifeSpan;

  public bool tracer = false;

  private void Awake()
  {
    color = new Vector3(1, 1, 1);
    sprite = GetComponent<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();
    col = GetComponent<CircleCollider2D> ();
    sprite.color = new Color(color.x, color.y, color.z, alpha);
  }

  private void Update()
  {
    alpha -= Time.deltaTime * 1.0f/lifeSpan;
    sprite.color = new Color(color.x, color.y, color.z, alpha);

    lifeSpan -= Time.deltaTime;
    maxLifeSpan -= Time.deltaTime;
    if (lifeSpan <= 0 || maxLifeSpan <= 0)
    {
      Destroy(gameObject);
    }
  }

  public void Setup(Vector2 dir, float _lifeSpan, float speed, bool _tracer)
  {
    lifeSpan = _lifeSpan;
    maxLifeSpan = 3 * lifeSpan;
    rb.velocity = dir.normalized * speed;
    tracer = _tracer;
    alpha = 1.0f;
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.CompareTag("Obstacle"))
    {
      ServiceLocator.GetAudioManager().Register(AudioManager.Clips.SONAR);
    }

    if (tracer)
    {
      color = new Vector3(0, 0.6f, 0);
      rb.velocity = Vector2.zero; // -collision.relativeVelocity.normalized;
      rb.position = collision.contacts[0].point + (collision.contacts[0].normal * 0.01f);
      col.enabled = false;
      lifeSpan = Mathf.Clamp(lifeSpan + collision.relativeVelocity.magnitude, 0, 2);
    }
  }
}
