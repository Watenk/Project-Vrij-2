using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Watenk;

/// <summary> Controls and manages a list of creatures using ISwarmAI and ICollectionManager </summary>
public class Swarm : ISwarm
{
	public float WanderRadius { get; private set; }
	public Vector3 Center { get; private set; }
	public byte Amount { get; private set; }
	public Transform[] Obstacles { get; private set; }
	
	private List<Boid> boids = new List<Boid>();
	private Dictionary<int, Boid> IDs = new Dictionary<int, Boid>();
	private int idCounter;
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
	
	public int Add(Boid data)
	{
		boids.Add(data);
		IDs.Add(idCounter, data);
		idCounter++;
		return idCounter - 1;
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
		GameObject.Destroy(instance.Rigidbody.gameObject);
	}

	public void Remove(int getter)
	{
		IDs.TryGetValue(getter, out Boid boid);
		IDs.Remove(getter);
		Remove(boid);
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
}
