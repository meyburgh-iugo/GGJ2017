using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance;

	public static T Instance
	{
		get
		{
			if(!instance)
			{
				instance = (T) FindObjectOfType(typeof(T));

				if(!instance)
				{
					Debug.LogWarning("An instance of " + typeof(T) + 
						" is needed in the scene, but there is none.");
				}
			}

			return instance;
		}
	}

  // DO NOT override
	protected void Awake()
	{
		if(Instance != this)
		{
			print ("Destroying Singleton of " + typeof(T).ToString() + " because already there is an instance in the scene");
			Destroy(this.gameObject);
		}
		else
		{
			Init();	
		}
	}

  // Put the initialization of Singleton class here.
  // DO NOT override Awake method.
	protected virtual void Init(){}
}