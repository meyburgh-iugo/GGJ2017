using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipController : MonoBehaviour {
  Rigidbody2D rb;
  readonly float force_multiplier = 20;

  // Use this for initialization
  void Start () {
    rb = GetComponent<Rigidbody2D>();
	}
	
  void FixedUpdate()
  {
    rb.AddRelativeForce(new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")).normalized * force_multiplier);
    
  }

	// Update is called once per frame
	void Update () {
		
	}
}
