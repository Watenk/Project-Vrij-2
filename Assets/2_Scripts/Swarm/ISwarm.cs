using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwarm : IGameObject
{
	public byte Amount { get; }
	public float WanderRadius { get; }
	public Transform[] Obstacles { get; }
	public BoidSettings BoidSettings { get; }
	public SwarmChannel SwarmChannel { get; }

	
	public List<IBoid> GetBoidNeighbours(IBoid boid, float range);
}
