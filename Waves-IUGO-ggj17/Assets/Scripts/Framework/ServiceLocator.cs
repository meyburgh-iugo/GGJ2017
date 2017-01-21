using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using SNS;

public class ServiceLocator : Singleton<ServiceLocator>
{
  public string SceneNameAfterLoad;

  static AudioManager sAudioManager;
  static DataManager sDataManager;
  static InputHandler sInputHandler;
  static EventSystem sEventSystem;

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

    SceneManager.LoadScene(SceneNameAfterLoad);
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
