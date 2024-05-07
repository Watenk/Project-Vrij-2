using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwarm : IFixedUpdateable
{
	public float WanderRadius { get; }
	public Vector3 Center { get; }
	public byte Amount { get; }
	public Transform[] Obstacles { get; }
	
	public List<Boid> GetNeighbours(Boid boid, float range);
}
