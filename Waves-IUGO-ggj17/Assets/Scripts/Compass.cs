using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
  private Transform t;
  public Rigidbody2D player;
  public Transform goal;
	// Use this for initialization
	void Awake ()
  {
    t = transform;
	}
	
	// Update is called once per frame
	void Update ()
  {
    t.localRotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(goal.position.y - player.position.y, goal.position.x - player.position.x));
  }
}
