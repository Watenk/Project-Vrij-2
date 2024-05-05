using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> is an ICollectionManager </summary>
public class SwarmManager : ICollectionManager<ISwarm, ISwarm, int>, IFixedUpdateable
{
	private List<ISwarm> swarms = new List<ISwarm>();	
	
	public void FixedUpdate()
	{
		foreach (ISwarm swarm in swarms)
		{
			swarm.FixedUpdate();
		}
	}
	
	public void Add(ISwarm data)
	{
		swarms.Add(data);
	}

	public ISwarm Get(int getter)
	{
		return swarms[getter];
	}

	public int GetCount()
	{
		return swarms.Count;
	}

	public void Remove(ISwarm instance)
	{
		swarms.Remove(instance);
	}

	public void Remove(int getter)
	{
		swarms.Remove(Get(getter));
	}
}
