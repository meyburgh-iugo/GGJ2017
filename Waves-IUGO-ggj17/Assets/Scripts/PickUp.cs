using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour
{
  private PlayerEffect Effect;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetEffect(PlayerEffect Effect)
  {

  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerDie>().IsDead)
    {
      PlayerEffectSlot slot = GetComponentInParent<PlayerEffectSlot>();
      if(slot.Empty())
      {
        slot.Fill(Effect);
        Destroy(gameObject);
        return;
      }
    }
  }
}
