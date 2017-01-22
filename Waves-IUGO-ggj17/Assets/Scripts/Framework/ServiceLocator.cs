using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using SNS;

public enum EDifficulty
{
  Normal,
  Hard,
  Nightmare
}

public class ServiceLocator : Singleton<ServiceLocator>
{
  static AudioManager sAudioManager;
  static DataManager sDataManager;
  static InputHandler sInputHandler;
  static EventSystem sEventSystem;
  public static EDifficulty Difficulty;

  protected override void Init()
  {
    print("Service Locator Init");
    DontDestroyOnLoad(this);

    sAudioManager = new AudioManager();
    sAudioManager.Init();

    sDataManager = new DataManager();
    sDataManager.Init();

    sInputHandler = new InputHandler();

    sEventSystem = new EventSystem();
  }

  public static AudioManager GetAudioManager()
  {
      Debug.Assert(sAudioManager != null, "sAudioManager is null.");
      return sAudioManager;
  }

  public static DataManager GetDataManager()
  {
    Debug.Assert(sDataManager != null, "sDataManager is null.");
    return sDataManager;
  }

  public static InputHandler GetInputHandler()
  {
    Debug.Assert(sInputHandler != null, "sInputHandler is null.");
    return sInputHandler;
  }

  public static EventSystem GetEventSystem()
  {
    Debug.Assert(sEventSystem != null, "sEventSystem is null.");
    return sEventSystem;
  }
}
