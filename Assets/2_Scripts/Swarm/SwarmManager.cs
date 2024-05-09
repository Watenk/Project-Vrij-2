using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : ICollectionManager<ISwarm, ISwarm, int>, IFixedUpdateable
{
	private List<ISwarm> swarms = new List<ISwarm>();	
	private Dictionary<int, ISwarm> IDs = new Dictionary<int, ISwarm>();
	private int idCounter = 0;
	
	public void FixedUpdate()
	{
		foreach (ISwarm swarm in swarms)
		{
			swarm.FixedUpdate();
		}
	}
	
	public int Add(ISwarm data)
	{
		swarms.Add(data);
		IDs.Add(idCounter, data);
		idCounter++;
		return idCounter - 1;
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
		IDs.TryGetValue(getter, out ISwarm swarm);
		IDs.Remove(getter);
		Remove(swarm);
	}
}
