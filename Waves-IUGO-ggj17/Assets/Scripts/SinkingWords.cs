using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinkingWords : MonoBehaviour {

  public Text text_component;

  private Transform player_transform;

  static string instantiation_text;
  public static void add_monologue(GameObject Narrative, string text)
  {
    instantiation_text = text;
    GameObject go = Instantiate(Narrative, Vector2.zero, Quaternion.identity);
  }

	// Use this for initialization
	void Start ()
  {
    player_transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    transform.position = new Vector3(33, -12, 0) + player_transform.position;
    text_component.text = instantiation_text;
  }
	
	// Update is called once per frame
	void Update ()
  {
    transform.position += Time.deltaTime * Vector3.up;
    if (transform.position.y >= player_transform.position.y + 8)
    {
      Destroy(gameObject);
    }
  }
}
