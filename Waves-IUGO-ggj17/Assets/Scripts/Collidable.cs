using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collidable : MonoBehaviour
{
  public enum Kind{
    player,
    obstacle,
    light
  }
  public Kind kind;

  // Use this for initialization
  void Awake ()
  {
  }

  // Update is called once per frame
  public void OnCollisionEnter2D(Collision2D collision)
  { 
    switch (kind)
    {
      case Kind.obstacle:
      {
          if (collision.gameObject.CompareTag("Player"))
          {
            GetComponent<Collider>().enabled = false;
            StartCoroutine(collision.gameObject.GetComponent<PlayerDie>().Die(PlayerPrefs.GetInt("StartingText",1) < 2 ? 0 : 2));
          }
          break;
      }
      case Kind.light:
      {
          if (collision.gameObject.CompareTag("Player"))
          {
            GetComponent<Collider>().enabled = false;
            StartCoroutine(collision.gameObject.GetComponent<PlayerDie>().Die(1));
          }
          break;
      }
    }
  }
}
