using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public partial class Boid : IBoid
{
	public float Speed { get; private set; }
	public GameObject GameObject { get; private set; }
	public Rigidbody RB { get; private set; }
	public uint ID { get; private set; }
	public BoidSettings BoidSettings { get; private set; }
	public SwarmSettings SwarmSettings { get; private set; }
	public PhysicsDamageDetector PhysicsDamageDetector { get; private set; }
	public Health<Boid> Health { get; private set; }

	public Boid(SwarmSettings swarmSettings, BoidSettings boidSettings, Vector3 pos, Transform parent)
	{
		SwarmSettings = swarmSettings;
		BoidSettings = boidSettings;
		GameObject = GameObject.Instantiate(boidSettings.Prefabs[Random.Range(0, boidSettings.Prefabs.Length)], pos, Quaternion.identity, parent);
		Speed = Random.Range(boidSettings.SpeedBounds.x, boidSettings.SpeedBounds.y);
		
		PhysicsDamageDetector = GameObject.GetComponent<PhysicsDamageDetector>();
		if (PhysicsDamageDetector == null) DebugUtil.ThrowError(GameObject.name + " Doesn't contain a DamageTaker");
		RB = GameObject.GetComponent<Rigidbody>();
		if (RB == null) DebugUtil.ThrowError(GameObject.name + " Doesn't contain a Rigidbody");
		
		Health = new Health<Boid>(this, Random.Range(boidSettings.HealthBounds.x, boidSettings.HealthBounds.y));
		
		PhysicsDamageDetector.OnDamage += (amount) => Health.ChangeHealth(amount * -1);
	}
	
	~Boid()
	{
		PhysicsDamageDetector.OnDamage -= (amount) => Health.ChangeHealth(amount * -1);
	}

	public void ChangeID(uint newID)
	{
		ID = newID;
	}
}