using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
  public Transform goal;
  public float abyssStart = 50.0f;
  private float target_z = -10.0f;

  private float smoothSpeed = 0.2f;
  private float initOrtho = 0.5f;
  private float targetOrtho = 5.0f;

  private Camera cam;
	// Use this for initialization
	void Start () {
    cam = GetComponent<Camera>();
    cam.transform.position = goal.transform.position;
    cam.orthographicSize = initOrtho;
	}
	
	// Update is called once per frame
	void LateUpdate () {
    Vector3 pos = new Vector3(Mathf.Lerp(transform.position.x, goal.position.x, 0.5f * Time.deltaTime), Mathf.Lerp(transform.position.y, goal.position.y, 0.5f * Time.deltaTime), target_z);
    transform.position = pos;

    cam.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

    float time = -goal.position.y / abyssStart; 
    cam.backgroundColor = new Color(0, Mathf.Lerp(0.44f, 0.0f, time), Mathf.Lerp(0.50f, 0.0f, time));
  }
}
