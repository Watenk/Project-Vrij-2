using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwarm : IFixedUpdateable, ICollection<Boid>, IID
{
	public float WanderRadius { get; }
	public Vector3 Center { get; }
	public byte Amount { get; }
	public Transform[] Obstacles { get; }
	
	public List<Boid> GetNeighbours(Boid boid, float range);
}
