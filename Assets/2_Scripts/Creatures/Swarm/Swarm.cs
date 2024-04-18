using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : ICollectionManager<Creature, CreatureData, int>
{
	private List<Creature> creatures = new List<Creature>();
	private ISwarmAI swarmAI;

	public void Add(CreatureData data)
	{
		throw new System.NotImplementedException();
	}

	public void Remove(Creature instance)
	{
		throw new System.NotImplementedException();
	}

	public void Remove(int getter)
	{
		throw new System.NotImplementedException();
	}
	
	public Creature Get(int getter)
	{
		throw new System.NotImplementedException();
	}

    public int GetSize()
    {
        return creatures.Count;
    }
}
