using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class BoidMovement : IFixedUpdateable
{		
	// Dependency 
	private IBoid boid;
	private ISwarm swarm;
	
	public BoidMovement(IBoid boid, ISwarm swarm)
	{
		this.boid = boid;
		this.swarm = swarm;
	}
	
	public void FixedUpdate()
	{
		UpdateBoidsVelocity();
		UpdateBoundsVelocity();
		UpdateObjectAvoidenceVelocity();
		UpdateRotation();
	}
	
	private void UpdateBoidsVelocity()
	{
		List<IBoid> neighbours = swarm.GetBoidNeighbours(boid, swarm.BoidSettings.NeighbourDetectRange);
		if (neighbours.Count == 0) return;
		
		Vector3 averageSeperation = Vector3.zero;
		Vector3 averageVelocity = Vector3.zero;
		Vector3 averagePosition = Vector3.zero;
	
		foreach (IBoid neighbour in neighbours)
		{
			averageSeperation += (boid.GameObject.transform.position - neighbour.GameObject.transform.position) / (Vector3.Distance(boid.GameObject.transform.position, neighbour.GameObject.transform.position) + 1);
			averageVelocity += neighbour.RB.velocity;
			averagePosition += neighbour.RB.position;
		}
		
		averageSeperation /= neighbours.Count;
		averageVelocity /= neighbours.Count;
		averagePosition /= neighbours.Count;
	
		Vector3 cohesion = (averagePosition - boid.GameObject.transform.position) * swarm.BoidSettings.CohesionForce;
		Vector3 alignment = averageVelocity * swarm.BoidSettings.AlignmentForce;
		Vector3 separation = averageSeperation * swarm.BoidSettings.SeparationForce;

		Vector3 velocity = cohesion + alignment + separation;
		boid.RB.velocity += velocity.normalized * boid.Speed;
	}
	
	private void UpdateBoundsVelocity()
	{
		if (Vector3.Distance(boid.GameObject.transform.position, swarm.GameObject.transform.position) <= swarm.WanderRadius) return;
		
		boid.RB.velocity += (swarm.GameObject.transform.position - boid.GameObject.transform.position).normalized;
	}
	
	private void UpdateObjectAvoidenceVelocity()
	{
		if (swarm.Obstacles.Length == 0) return;
		
		Vector3 velocity = Vector3.zero;
		foreach (Transform obstacle in swarm.Obstacles)
		{
			if (Vector3.Distance(boid.GameObject.transform.position, obstacle.position) > swarm.BoidSettings.ObstacleDetectRange) continue;
			
			velocity += (obstacle.position - boid.GameObject.transform.position).normalized;
		}
		boid.RB.velocity += velocity.normalized;
	}
	
	private void UpdateRotation()
	{
		Vector3 direction = boid.RB.velocity.normalized;
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		boid.GameObject.transform.rotation = Quaternion.Slerp(boid.GameObject.transform.rotation, targetRotation, Time.deltaTime * 10f);
	}
}
