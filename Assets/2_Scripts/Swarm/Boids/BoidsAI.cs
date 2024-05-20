using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class BoidsAI 
{
	private ISwarm swarm;
	private SwarmAISettings swarmAIData;
	
	public BoidsAI(ISwarm swarm, SwarmAISettings swarmAIData)
	{
		this.swarm = swarm;
		this.swarmAIData = swarmAIData;
	}
	
	public void UpdateAI(Dictionary<uint, Boid> boids)
	{		
		foreach (var kvp in boids)
		{
			Boid boid = kvp.Value;
			
			// Boids
			boid.Rigidbody.velocity += CalcBoidVelocity(boid) * boid.Speed;
			
			// Bounds
			if (Vector3.Distance(boid.Rigidbody.position, swarm.Center) > swarm.WanderRadius - swarm.WanderRadius * 0.5)
			{
				Vector3 centerDirection = (swarm.Center - boid.Rigidbody.position).normalized;
				boid.Rigidbody.velocity += centerDirection;
			}
			
			// Objects
			foreach (Transform obstacle in swarm.Obstacles)
			{
				if (Vector3.Distance(boid.Rigidbody.position, obstacle.position) < swarmAIData.ObstacleDetectRange)
				{
					Vector3 avoidDirection = (obstacle.position - boid.Rigidbody.position).normalized;
					boid.Rigidbody.velocity += avoidDirection;
				}				
			}
			
			// Rotation
			Vector3 direction = boid.Rigidbody.velocity.normalized;
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			boid.Rigidbody.transform.rotation = Quaternion.Slerp(boid.Rigidbody.transform.rotation, targetRotation, Time.deltaTime * 10f);
		}
	}
	
	private Vector3 CalcBoidVelocity(Boid boid)
	{
		Vector3 averageSeperation = Vector3.zero;
		Vector3 averageVelocity = Vector3.zero;
		Vector3 averagePosition = Vector3.zero;
	
		List<Boid> boids = swarm.GetNeighbours(boid, swarmAIData.NeighbourDetectRange);
		foreach (Boid otherBoid in boids)
		{
			averageSeperation += (boid.Rigidbody.transform.position - otherBoid.Rigidbody.transform.position) / (Vector3.Distance(boid.Rigidbody.transform.position, otherBoid.Rigidbody.transform.position) + 1);
			averageVelocity += otherBoid.Rigidbody.velocity;
			averagePosition += otherBoid.Rigidbody.position;
		}
		if (boids.Count > 0)
		{
			averageSeperation /= boids.Count;
			averageVelocity /= boids.Count;
			averagePosition /= boids.Count;
		}
	
		Vector3 cohesion = (averagePosition - boid.Rigidbody.transform.position) * swarmAIData.CohesionForce;
		Vector3 alignment = averageVelocity * swarmAIData.AlignmentForce;
		Vector3 separation = averageSeperation * swarmAIData.SeparationForce;

		Vector3 velocity = cohesion + alignment + separation;
		return velocity.normalized;
	}
}
