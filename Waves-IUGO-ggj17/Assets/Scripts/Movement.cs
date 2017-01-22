﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  private Animator anim;
  private Rigidbody2D rb;
  private Transform t;
	public float speed = 0.2f;
  public float turnSpeed = 10.0f;
  // Use this for initialization
	void Awake ()
  {
    anim = GetComponent<Animator>();
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

    anim.SetFloat("Speed", hori);

    rb.AddRelativeForce (new Vector2(hori * Time.deltaTime * speed, Time.deltaTime * speed * vert), ForceMode2D.Impulse);
  }
}
