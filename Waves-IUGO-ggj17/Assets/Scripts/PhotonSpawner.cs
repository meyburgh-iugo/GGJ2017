using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonSpawner : MonoBehaviour
{
  public GameObject photon;
  public int photonCount = 720;
  public float slowPhotonRate = 5f;
  public float fastPhotonRate = 1f;
  public float photonLifeSpan = 1.3f;
  public float photonSpeed = 10.0f;
  public float radius = 0.2f;

  private float photonRate;
  private Transform t;
	// Use this for initialization
	void Start ()
  {
    t = transform;
    photonRate = slowPhotonRate;
    StartCoroutine("Spawn");
  }

  IEnumerator Spawn()
  {   
    while(true)
    {
      yield return new WaitForSeconds(photonRate);

      for (float i = 0; i < 360.0f; i += 360.0f / photonCount)
      {
        var go = Instantiate(photon, new Vector3(t.position.x + radius * Mathf.Cos(i * Mathf.Deg2Rad), t.position.y + radius * Mathf.Sin(i * Mathf.Deg2Rad), t.position.z), Quaternion.identity);
        var dir = go.transform.position - t.position;
        var trace = Random.Range(0.0f, 1.0f) > 0.7f;
        go.GetComponent<Photon>().Setup(dir, photonLifeSpan, photonSpeed, trace);
      }
    }
  }

  public void IncreasePhotonRate()
  {
    photonRate = fastPhotonRate;
    StopCoroutine("Spawn");
    StartCoroutine("Spawn");
  }

  public void DecreasePhotonRate()
  {
    photonRate = slowPhotonRate;
    StopCoroutine("Spawn");
    StartCoroutine("Spawn");
  }
}
