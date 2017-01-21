using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePooler : Singleton<MonoBehaviour>
{
  public string[] messages;
  public float messageLong;
  public Queue<string> messagesQueue;

  private MessageFade fade;

  private void Awake()
  {
    messagesQueue = new Queue<string>();
  }

  void Start ()
  {
    fade = GetComponent<MessageFade>();
    fade.OnFinishFade += UnstackMessage;

		for (int i = 0; i < messages.Length; i++)
    {
      messagesQueue.Enqueue(messages[i]);
    }

    if (messagesQueue.Count > 0)
    {
      fade.ShowMessage(messagesQueue.Dequeue());
    }
  }

  void StackMessage(string message)
  {
    messagesQueue.Enqueue(message);
    if (!fade.IsPlaying)
    {
      fade.ShowMessage(messagesQueue.Dequeue());
    }
  }
	
	// Update is called once per frame
	void UnstackMessage ()
  {
    var message = messagesQueue.Dequeue();
    if (message != null)
    {
      fade.ShowMessage(message);
    }
	}
}
