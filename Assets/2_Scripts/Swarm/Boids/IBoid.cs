using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoid : IGameObject, IRigidBody, IID, IFixedUpdateable
{
	public ISwarm Swarm { get; }
	public Health<Boid> Health { get; }
	public float Speed { get; }
	public BoidMovement BoidMovement{ get; }
	public DamageTaker DamageTaker { get; }
}
