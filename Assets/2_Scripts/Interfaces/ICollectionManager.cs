using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A manager that stores objects of a certain type which you can add, remove and get. </summary>
/// <typeparam name="T"> Type it stores </typeparam>
/// <typeparam name="U"> Add Type </typeparam>
/// <typeparam name="V"> ID Type </typeparam>
public interface ICollectionManager<T, U, V>
{
	public V Add(U data);
	public void Remove(T instance);
	public void Remove(V getter);
	public T Get(V getter);
	public int GetCount();
}