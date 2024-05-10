using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A manager that stores objects of a certain type which you can add, remove and get. </summary>
/// <typeparam name="T"> Type it stores </typeparam>
/// <typeparam name="U"> Add Type </typeparam>
public interface ICollectionManager<T, U>
{
	public uint Add(U data);
	public void Remove(T instance);
	public void Remove(uint getter);
	public T Get(uint getter);
	public int GetCount();
	public void Clear();
}