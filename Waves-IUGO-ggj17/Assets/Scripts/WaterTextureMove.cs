using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTextureMove : MonoBehaviour
{
  private Transform t;
  private Material m;
  public float parallaxAmount = 1f;
	// Use this for initialization
	void Start ()
  {
    t = GameObject.FindGameObjectWithTag("Player").transform;
    m = GetComponent<SpriteRenderer>().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update ()
  {
    m.SetTextureOffset("_MainTex", new Vector2(t.position.x/parallaxAmount, t.position.y/parallaxAmount));
	}
}
