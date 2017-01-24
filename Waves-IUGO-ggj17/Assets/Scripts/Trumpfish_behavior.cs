﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trumpfish_behavior : MonoBehaviour
{
  public float RadiusOfView = 1;
  public float GiveUpDistance = 0.2f;
  public float speed = 0.2f;
  public float maxDepth = -60.0f;

  private Transform Player;
  private Rigidbody2D rb;
  private MessageFade messenger;
  private Animator anim;

  private string[][] speeches;
  private Queue<MessagePooler.MessagePiece> messagesQueue;
  private bool madeHisPoint;

  // Use this for initialization
  void Start()
  {
    messagesQueue = new Queue<MessagePooler.MessagePiece>();
    speeches = new string[7][];

    speeches[0] = new string[] { "I will build a net all over the sea...", "and make the shrimps pay for it"};
    speeches[1] = new string[] { "Orange is the new black."};
    speeches[2] = new string[] { "Make the sea great again!" };
    speeches[3] = new string[] { "Happy Global Game Jam to all, including to my...", " sleep that have fought me and lost so badly they just don’t know what to do. Love!" };
    speeches[4] = new string[] { "The beauty of me is that I’m very orange." };
    speeches[5] = new string[] { "It’s freezing and snowing in Vancouver – we need global warming!" };
    speeches[6] = new string[] { "You're disgusting!" };

    rb = GetComponent<Rigidbody2D>();
    Player = GameObject.FindGameObjectWithTag("Player").transform;
    rb.AddForce(new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
    anim = GetComponent<Animator>();

    messenger = GetComponentInChildren<MessageFade>();
    messenger.OnFinishFade += DequeueMessage;

    madeHisPoint = false;
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 dir = transform.position - Player.position;
    if (transform.position.y < maxDepth)
    {
      rb.velocity = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(0.05f, 0.1f));
      anim.SetFloat("Speed", rb.velocity.x);
    }
    else if (dir.magnitude < RadiusOfView && !madeHisPoint)
    {
      if (!messenger.IsPlaying)
      {
        int idx = Random.Range(0, speeches.Length);
        for (int i = 0; i < speeches[idx].Length; i++)
        {
          QueueMessage(new MessagePooler.MessagePiece { message = speeches[idx][i], fadeIn = 0.25f, time = 2.0f, fadeOut = 0.25f });
        }
      }
      rb.velocity = Vector2.zero;
      rb.AddForce(-dir.normalized * speed, ForceMode2D.Impulse);
      anim.SetFloat("Speed", rb.velocity.x);

      if (dir.magnitude < GiveUpDistance)
        madeHisPoint = true;
    }
    else if (rb.velocity.magnitude < 0.1)
    {
      rb.AddForce(new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
      anim.SetFloat("Speed", rb.velocity.x);
    }  
  }

  public void QueueMessage(MessagePooler.MessagePiece message)
  {
    messagesQueue.Enqueue(message);
    if (!messenger.IsPlaying)
    {
      messenger.ShowMessage(messagesQueue.Dequeue());
    }
  }

  // Update is called once per frame
  void DequeueMessage()
  {
    if (messagesQueue.Count > 0)
    {
      messenger.ShowMessage(messagesQueue.Dequeue());
    }
  }
}
