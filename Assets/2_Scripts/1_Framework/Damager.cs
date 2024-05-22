using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
	private float lifeTime;
	
	private void Start() 
	{
		lifeTime = 10.0f;	
	}
	
	private void Update() 
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0)
		{
			Destroy(this.gameObject);
		}
	}
	
	private void OnTriggerEnter(Collider other) 
	{
		IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
		if (damagable == null) return;	
		damagable.TakeDamage(1);
	}
}
