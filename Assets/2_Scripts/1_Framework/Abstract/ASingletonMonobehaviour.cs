using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Watenk
{

/// <summary> Keeps track of a single instance of a class and is a monobehaviour </summary>
public abstract class ASingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	public static T Instance{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();

				if (instance == null)
				{
					GameObject singletonObject = new GameObject(typeof(T).Name);
					instance = singletonObject.AddComponent<T>();
				}
			}
			return instance;
		}
	}

	protected virtual void OnApplicationQuit()
	{
		instance = null;
	}
}

}