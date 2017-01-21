using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  private Rigidbody2D rb;
  private Transform t;
	public float speed = 10;
  public float turnSpeed = 100;
  // Use this for initialization
	void Awake ()
  {
    t = transform;
    rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
  {
    var vert = Input.GetAxis("Vertical");
    var hori = Input.GetAxis("Horizontal");

    rb.rotation -= hori * Time.deltaTime * speed;

    rb.velocity = new Vector2(t.right.x * speed * Time.deltaTime * vert, t.right.y * speed * Time.deltaTime * vert);
  }
}
