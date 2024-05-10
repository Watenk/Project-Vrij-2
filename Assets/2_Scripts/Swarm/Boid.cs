using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class Boid: MonoBehaviour, IHealth
{
	// Events
	public event IHealth.HealthChangeEventHandler OnHealthChanged;
	public event IHealth.DeathEventHandler OnDeath;

	public int Health { get; private set; }
	public int MaxHealth { get; private set; }
	public Rigidbody Rigidbody { get; private set; }
	public float Speed { get; private set; }

	// References / Settings
	[Header("Settings")]
	[SerializeField]
	private int maxHealth;
	
	// Dependencies
	private SwarmManager swarmManager;
	private uint swarmID;
	private uint boidID;

	public void Init(uint swarmID, uint boidID, float speed)
	{
		Speed = speed;
		MaxHealth = maxHealth;
		Health = maxHealth;
		
		swarmManager = ServiceManager.Instance.Get<SwarmManager>();
		this.swarmID = swarmID;
		this.boidID = boidID;
		
		Rigidbody = GetComponent<Rigidbody>();
		if (Rigidbody == null)
		{
			Rigidbody = GetComponentInChildren<Rigidbody>();
			if (Rigidbody == null)
			{
				DebugUtil.ThrowError(this.name + "doesn't contain a Rigidbody");
			}
		}
	}

	public void ChangeHealth(int amount)
	{
		Health += amount;
		
		if (Health <= 0) Die();
		else if (Health > MaxHealth) Health = MaxHealth;
		
		OnHealthChanged?.Invoke(Health);
	}

	public void Die()
	{
		OnDeath?.Invoke();
		swarmManager.Get(swarmID).Remove(boidID);
	}
}
