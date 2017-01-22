using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePooler : Singleton<MessagePooler>
{
  public string[][] messages;
  public Queue<string> messagesQueue;

  private MessageFade fade;
  public MessageFade Fade { get { return fade;  } }
  void Start()
  {
    messagesQueue = new Queue<string>();
    fade = GetComponent<MessageFade>();
    fade.OnFinishFade += DequeueMessage;

    messages = new string[3][];
    messages[0] = new string[1] { "Relax, you cannot die..." };
    messages[1] = new string[2] { "Darkness is not safe...", "Try light" };

    switch (Random.Range(0, 4))
    {
      case 0: messages[2] = new string[] { "My wife just dumped me...", "She wasn't pretty, though." }; break;
      case 1: messages[2] = new string[] { "Do you see that whale?", "It reminds me my wife." }; break;
      case 2: messages[2] = new string[] { "I want to be like James Cameron, you know...", "Could I find the Titanic down here?" }; break;
      case 3: messages[2] = new string[] { "And let us do that again...", "Oh schnaps, again?" }; break;
    }

    int deaths = PlayerPrefs.GetInt("StartingText", 0);
    for (int i = 0; i < messages[deaths % messages.Length].Length; i++)
    {
      messagesQueue.Enqueue(messages[deaths][i]);
    }

    if (messagesQueue.Count > 0)
    {
      fade.ShowMessage(messagesQueue.Dequeue());
    }
  }

  public void QueueMessage(string message)
  {
    messagesQueue.Enqueue(message);
    if (!fade.IsPlaying)
    {
      fade.ShowMessage(messagesQueue.Dequeue());
    }
  }
	
	// Update is called once per frame
	void DequeueMessage ()
  {
    if (messagesQueue.Count > 0)
    {
      fade.ShowMessage(messagesQueue.Dequeue());
    }
	}
}
