using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
	public delegate void HealthChangeEventHandler(int health);
	public event HealthChangeEventHandler OnHealthChanged;
	public delegate void DeathEventHandler();
	public event DeathEventHandler OnDeath;
	
	public int Health { get; }
	public int MaxHealth { get; }
	
	public void ChangeHealth(int amount);
	public void Die();
}
