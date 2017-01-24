using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OtherShipController : MonoBehaviour {

	public float turnSpeed;
	public float thrust;

	private Rigidbody2D rb;
	//private Collider collider;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		//collider = GetComponent<Collider>();
	}

	void FixedUpdate ()
	{
		float turnInput = Input.GetAxis ("Horizontal");
		float thrustInput = Input.GetAxis ("Vertical");

		float thrustForce = 0;

		if (thrustInput > 0)
		{
			thrustForce = thrustInput * thrust;
		}

		if(Mathf.Abs(turnInput) > 0.01)
			rb.rotation -= (turnSpeed * turnInput);
	
		rb.AddRelativeForce(new Vector2 (0.0f, thrustForce));
	}
}