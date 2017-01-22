using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
  private SpriteRenderer sprite;
  private Rigidbody2D rb;
  private float lifeSpan;
  private float maxLifeSpan;

  public GameObject trace;

  private void Awake()
  {
    sprite = GetComponent<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();
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

  public void Setup(Vector2 dir, float _lifeSpan, float speed, bool _tracer)
  {
    lifeSpan = 0;
    maxLifeSpan = _lifeSpan;
    rb.velocity = dir.normalized * speed;
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.gameObject.CompareTag("Obstacle"))
    {
      AudioManager audio = ServiceLocator.GetAudioManager (); 
      if (audio != null) 
      {

        audio.Register (AudioManager.Clips.SONAR); 
      }

    } 
   
    var position = collision.contacts[0].point + (collision.contacts[0].normal * 0.01f);
    var go = Instantiate(trace, position, Quaternion.identity);
    go.GetComponent<Trace>().Setup(maxLifeSpan, lifeSpan);
  } 
}
