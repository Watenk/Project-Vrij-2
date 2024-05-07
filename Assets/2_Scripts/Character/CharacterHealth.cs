using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class CharacterHealth : IDamageable
{
	public int Health { get; private set; }
	public int MaxHealth { get; private set; }

	public CharacterHealth(int maxHealth)
	{
		MaxHealth = maxHealth;
		Health = MaxHealth;
	}

	public void TakeDamage(int amount)
	{
		Health -= amount;
		
		if (Health <= 0) Die();
	}

	public void Die()
	{
		DebugUtil.ThrowLog("Character Died");
	}
}
