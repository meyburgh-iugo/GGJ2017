using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonSpawner : MonoBehaviour
{
  public GameObject photon;
  public int photonCount = 720;
  public float photonRate = 1.0f;
  public float photonLifeSpan = 1.3f;
  public float photonSpeed = 10.0f;
  public float radius = 0.2f;

  private Transform t;
	// Use this for initialization
	void Start ()
  {
    t = transform;
    InvokeRepeating("Spawn", 1.0f, photonRate);
  }

  void Spawn()
  {
    for(float i = 0; i < 360.0f; i+= 360.0f/photonCount)
    {
      var go = Instantiate(photon, new Vector3(t.position.x + radius * Mathf.Cos(i * Mathf.Deg2Rad), t.position.y + radius * Mathf.Sin(i * Mathf.Deg2Rad), t.position.z), Quaternion.identity);
      var dir = go.transform.position - t.position;
      go.GetComponent<Photon>().Setup(dir, photonLifeSpan, photonSpeed);

    }
  }
}
