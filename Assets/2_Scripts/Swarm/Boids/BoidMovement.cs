using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public partial class Boid : IBoid
{			
	public void UpdateMovement(List<IBoid> neighbours, Vector3 swarmCenter)
	{
		UpdateBoidsVelocity(neighbours);
		UpdateBoundsVelocity(swarmCenter);
		UpdateObjectAvoidenceVelocity();
		UpdateRotation();
	}
	
	private void UpdateBoidsVelocity(List<IBoid> neighbours)
	{
		if (neighbours.Count == 0) return;
		
		Vector3 averageSeperation = Vector3.zero;
		Vector3 averageVelocity = Vector3.zero;
		Vector3 averagePosition = Vector3.zero;
	
		foreach (IBoid neighbour in neighbours)
		{
			averageSeperation += (GameObject.transform.position - neighbour.GameObject.transform.position) / 
								 (Vector3.Distance(GameObject.transform.position, neighbour.GameObject.transform.position) + 1);
			averageVelocity += neighbour.RB.velocity;
			averagePosition += neighbour.RB.position;
		}
		
		averageSeperation /= neighbours.Count;
		averageVelocity /= neighbours.Count;
		averagePosition /= neighbours.Count;
	
		Vector3 cohesion = (averagePosition - GameObject.transform.position) * BoidSettings.CohesionForce;
		Vector3 alignment = averageVelocity * BoidSettings.AlignmentForce;
		Vector3 separation = averageSeperation * BoidSettings.SeparationForce;

		Vector3 velocity = cohesion + alignment + separation;
		RB.velocity += velocity.normalized * Speed;
	}
	
	private void UpdateBoundsVelocity(Vector3 swarmCenter)
	{
		if (Vector3.Distance(GameObject.transform.position, swarmCenter) <= SwarmSettings.WanderRadius) return;
		
		RB.velocity += (swarmCenter - GameObject.transform.position).normalized;
	}
	
	private void UpdateObjectAvoidenceVelocity()
	{
		if (SwarmSettings.Obstacles.Length == 0) return;
		
		Vector3 velocity = Vector3.zero;
		foreach (Transform obstacle in SwarmSettings.Obstacles)
		{
			if (Vector3.Distance(GameObject.transform.position, obstacle.position) > BoidSettings.ObstacleDetectRange) continue;
			
			velocity += (obstacle.position - GameObject.transform.position).normalized;
		}
		RB.velocity += velocity.normalized;
	}
	
	private void UpdateRotation()
	{
		Vector3 direction = RB.velocity.normalized;
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		GameObject.transform.rotation = Quaternion.Slerp(GameObject.transform.rotation, targetRotation, Time.deltaTime * 10f);
	}
}