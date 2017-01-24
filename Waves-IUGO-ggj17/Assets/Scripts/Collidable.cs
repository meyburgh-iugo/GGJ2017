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
          if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerDie>().IsDead)
          {
            var is_anglerfish = GetComponent<Anglerfish_Behavior>();
            int death_kind = 0;
            if (is_anglerfish != null)
            {
              death_kind = 1;
            }

            StartCoroutine(collision.gameObject.GetComponent<PlayerDie>().Die(death_kind));
          }
          break;
      }
    }
  }
}
