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

    switch (Random.Range(0, 3))
    {
      case 0: messages[2] = new string[3] { "My wife just dumped me...", "Because I purcharsed that submarine", "She wasn't pretty, though." }; break;
      case 1: messages[2] = new string[2] { "Do you see that whale?", "It remembers my ex wife." }; break;
      case 3: messages[2] = new string[2] { "I want to be like James Cameron...", "Could I find Titanic down here?" }; break;
    }

    int deaths = PlayerPrefs.GetInt("StartingText", 0);
		for (int i = 0; i < messages[deaths].Length; i++)
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
