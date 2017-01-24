using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float speed = 0.2f;
  public float turnSpeed = 10.0f;
  public GameObject bubbles;
  public float bubblePeriod = 0.1f;

  private Animator anim;
  private Rigidbody2D rb;
  private ParticleSystem bubbleParticles;
  private float sinceLastBubble;

  // Use this for initialization
	void Awake ()
  {
    anim = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    bubbleParticles = bubbles.GetComponent<ParticleSystem> ();
    sinceLastBubble = 0f;

    rb.drag = speed * 0.9f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
  {
    var vert = Input.GetAxis("Vertical");

    // don't let the player go 'above' water level
    if (rb.position.y > 0.1)
    {
      vert = -Mathf.Abs(vert);
    }

    var hori = Input.GetAxis("Horizontal");

    anim.SetFloat("Speed", hori);

    if (Mathf.Abs (hori) > 0.1 || Mathf.Abs (vert) > 0.1) {

      var bubbleCount = Mathf.FloorToInt ((sinceLastBubble + Time.deltaTime) / bubblePeriod);
      sinceLastBubble += (Time.deltaTime - bubblePeriod * bubbleCount);

      if(bubbleCount > 0)
        bubbleParticles.Emit (bubbleCount);
    } else {
      sinceLastBubble = 0;
    }

    rb.AddRelativeForce (new Vector2(hori * Time.deltaTime * speed, Time.deltaTime * speed * vert), ForceMode2D.Impulse);
  }
}
