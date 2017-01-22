using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish_Behavior : MonoBehaviour
{
  public float RadiusOfView = 10;
  public float speed = 5;

  private Transform t;
  private Transform Player;
  private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
  {
    t = transform;
    rb = GetComponent<Rigidbody2D>();
    Player = GameObject.FindGameObjectWithTag("Player").transform;
    rb.AddForce(new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
  }
	
	// Update is called once per frame
	void Update ()
  {
    Vector2 dir = transform.position - Player.position;
    if (dir.magnitude < RadiusOfView)
    {
      rb.velocity = Vector2.zero;
      rb.AddForce(-dir.normalized * speed, ForceMode2D.Impulse);
    }
    else if (rb.velocity.magnitude < 0.1)
    {
      rb.AddForce(new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
    }
	}
}
