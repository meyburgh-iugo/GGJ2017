using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

  public Text Text;

  void Awake()
  {
    Text.text = "Best Dive: " + PlayerPrefs.GetInt("DepthScore") + "m";
  }

  public void StartGame()
  {
    SceneManager.LoadScene("2_Gameplay");
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
