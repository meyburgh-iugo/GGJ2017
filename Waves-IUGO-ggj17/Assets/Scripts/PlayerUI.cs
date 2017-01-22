using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
  Transform t;
  public Text lifes;
  public Text deep;
	// Use this for initialization
	void Start ()
  {
    t = transform;
  }

  private void Update()
  {
    deep.text = "Deep: " + t.position.y.ToString("F1") + " m";
  }
  
}
