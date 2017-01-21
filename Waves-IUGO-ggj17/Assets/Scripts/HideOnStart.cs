using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStart : MonoBehaviour
{
  SpriteRenderer sprite;
	// Use this for initialization
	void Start ()
  {
    sprite = GetComponent<SpriteRenderer>();
    sprite.enabled = false;
		
	}
}
