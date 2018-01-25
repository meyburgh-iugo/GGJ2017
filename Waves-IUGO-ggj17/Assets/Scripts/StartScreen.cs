using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

  Text Text;
  Camera CanvasCamera;
  WaterDistortion WaterEffect;
  bool ButtonClicked = false;
  float ExitTimer = 1.0f;

  void Awake()
  {
    Text = transform.Find("Score").GetComponent<Text>();
    Text.text = string.Format("Deepest distances:\nNormal: {0} m\nHard: {1} m\nNightmare: {2} m", PlayerPrefs.GetInt("Deepest_Normal"), PlayerPrefs.GetInt("Deepest_Hard"), PlayerPrefs.GetInt("Deepest_Nightmare"));

    CanvasCamera = gameObject.GetComponent<Canvas>().worldCamera;
    WaterEffect = CanvasCamera.GetComponent<WaterDistortion>();
  }

  void LateUpdate()
  {
    if(ButtonClicked)
    {
      ExitTimer -= Time.deltaTime;
      //CanvasCamera.transform.Translate(0.0f, 0.0f, -1.0f);
      WaterEffect._Distortion += 1.0f;
      //WaterEffect._Waves += 1.0f;
      WaterEffect._Speed -= 0.5f;
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
