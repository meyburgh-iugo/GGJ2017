using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepThoughts : MonoBehaviour
{
  public GameObject Narrative;

  float next_soliloquy = -5;
  string[] thoughts = new string[1];
  int thought_index = 0;

	// Use this for initialization
	void Start ()
  {
    thoughts[0] = "My wife left me\nthe day I brought this submarine";
  }
	
	// Update is called once per frame
	void Update ()
  {
		if (thought_index != thoughts.GetLength(0) && next_soliloquy >= transform.position.y)
    {
      print(thoughts[thought_index]);
      next_soliloquy -= 10;
      SinkingWords.add_monologue(Narrative, thoughts[thought_index]);
      ++thought_index;
    }
  }
}
