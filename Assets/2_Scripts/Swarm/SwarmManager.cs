using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : ADictCollection<ISwarm, ISwarm>, IFixedUpdateable
{
	public void FixedUpdate()
	{
		foreach (var swarm in instances)
		{
			swarm.Value.FixedUpdate();
		}
	}
	
	protected override ISwarm Construct(ISwarm data)
	{
		return data;
	}

    protected override void Deconstruct(ISwarm instance) {}
}
