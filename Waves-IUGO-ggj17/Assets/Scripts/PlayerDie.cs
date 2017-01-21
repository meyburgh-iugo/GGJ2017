using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
	// Use this for initialization
	void Start ()
  {
		
	}
	
	// Update is called once per frame
	public IEnumerator Die ()
  {
    GetComponent<Movement>().enabled = false;

    yield return new WaitForSeconds(1.0f);

    int deathKind = PlayerPrefs.GetInt("DeathCount", 0);
    switch (deathKind)
    {
      case 0:
        MessagePooler.Instance.QueueMessage("Ops, you died.");
        break;
      case 1:
        MessagePooler.Instance.QueueMessage("Even light was not safe, heh?");
        break;
      default:
        MessagePooler.Instance.QueueMessage("Deeper you go, hard is to go back");
        break;
    }

    MessagePooler.Instance.Fade.OnFinishFade += RestartLevel;
	}

  void RestartLevel()
  {
    SceneManager.LoadScene("2_Gameplay");
  }
}
