using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish_boss : MonoBehaviour
{
  public GameObject prefab_jellyfish;
  private Transform player;
  private bool casted = false;

  private List<GameObject> jellyfishes;

  // Use this for initialization
	void Start ()
  {
    jellyfishes = new List<GameObject>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();	
	}
	
	// Update is called once per frame
	void Update ()
  {
		if (player.position.y <= -66.6 && !casted)
    {
      casted = true;
      MessagePooler.Instance.QueueMessage("Wait a minute...");
      MessagePooler.Instance.QueueMessage("Those jellyfishes got 20% more glow and are red?!");
      MessagePooler.Instance.QueueMessage("Bad things are coming...");

      for (int i = 0; i < 4; i++)
      {
        SpawnerBossjellyfish();
      }
    }
    else if (player.position.y <= -86.6 && casted)
    {
      for(int i = 0; i < jellyfishes.Count; i++)
      {
        var rb = jellyfishes[i].GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-0.01f, 0.01f), 0.1f), ForceMode2D.Impulse);

        var jb = jellyfishes[i].GetComponent<Jellyfish_Behavior>();
        jb.RadiusOfView = 10;
        jb.speed = 1;
      }
    }
	}

  void SpawnerBossjellyfish()
  {
    var go = Instantiate(prefab_jellyfish, new Vector2(player.position.x + Random.Range(-15, 15), player.position.y - 15), Quaternion.identity);
    float slc = Random.Range(1.5f, 4f);
    go.transform.localScale = new Vector3(slc, slc, 1);
    go.transform.parent = transform;

    var rb = go.GetComponent<Rigidbody2D>();
    rb.AddForce(new Vector2(Random.Range(-0.01f, 0.01f), -0.01f), ForceMode2D.Impulse);

    var sr = go.GetComponent<SpriteRenderer>();
    sr.color = Color.red;

    var jb = go.GetComponent<Jellyfish_Behavior>();
    jb.RadiusOfView = 15;
    jb.speed = 0.25f;

    var anim = go.GetComponent<Animator>();
    anim.speed = 1.2f;

    jellyfishes.Add(go);
  }
}
