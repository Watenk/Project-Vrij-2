using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Watenk
{
	
/// <summary> Keeps track of a single instance of a class </summary>
public abstract class ASingleton<T> where T : class, new()
{
	private static T instance;

	public static T Instance{
		get
		{
			if (instance == null)
			{
				instance = new T();
			}
			return instance;
		}
	}
}

}
