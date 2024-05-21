using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth<T>
{
	public delegate void HealthChangeEventHandler(T instance);
	public event HealthChangeEventHandler OnHealthChanged;
	public delegate void DeathEventHandler(T instance);
	public event DeathEventHandler OnDeath;
	
	public int Health { get; }
	public int MaxHealth { get; }
	
	public void ChangeHealth(int amount);
	public void Die();
}
