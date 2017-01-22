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
  Text NewRecord;
  // Use this for initialization
  void Start ()
  {
    DeepestText = GameObject.Find("Deepest").GetComponent<Text>();
    DeepestText.text = "Deepest: " + PlayerPrefs.GetInt("Deepest") + "m";
    DeepText = GameObject.Find("Deep").GetComponent<Text>();
    NewRecord = GameObject.Find("NewRecord").GetComponent<Text>();
    NewRecord.enabled = false;
  }

  // Update is called once per frame
  public IEnumerator Die (int deathKind)
  {
    isDead = true;

    ServiceLocator.GetAudioManager().Register(AudioManager.Clips.EXPLOSION);

    if (transform.position.y < PlayerPrefs.GetInt("Deepest", 0))
    {
      PlayerPrefs.SetInt("Deepest", (int)transform.position.y);
      DeepestText.text = "Deepest: " + PlayerPrefs.GetInt("Deepest", 0) + "m";
      DeepText.enabled = false;
      NewRecord.enabled = true;
    }
    else
    {
      DeepText.enabled = true;
    }

    GetComponent<Movement>().enabled = false;

    yield return new WaitForSeconds(1.0f);

    switch (deathKind)
    {
      case 1:
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
          MessagePooler.Instance.QueueMessage("Even the light was not safe, heh?");
        }
        else
        {
          MessagePooler.Instance.QueueMessage("Your greedy pitty. I died!");
        }
        break;
      default:
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
          if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.QueueMessage("Oops, I died.");
          }
          else if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.QueueMessage("Deeper you go, harder it is to go back.");
          }
          else
          {
            MessagePooler.Instance.QueueMessage("Hey player, thank you for letting me die down here.");
          }
        }
        else
        {
          if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.QueueMessage("Oh My Gosh! We got a leak!");
          }
          else if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.QueueMessage("Try one more time, buddy.");
          }
          else if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.QueueMessage("It looks like I am dying?...");
            MessagePooler.Instance.QueueMessage("Nah, I was dead inside long time ago.");
          }
          else
          {
            MessagePooler.Instance.QueueMessage("Go home, you're drunk.");
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
    NewRecord.enabled = false;
    RestartLevel();
  }
}
