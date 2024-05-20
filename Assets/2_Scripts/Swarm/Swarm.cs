using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Watenk;

/// <summary> Controls and manages a list of creatures using ISwarmAI and ICollectionManager </summary>
public class Swarm : ADictCollection<Boid>, ISwarm
{
	public float WanderRadius { get; private set; }
	public Vector3 Center { get; private set; }
	public byte Amount { get; private set; }
	public Transform[] Obstacles { get; private set; }
	public uint ID { get; private set; }

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
		swarmAI.UpdateAI(instances);
	}

	public override uint Add(Boid instance)
	{
		instance.OnDeath += () => Remove(instance.ID);
		return base.Add(instance);
	}

	public override void Remove(Boid instance)
	{
		instance.OnDeath -= () => Remove(instance.ID);
		base.Remove(instance);
	}

	public List<Boid> GetNeighbours(Boid boid, float range){
		List<Boid> neighbours = new List<Boid>();
		foreach (var kvp in instances)
		{
			Boid otherBoid = kvp.Value;
			
			if (Vector3.Distance(boid.Rigidbody.transform.position, otherBoid.Rigidbody.transform.position) <= range)
			{
				neighbours.Add(otherBoid);
			}
		}
		
		return neighbours;
	}

	public void ChangeID(uint newID)
	{
		ID = newID;
	}
}
