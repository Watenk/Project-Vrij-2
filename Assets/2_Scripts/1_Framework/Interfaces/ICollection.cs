using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A manager that stores objects of a certain type which you can add, remove and get. </summary>
/// <typeparam name="T"> Type it stores </typeparam>
/// <typeparam name="U"> Getter Type </typeparam>
public interface ICollection<T, U>
{
	public uint Add(T instance);
	public void Remove(T instance);
	public void Remove(U getter);
	public T Get(U getter);
	public int GetCount();
	public void Clear();
}