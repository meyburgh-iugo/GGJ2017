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

  public void SetEffect(PlayerEffect NewEffect)
  {
    Effect = NewEffect;
  }

  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PlayerDie>().IsDead)
    {
      PlayerEffectSlot slot = other.gameObject.GetComponent<PlayerEffectSlot>();
      if(slot != null && slot.Empty())
      {
        slot.Fill(Effect);
        Destroy(gameObject);
        return;
      }
    }
  }
}
