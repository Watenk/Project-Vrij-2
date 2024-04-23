using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Controls and manages a list of creatures using ISwarmAI and ICollectionManager </summary>
public class Swarm : ICollectionManager<GameObject, CreatureData, int>
{
	private List<GameObject> creatures = new List<GameObject>();
	private ISwarmAI swarmAI;
	private IFactory<GameObject, CreatureData> factory;

	public Swarm(ISwarmAI swarmAI)
	{
		this.swarmAI = swarmAI;
	}

	public void Add(CreatureData data)
	{
		GameObject newCreature = factory.Create(data);
		creatures.Add(newCreature);
	}

	public void Remove(GameObject instance)
	{
		throw new System.NotImplementedException();
	}

	public void Remove(int getter)
	{
		throw new System.NotImplementedException();
	}
	
	public GameObject Get(int getter)
	{
		throw new System.NotImplementedException();
	}

	public int GetSize()
	{
		return creatures.Count;
	}
}
