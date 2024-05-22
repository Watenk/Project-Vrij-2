using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Creates an object </summary>
/// <typeparam name="T"> The type of objects it constructs </typeparam>
/// <typeparam name="U"> The data it needs to construct it </typeparam>
public interface IFactory<T, U>
{
	public T Construct(U data);
	public void Deconstruct(T instance);
}
