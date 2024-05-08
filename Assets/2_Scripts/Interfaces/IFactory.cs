using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Creates an object using data </summary>
/// <typeparam name="T"> The type of objects it creates </typeparam>
/// <typeparam name="U"> The type of data it needs </typeparam>
public interface IFactory<T, U>
{
	public T Create(U data);
}
