using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class Boid : IBoid
{
	public ISwarm Swarm { get; private set; }
	public Health<Boid> Health { get; private set; }
	public GameObject GameObject { get; private set; }
	public float Speed { get; private set; }
	public Rigidbody RB { get; private set; }
	public uint ID { get; private set; }
	public BoidMovement BoidMovement { get; private set; }
	public DamageTaker DamageTaker { get; private set; }

	public Boid(ISwarm swarm, Vector3 pos)
	{
		Swarm = swarm;
		
		Speed = Random.Range(Swarm.BoidSettings.SpeedBounds.x, Swarm.BoidSettings.SpeedBounds.y);
		GameObject = GameObject.Instantiate(Swarm.BoidSettings.Prefabs[Random.Range(0, Swarm.BoidSettings.Prefabs.Length)], pos, Quaternion.identity, swarm.GameObject.transform);
		DamageTaker = GameObject.GetComponent<DamageTaker>();
		if (DamageTaker == null) DebugUtil.ThrowError("Boid Doesn't contain a DamageTaker");
		RB = GameObject.GetComponent<Rigidbody>();
		if (RB == null) DebugUtil.ThrowError("Boid Doesn't contain a Rigidbody");
		Health = new Health<Boid>(this, Random.Range(Swarm.BoidSettings.HealthBounds.x, Swarm.BoidSettings.HealthBounds.y));
		BoidMovement = new BoidMovement(this, swarm);
		
		DamageTaker.OnDamage += (amount) => Health.ChangeHealth(amount * -1);
	}
	
	~Boid()
	{
		DamageTaker.OnDamage -= (amount) => Health.ChangeHealth(amount * -1);
	}

	public void FixedUpdate()
	{
		BoidMovement.FixedUpdate();
	}
	
	public void ChangeID(uint newID)
	{
		ID = newID;
	}
}