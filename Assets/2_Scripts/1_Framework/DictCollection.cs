using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

/// <summary> A manager that keeps track of GameObjects using ICollectionManager </summary>
/// <typeparam name="T"> The type of object it stores </typeparam>
public class DictCollection<T> : ICollection<T, uint> where T : IID
{
	public Dictionary<uint, T> Collection { get; protected set; } = new Dictionary<uint, T>();
	
	protected Dictionary<T, uint> keys = new Dictionary<T, uint>();
	protected uint idCounter = 1;
	
	public virtual uint Add(T instance)
	{
		instance.ChangeID(idCounter);
		
		Collection.Add(idCounter, instance);
		keys.Add(instance, idCounter);
		
		idCounter++;
		return idCounter - 1;
	}

	public T Get(uint getter)
	{
		Collection.TryGetValue(getter, out T instance);
		if (instance == null) DebugUtil.ThrowError("Tried to get id " + getter + " from " + this.GetType().Name + " but cant find instance");
		return instance;
	}

	public int GetCount()
	{
		return Collection.Count;
	}

	public virtual void Remove(T instance)
	{
		keys.TryGetValue(instance, out uint key);
		if (key == 0)
		{
			DebugUtil.ThrowError("Tried to remove instance while its not tracked by " + this.GetType().Name);
			return;	
		} 
		
		Remove(key);
	}

	public void Remove(uint getter)
	{
		T instance = Get(getter);
		
		keys.Remove(instance);
		Collection.Remove(getter);
	}
	
	public void Clear()
	{
		foreach (var kvp in Collection)
		{
			Remove(kvp.Key);
		}
	}
}
