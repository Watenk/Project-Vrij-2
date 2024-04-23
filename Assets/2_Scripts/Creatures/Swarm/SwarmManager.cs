using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> is an ICollectionManager </summary>
public class SwarmManager : ICollectionManager<Swarm, Swarm, int>
{
	private List<Swarm> swarms = new List<Swarm>();	
	
	public void Add(Swarm data)
	{
		swarms.Add(data);
	}

	public Swarm Get(int getter)
	{
		return swarms[getter];
	}

	public int GetSize()
	{
		return swarms.Count;
	}

	public void Remove(Swarm instance)
	{
		swarms.Remove(instance);
	}

	public void Remove(int getter)
	{
		swarms.Remove(swarms[getter]);
	}
}
