using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

/// <summary> Controls and manages a list of creatures using ISwarmAI and ICollectionManager </summary>
public class Swarm : ICollectionManager<Boid, Boid, int>, IFixedUpdateable, ISwarm
{
	public float WanderRadius { get; private set; }
	public Vector3 Center { get; private set; }
	public byte Amount { get; private set; }
	public Transform[] Obstacles { get; private set; }
	
	private List<Boid> boids = new List<Boid>();
	private ISwarmAI swarmAI;

	public Swarm(SwarmAIData swarmAIData, float wanderRadius, Vector3 center, byte amount, Transform[] obstacles)
	{		
		swarmAI = new BoidsSwarmAI(this, swarmAIData);
		WanderRadius = wanderRadius;
		Center = center;
		Amount = amount;
		Obstacles = obstacles;
	}

	public void FixedUpdate()
	{
		swarmAI.UpdateAI(boids);
	}
	
	public List<Boid> GetNeighbours(Boid boid, float range){
		List<Boid> neighbours = new List<Boid>();
		foreach (Boid otherBoid in boids)
		{
			if (Vector3.Distance(boid.Rigidbody.transform.position, otherBoid.Rigidbody.transform.position) <= range)
			{
				neighbours.Add(otherBoid);
			}
		}
		
		return neighbours;
	}
	
	public void Add(Boid data)
	{
		boids.Add(data);
	}

	public Boid Get(int getter)
	{
		return boids[getter];
	}

	public int GetCount()
	{
		return boids.Count;
	}

	public void Remove(Boid instance)
	{
		boids.Remove(instance);
	}

	public void Remove(int getter)
	{
		boids.Remove(Get(getter));
	}
}
