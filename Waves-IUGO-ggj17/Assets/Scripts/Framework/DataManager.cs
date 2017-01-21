using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleToAttribute("ServiceLocator")]

public class DataManager
{
  public DataManager()
  {}

  public void Init()
  {
    Debug.Log("Data Manager Init");
    LoadWhatever();
  }

  #region Ship Data
  [System.Serializable]
  public struct WhateverData
  {
    [System.Serializable]
    public struct Data
    {
      public string type;
      public float speed;
      public int life;
      public float shoot_rate;
    }
    public Data[] data;
  }
  WhateverData whatevers;

  void LoadWhatever()
  {
    TextAsset json = Resources.Load<TextAsset>("Data/Whatevers");
    whatevers = JsonUtility.FromJson<WhateverData>(json.text);
  }

  public WhateverData.Data GetCharacterOfType(string type)
  {
    foreach (var c in whatevers.data)
    {
       if (c.type.Equals(type))
      {
        return c;
      }
    }
    return new WhateverData.Data { type = "Unknown", speed = 0, life = 1, shoot_rate = 0.2f };
  }
  #endregion
}
