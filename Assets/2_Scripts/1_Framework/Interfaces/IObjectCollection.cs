using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A manager that stores objects of a certain type which you can add, remove and get. </summary>
/// <typeparam name="T"> Type it stores </typeparam>
public interface ICollection<T> where T : IID
{
	public uint Add(T instance);
	public void Remove(T instance);
	public void Remove(uint getter);
	public T Get(uint getter);
	public int GetCount();
	public void Clear();
}