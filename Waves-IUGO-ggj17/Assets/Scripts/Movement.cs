using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  private Rigidbody2D rb;
  private Transform t;
	public float speed = 0.2f;
  public float turnSpeed = 10.0f;
  // Use this for initialization
	void Awake ()
  {
    t = transform;
    rb = GetComponent<Rigidbody2D>();
    rb.drag = speed * 0.95f;
    rb.angularDrag = turnSpeed * 0.02f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
  {
    var vert = Input.GetAxis("Vertical");
    var hori = Input.GetAxis("Horizontal");

    rb.rotation += (-hori * Time.deltaTime * turnSpeed);
    rb.AddRelativeForce (Vector2.right * Time.deltaTime * speed * vert, ForceMode2D.Impulse);
  }
}
