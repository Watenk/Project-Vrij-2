using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class BoidMovement : IFixedUpdateable
{
	private BoidSettings boidSettings;
	
	public BoidMovement(BoidSettings boidSettings)
	{
		this.boidSettings = boidSettings;
	}

	public void FixedUpdate()
	{
		throw new System.NotImplementedException();
	}

	public void UpdateAI(Dictionary<uint, Boid> boids)
	{		
		// foreach (var kvp in boids)
		// {
		// 	Boid boid = kvp.Value;
			
		// 	// Boids
		// 	boid.rb.velocity += CalcBoidVelocity(boid) * boid.Speed;
			
		// 	// Bounds
		// 	if (Vector3.Distance(boid.rb.position, swarm.Center) > swarm.WanderRadius - swarm.WanderRadius * 0.5)
		// 	{
		// 		Vector3 centerDirection = (swarm.Center - boid.rb.position).normalized;
		// 		boid.rb.velocity += centerDirection;
		// 	}
			
		// 	// Objects
		// 	foreach (Transform obstacle in swarm.Obstacles)
		// 	{
		// 		if (Vector3.Distance(boid.rb.position, obstacle.position) < boidSettings.ObstacleDetectRange)
		// 		{
		// 			Vector3 avoidDirection = (obstacle.position - boid.rb.position).normalized;
		// 			boid.rb.velocity += avoidDirection;
		// 		}				
		// 	}
			
		// 	// Rotation
		// 	Vector3 direction = boid.rb.velocity.normalized;
		// 	Quaternion targetRotation = Quaternion.LookRotation(direction);
		// 	boid.rb.transform.rotation = Quaternion.Slerp(boid.rb.transform.rotation, targetRotation, Time.deltaTime * 10f);
		// }
	}
	
	private Vector3 CalcBoidVelocity(Boid boid)
	{
		// Vector3 averageSeperation = Vector3.zero;
		// Vector3 averageVelocity = Vector3.zero;
		// Vector3 averagePosition = Vector3.zero;
	
		// List<Boid> boids = swarm.GetNeighbours(boid, boidSettings.NeighbourDetectRange);
		// foreach (Boid otherBoid in boids)
		// {
		// 	averageSeperation += (boid.rb.transform.position - otherBoid.rb.transform.position) / (Vector3.Distance(boid.rb.transform.position, otherBoid.rb.transform.position) + 1);
		// 	averageVelocity += otherBoid.rb.velocity;
		// 	averagePosition += otherBoid.rb.position;
		// }
		// if (boids.Count > 0)
		// {
		// 	averageSeperation /= boids.Count;
		// 	averageVelocity /= boids.Count;
		// 	averagePosition /= boids.Count;
		// }
	
		// Vector3 cohesion = (averagePosition - boid.rb.transform.position) * boidSettings.CohesionForce;
		// Vector3 alignment = averageVelocity * boidSettings.AlignmentForce;
		// Vector3 separation = averageSeperation * boidSettings.SeparationForce;

		// Vector3 velocity = cohesion + alignment + separation;
		// return velocity.normalized;
		return Vector3.zero;
	}
}
