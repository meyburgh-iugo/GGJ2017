using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDie : MonoBehaviour
{
  private bool isDead = false;
  public bool IsDead { get { return isDead;  } }


  GameObject GameUI;
  Text DeepestText;
  Text DeepText;
  // Use this for initialization
  void Start ()
  {
    DeepestText = GameObject.Find("Deepest").GetComponent<Text>();
    DeepestText.text = "Deepest: " + PlayerPrefs.GetInt("Deepest") + "m";

    DeepText = GameObject.Find("Deep").GetComponent<Text>();
  }

  // Update is called once per frame
  public IEnumerator Die (int deathKind)
  {
    isDead = true;

    if(transform.position.y < PlayerPrefs.GetInt("Deepest", 0))
    {
      PlayerPrefs.SetInt("Deepest", (int)transform.position.y);
      DeepestText.text = "Deepest: " + PlayerPrefs.GetInt("Deepest", 0) + "m";
      DeepText.enabled = false;
    }
    else
    {
      DeepText.enabled = true;
    }

    GetComponent<Movement>().enabled = false;

    if (PlayerPrefs.GetInt("Lives", 0) == -10)
    {
      MessagePooler.Instance.QueueMessage("You are doing great!");
      yield return new WaitForSeconds(8.0f);
    }

    yield return new WaitForSeconds(1.0f);

    switch (deathKind)
    {
      case 0:
        MessagePooler.Instance.QueueMessage("Oops, you died.");
        PlayerPrefs.SetInt("StartingText", 1);
        break;
      case 1:
        MessagePooler.Instance.QueueMessage("Died again. Even the light was not safe, heh?");
        PlayerPrefs.SetInt("StartingText", 2);
        break;
      default:
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
          if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.QueueMessage("Deeper you go, harder it is to go back.");
          }
          else
          {
            MessagePooler.Instance.QueueMessage("Keep calm, it is all about the journey.");
          }
        }
        else
        {
          if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.QueueMessage("Oh My Gosh! We got a leak.");
          }
          else
          {
            MessagePooler.Instance.QueueMessage("Try one more time, buddy.");
          }
        }
        
        break;
    }

    MessagePooler.Instance.Fade.OnFinishFade += GameOver;
	}

  void GameOver()
  {
    StartCoroutine("DelayRestart");
  }

  void RestartLevel()
  {
    SceneManager.LoadScene("2_Gameplay");
  }

  public IEnumerator DelayRestart()
  {
    yield return new WaitForSeconds(3);
    RestartLevel();
  }
}
