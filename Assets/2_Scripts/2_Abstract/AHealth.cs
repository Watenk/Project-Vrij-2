using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public abstract class AHealth<T> : IHealth<T>
{
    public event IHealth<T>.HealthChangeEventHandler OnHealthChanged;
    public event IHealth<T>.DeathEventHandler OnDeath;

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
		if (Health > MaxHealth) Health = MaxHealth;
		
		OnHealthChanged();
	}

	public virtual void Die()
	{
		OnDeath();
	}
}