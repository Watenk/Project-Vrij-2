using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public abstract class AHealth : IHealth
{
	public event IHealth.HealthChangeEventHandler OnHealthChanged;
    public event IHealth.DeathEventHandler OnDeath;

    public int Health { get; private set; }
	public int MaxHealth { get; private set; }

	public AHealth(int maxHealth)
	{
		MaxHealth = maxHealth;
		Health = maxHealth;
	}

	public virtual void ChangeHealth(int amount)
	{
		Health += amount;
		
		if (Health <= 0) Die();
		else if (Health > MaxHealth) Health = MaxHealth;
		
		OnHealthChanged(Health);
	}

	public virtual void Die()
	{
		OnDeath();
	}
}