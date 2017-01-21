using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
  public Transform goal;
  private Vector3 offset;
	// Use this for initialization
	void Start () {
    offset = transform.position - goal.position;
    offset.x = 0;
    offset.y = 0;
	}
	
	// Update is called once per frame
	void Update () {
    transform.position = Vector3.Lerp (transform.position, goal.position, 0.5f * Time.deltaTime) + offset;
	}
}
