using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour, IDamagable
{
	public delegate void DamageEventHandler(int amount);
	public event DamageEventHandler OnDamage;

    public void TakeDamage(int amount)
    {
        OnDamage(amount);
    }
}
