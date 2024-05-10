using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public abstract class ADictCollection<T, U> : ICollectionManager<T, U>
{
	protected Dictionary<uint, T> instances = new Dictionary<uint, T>();
	protected Dictionary<T, uint> keys = new Dictionary<T, uint>();
	protected uint idCounter = 1;
	
	// ICollectionManager
	public uint Add(U data)
	{
		T instance = Construct(data);
		instances.Add(idCounter, instance);
		keys.Add(instance, idCounter);
		
		idCounter++;
		return idCounter - 1;
	}


	public T Get(uint getter)
	{
		instances.TryGetValue(getter, out T instance);
		if (instance == null) DebugUtil.ThrowWarning("Tried to get from " + this.GetType().Name + " but cant find instance");
		return instance;
	}

	public int GetCount()
	{
		return instances.Count;
	}

	public void Remove(T instance)
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
		
		Deconstruct(instance);
	}
    public void Clear()
    {
        foreach (var kvp in instances)
		{
			Remove(kvp.Key);
		}
    }
	
	// IFactory
	protected abstract T Construct(U data);
	protected abstract void Deconstruct(T instance);
}
