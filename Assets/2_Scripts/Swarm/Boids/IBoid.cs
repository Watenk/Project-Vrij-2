using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoid : IGameObject, IRigidBody, IID
{
	public float Speed { get; }
	public PhysicsDamageDetector PhysicsDamageDetector { get; }
	public Health<Boid> Health { get; }
	
	public void UpdateMovement(List<IBoid> neighbours, Vector3 swarmCenter);
}
