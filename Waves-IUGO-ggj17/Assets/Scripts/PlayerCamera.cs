﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
  private GameObject player;
  private Rigidbody2D playerBody;
  private float abyssStart = 100.0f;

  private float target_z = -10.0f;

  private float smoothSpeed = 0.2f;
  private float initOrtho = 0.5f;
  private float maxOrtho = 5.0f;

  private Camera cam;
	// Use this for initialization
	void Start () {
    player = GameObject.FindGameObjectWithTag("Player");
    playerBody = player.GetComponent<Rigidbody2D>();
    
    cam = GetComponent<Camera>();
    cam.transform.position = playerBody.transform.position;
    cam.orthographicSize = initOrtho;
	}
	
	// Update is called once per frame
	void LateUpdate () {
    Vector3 pos = new Vector3(Mathf.Lerp(transform.position.x, playerBody.position.x, 0.5f * Time.deltaTime), Mathf.Lerp(transform.position.y, playerBody.position.y, Time.deltaTime), target_z);
    transform.position = pos;

    bool playerIsDead = player.GetComponent<PlayerDie>().IsDead;

    float targetOrtho = playerIsDead ? initOrtho : 8 * (1 / (1 + Mathf.Exp(-playerBody.velocity.magnitude + 1.0f)));

    targetOrtho = Mathf.Clamp(targetOrtho, initOrtho, maxOrtho);
    cam.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, (playerIsDead ? 2.25f : 1.0f) * smoothSpeed * Time.deltaTime);

    float time = -playerBody.position.y / abyssStart; 
    cam.backgroundColor = new Color(0, Mathf.Lerp(0.44f, 0.0f, time), Mathf.Lerp(0.50f, 0.0f, time));
  }
}
