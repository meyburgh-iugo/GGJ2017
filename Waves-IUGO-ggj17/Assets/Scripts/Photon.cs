using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
  public void Setup(Vector2 dir, float lifeSpan, float speed)
  {
    GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
    Invoke("Die", lifeSpan);
  }
	
	// Update is called once per frame
	void Die()
  {
    Destroy(gameObject);
	}
}
