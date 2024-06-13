using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class Health<T> : IHealth<T>
{
	public event IHealth<T>.HealthChangeEventHandler OnHealthChanged;
	public event IHealth<T>.DeathEventHandler OnDeath;
	public static event Action<int> OnKill = delegate { };

	public int HP { get; private set; }
	public int MaxHP { get; private set; }
	
	private T instance;

	public Health(T instance, int maxHealth)
	{
		this.instance = instance;
		MaxHP = maxHealth;
		HP = maxHealth;
	}

	public virtual void ChangeHealth(int amount)
	{
		HP += amount;
		
		if(amount > 0) OnKill(3);
		if (HP <= 0) Die();
		if (HP > MaxHP) HP = MaxHP;
		
		OnHealthChanged?.Invoke(instance);	
	}

	public virtual void Die()
	{
		OnDeath?.Invoke(instance);
	}
}