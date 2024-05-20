using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Creates an object </summary>
/// <typeparam name="T"> The type of objects it constructs </typeparam>
public interface IFactory<T>
{
	public T Construct(params object[] parameters);
}
