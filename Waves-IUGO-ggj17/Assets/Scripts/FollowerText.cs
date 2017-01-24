using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerText : MonoBehaviour
{
  private RectTransform follower;

  public Vector2 offset = new Vector2(0.5f, -50.0f);
  public Transform followed;
  private Camera cam;

  // Use this for initialization
  void Start ()
  {
    cam = Camera.main;
    follower = GetComponent<RectTransform>();
  }
	
	// Update is called once per frame
	void Update ()
  {
    Vector2 pos = cam.WorldToScreenPoint(followed.position);
    follower.position = pos;	
	}
}
