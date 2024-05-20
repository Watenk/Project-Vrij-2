using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

/// <summary> A manager that keeps track of GameObjects using ICollectionManager </summary>
/// <typeparam name="T"> The type of object it stores </typeparam>
public class DictCollection<T> : ICollection<T> where T : IID
{
	protected Dictionary<uint, T> instances = new Dictionary<uint, T>();
	protected Dictionary<T, uint> keys = new Dictionary<T, uint>();
	protected uint idCounter = 1;
	
	// ICollectionManager
	public virtual uint Add(T instance)
	{
		instance.ChangeID(idCounter);
		
		instances.Add(idCounter, instance);
		keys.Add(instance, idCounter);
		
		idCounter++;
		return idCounter - 1;
	}

	public T Get(uint getter)
	{
		instances.TryGetValue(getter, out T instance);
		if (instance == null) DebugUtil.ThrowError("Tried to get id " + getter + " from " + this.GetType().Name + " but cant find instance");
		return instance;
	}

	public int GetCount()
	{
		return instances.Count;
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
		instances.Remove(getter);
		
		if (instance is IGameObject)
		{
			GameObject.Destroy(((IGameObject)instance).GameObject);
		}
	}
	
	public void Clear()
	{
		foreach (var kvp in instances)
		{
			Remove(kvp.Key);
		}
	}
}
