using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDie : MonoBehaviour
{

  public GameObject DeathEffectPrefab;

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
    DeepestText.text = "deepest: " + (Mathf.Abs(PlayerPrefs.GetInt("Deepest", 0))).ToString() + " m";
    DeepText = GameObject.Find("Deep").GetComponent<Text>();
    NewRecord = GameObject.Find("NewRecord").GetComponent<Text>();
    NewRecord.enabled = false;
  }

  // Update is called once per frame
  public IEnumerator Die (int deathKind)
  {
    isDead = true;

    ServiceLocator.GetAudioManager().Register(AudioManager.Clips.EXPLOSION, 1);

    if (transform.position.y < PlayerPrefs.GetInt("Deepest", 0))
    {
      PlayerPrefs.SetInt("Deepest", (int)transform.position.y);
      DeepestText.text = (Mathf.Abs(PlayerPrefs.GetInt("Deepest", 0))).ToString();
      DeepText.enabled = false;
      NewRecord.enabled = true;
    }
    else
    {
      DeepText.enabled = false;
    }

    GetComponent<Movement>().enabled = false;
    GetComponent<Animator>().SetTrigger("Death");

    yield return new WaitForSeconds(1.0f);

    float fIn = 0.5f;
    float fOut = 3.0f;
    switch (deathKind)
    {
      case 1:
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
          MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "Even the light was not safe, heh?", fadeIn = fIn, time = 3f, fadeOut = fOut });
        }
        else
        {
          MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "Your greedy pitty. I died!", fadeIn = fIn, time = 2f, fadeOut = fOut });
        }
        break;
      default:
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
          if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "Oops, I died.", fadeIn = fIn, time = 2f, fadeOut = fOut });
          }
          else if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "Deeper you go, harder it is to go back.", fadeIn = fIn, time = 2f, fadeOut = fOut });
          }
          else
          {
            MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "Hey player, thank you for letting me die down here.", fadeIn = fIn, time = 2f, fadeOut = fOut });
          }
        }
        else
        {
          if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "Oh My Gosh! We got a leak!", fadeIn = fIn, time = 3f, fadeOut = fOut });
          }
          else if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "Try one more time, buddy.", fadeIn = fIn, time = 3f, fadeOut = fOut });
          }
          else if (Random.Range(0.0f, 1.0f) < 0.5f)
          {
            MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "It looks like I am dying?...", fadeIn = fIn, time = 3f, fadeOut = fOut });
            MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "Nah, I was dead inside long time ago.", fadeIn = fIn, time = 3f, fadeOut = fOut });
          }
          else
          {
            MessagePooler.Instance.ClearAndQueueMessage(new MessagePooler.MessagePiece { message = "Go home, you're drunk.", fadeIn = fIn, time = 3f, fadeOut = fOut });
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

  private bool explode = false;
  void OnFrontDeathAnimation()
  {
    explode = true;
  }

  public IEnumerator DelayRestart()
  {
    if(explode)
    {
      yield return new WaitForSeconds(2);
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      gameObject.GetComponentInChildren<ParticleSystem>().Stop();
      GameObject expolsionEffect = Instantiate(DeathEffectPrefab, gameObject.transform.position, Quaternion.identity);
    }
   
    yield return new WaitForSeconds(2);
    NewRecord.enabled = false;
    RestartLevel();
  }
}
