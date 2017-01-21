using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
  public Text lifes;
	// Use this for initialization
	void Start ()
  {
    lifes.text = "Lives: " + PlayerPrefs.GetInt("Lives", 0);
  }
	
}
