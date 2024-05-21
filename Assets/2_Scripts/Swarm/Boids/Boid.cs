using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class Boid : IHealth<Boid>, IGameObject, IID, IFixedUpdateable
{
	// Events
    public event IHealth<Boid>.HealthChangeEventHandler OnHealthChanged;
    public event IHealth<Boid>.DeathEventHandler OnDeath;

	public int Health { get; private set; }
	public int MaxHealth { get; private set; }
	public uint ID { get; private set; }
	public GameObject GameObject { get; private set; }

	// References / Settings
	[Header("Settings")]
	[SerializeField]
	private int maxHealth;
	
	// Dependencies
	private BoidMovement boidMovement;


    public Boid(BoidSettings boidSettings, GameObject gameObject)
	{
		MaxHealth = maxHealth;
		Health = maxHealth;
		GameObject = gameObject;
		
		boidMovement = new BoidMovement(boidSettings);
	}

	public void FixedUpdate()
	{
		boidMovement.FixedUpdate();
	}

	public void ChangeHealth(int amount)
	{
		Health += amount;
		
		OnHealthChanged?.Invoke(this);
		
		if (Health <= 0) Die();
		if (Health > MaxHealth) Health = MaxHealth;
	}

	public void Die()
	{
		OnDeath?.Invoke(this);
	}

	public void ChangeID(uint newID)
	{
		ID = newID;
	}
}