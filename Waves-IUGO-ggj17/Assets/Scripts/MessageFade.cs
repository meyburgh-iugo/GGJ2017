using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFade : MonoBehaviour
{
  public event Action OnFinishFade;
  public float FadeInTime;
  public float ShowTime;
  public float FadeOutTime;
  public Text text;

  private string message;
  private bool isPlaying = false;
  public bool IsPlaying { get { return isPlaying;  } }

  public void Awake()
  {
    var c = text.color;
    text.color = new Color(c.r, c.g, c.b, 0);
  }

  public void ShowMessage(string _message)
  {
    message = _message;
    StartCoroutine(FadeIn());
  }

  IEnumerator FadeIn()
  {
    isPlaying = true;
    text.text = message;
    float fade = 0.01f;
    Color c = text.color;
    while (fade <= FadeInTime)
    {
      text.color = new Color(c.r, c.g, c.b, fade / FadeInTime);
      fade += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }

    yield return new WaitForSeconds(ShowTime);

    fade = FadeOutTime;
    while (fade > 0)
    {
      text.color = new Color(c.r, c.g, c.b, fade / FadeOutTime);
      fade -= Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }

    yield return new WaitForSeconds(ShowTime);
    isPlaying = false;
    if (OnFinishFade != null)
    {
      OnFinishFade();
    }
  }
}
