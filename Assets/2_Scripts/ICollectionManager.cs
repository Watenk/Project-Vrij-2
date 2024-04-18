using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A manager that stores objects of a certain type whitch you can add, remove and get. </summary>
/// <typeparam name="T"> The type of object the manager stores </typeparam>
/// <typeparam name="U"> The data type to add a object to the manager. </typeparam>
/// <typeparam name="V"> The type with which to get a object from the manager. </typeparam>
public interface ICollectionManager<T, U, V>
{
	public void Add(U data);
	public void Remove(T instance);
	public void Remove(V getter);
	public T Get(V getter);
	public int GetSize();
}