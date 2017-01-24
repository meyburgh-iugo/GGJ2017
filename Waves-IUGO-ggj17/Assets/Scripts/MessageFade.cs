using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFade : MonoBehaviour
{
  public event Action OnFinishFade;
  private float ShowTime = 1f;
  public Text text;

  private MessagePooler.MessagePiece message;
  private bool isPlaying = false;
  public bool IsPlaying { get { return isPlaying;  } }

  public void Awake()
  {
    var c = text.color;
    text.color = new Color(c.r, c.g, c.b, 0);
  }

  public void ShowMessage(MessagePooler.MessagePiece _message)
  {
    message = _message;
    StartCoroutine(FadeIn());
  }

  IEnumerator FadeIn()
  {
    isPlaying = true;
    text.text = message.message;
    float fade = 0.01f;
    Color c = text.color;
    while (fade <= message.fadeIn)
    {
      text.color = new Color(c.r, c.g, c.b, fade / message.fadeIn);
      fade += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }

    yield return new WaitForSeconds(message.time);

    fade = message.fadeOut;
    while (fade > 0)
    {
      text.color = new Color(c.r, c.g, c.b, fade / message.fadeOut);
      fade -= Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }
    text.color = new Color(c.r, c.g, c.b, 0);

    yield return new WaitForSeconds(ShowTime);
    isPlaying = false;
    if (OnFinishFade != null)
    {
      OnFinishFade();
    }
  }
}
