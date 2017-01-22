using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePooler : Singleton<MessagePooler>
{
  public struct MessagePiece
  {
    public string message;
    public float fadeIn;
    public float fadeOut;
    public float time;
  }

  public string[][] messages;
  public Queue<MessagePiece> messagesQueue;

  private MessageFade fade;
  public MessageFade Fade { get { return fade;  } }
  void Start()
  {
    messagesQueue = new Queue<MessagePiece>();
    fade = GetComponent<MessageFade>();
    fade.OnFinishFade += DequeueMessage;

    messages = new string[3][];
    messages[0] = new string[1] { "Relax, you cannot die..." };
    messages[1] = new string[2] { "Darkness is not safe...", "Try light" };

    switch (Random.Range(0, 7))
    {
      case 0: messages[2] = new string[] { "My wife just dumped me...", "She wasn't pretty, though." }; break;
      case 1: messages[2] = new string[] { "Do you see that whale?", "It reminds me my wife." }; break;
      case 2: messages[2] = new string[] { "I want to be like James Cameron, you know...", "Could I find the Titanic down here?" }; break;
      case 3: messages[2] = new string[] { "And let us do that again...", "Oh schnaps, again?" }; break;
      case 4: messages[2] = new string[] { "Yeah, I have father issues."}; break;
      case 5: messages[2] = new string[] { "As Quorthon used to say...", "It is a fine day to die." }; break;
      case 6: messages[2] = new string[] { "Let's do it, player. Yahooooooo" }; break;
    }

    int deaths = PlayerPrefs.GetInt("StartingText", 0);
    float fIn = 1.5f;
    float fOut = 0.5f;
    for (int i = 0; i < messages[deaths % messages.Length].Length; i++)
    {
      messagesQueue.Enqueue(new MessagePooler.MessagePiece{message = messages[deaths][i], fadeIn = fIn, time = 3f, fadeOut = fOut});
      fIn = 0.5f;
    }

    if (messagesQueue.Count > 0)
    {
      fade.ShowMessage(messagesQueue.Dequeue());
    }
  }

  public void QueueMessage(MessagePiece message)
  {
    messagesQueue.Enqueue(message);
    if (!fade.IsPlaying)
    {
      fade.ShowMessage(messagesQueue.Dequeue());
    }
  }

  public void ClearAndQueueMessage(MessagePiece message)
  {
    messagesQueue.Clear();
    QueueMessage(message);
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
