using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

  Text Text;
  WaterDistortion WaterEffect;
  bool ButtonClicked = false;
  float ExitTimer = 1.0f;

  void Awake()
  {
    Text = transform.FindChild("Score").GetComponent<Text>();
    Text.text = "Deepest: " + PlayerPrefs.GetInt("Deepest") + "m";

    WaterEffect = gameObject.GetComponent<Canvas>().worldCamera.GetComponent<WaterDistortion>();
  }

  private void Update()
  {
    if(ButtonClicked)
    {
      ExitTimer -= Time.deltaTime;
      WaterEffect._Distortion += 0.5f;
      if (ExitTimer < 0.0f)
      {
        SceneManager.LoadScene("2_Gameplay");
      }
    }
  }

  public void StartNormalGame()
  {
    ServiceLocator.Difficulty = EDifficulty.Normal;
    ButtonClicked = true;
  }

  public void StartHardGame()
  {
    ServiceLocator.Difficulty = EDifficulty.Hard;
    ButtonClicked = true;
  }

  public void StartNightmareGame()
  {
    ServiceLocator.Difficulty = EDifficulty.Nightmare;
    ButtonClicked = true;
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
