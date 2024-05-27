using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDamager : MonoBehaviour, IDamager
{
	private void OnTriggerEnter(Collider other) 
	{
		IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
		if (damagable == null) return;	
		damagable.TakeDamage(1);
	}
}
