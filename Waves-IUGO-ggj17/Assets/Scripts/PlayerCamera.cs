using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
  public Transform goal;
  private Vector3 offset;
  private Camera cam;
	// Use this for initialization
	void Start () {
    cam = GetComponent<Camera>();
    offset = transform.position - goal.position;
    offset.x = 0;
    offset.y = 0;

    
	}
	
	// Update is called once per frame
	void LateUpdate () {
    Vector3 pos = new Vector3(Mathf.Lerp (transform.position.x, goal.position.x, 0.5f * Time.deltaTime), Mathf.Lerp (transform.position.y, goal.position.y, 0.5f * Time.deltaTime), transform.position.z);
    transform.position = pos;

    float time = Mathf.Abs(goal.position.y) / 50.0f; 
    cam.backgroundColor = new Color(0, Mathf.Lerp(0.44f, 0.0f, time), Mathf.Lerp(0.50f, 0.0f, time));

  }
}
