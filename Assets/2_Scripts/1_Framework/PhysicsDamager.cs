using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDamager : MonoBehaviour, IDamager
{
	private void OnTriggerEnter(Collider other) 
	{
		IPhysicsDamagable damagable = other.gameObject.GetComponent<IPhysicsDamagable>();
		if (damagable == null) return;	
		damagable.TakeDamage(1);
	}
}
