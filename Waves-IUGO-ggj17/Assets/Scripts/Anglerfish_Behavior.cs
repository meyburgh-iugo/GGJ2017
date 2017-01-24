using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anglerfish_Behavior : MonoBehaviour
{
  public float RadiusOfView;
  public float speed;
  private float AttackDistance = 0.5f;

  private Transform t;
  private Transform Player;
  private Rigidbody2D rb;
  private Animator anim;

	// Use this for initialization
	void Start ()
  {
    t = transform;
    rb = GetComponent<Rigidbody2D>();
    Player = GameObject.FindGameObjectWithTag("Player").transform;
    Vector2 force = new Vector2(0,0);
    if (t.position.x < Player.position.x)
    {
      force.x = Random.Range(0.05f, 0.1f);
    }
    else
    {
      force.x = Random.Range(-0.1f, -0.05f);
    }
    rb.AddForce(force, ForceMode2D.Impulse);

    anim = GetComponent<Animator>();
  }
	
	// Update is called once per frame
	void Update ()
  {
    Vector2 dir = transform.position - Player.position;
    if (dir.magnitude < AttackDistance)
    {
      anim.SetTrigger("Bite");
    }
    else if (dir.magnitude < RadiusOfView)
    {
      rb.velocity = Vector2.zero;
      rb.AddForce(-dir.normalized * speed, ForceMode2D.Impulse);
      anim.SetFloat("Speed", rb.velocity.x);
    }
    else if (rb.velocity.magnitude < 0.1)
    {
      rb.AddForce(new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
      anim.SetFloat("Speed", rb.velocity.x);
    }
	}
}
