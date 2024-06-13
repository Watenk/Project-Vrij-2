using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
	[SerializeField]
	private float despawnTime;
	
	private Timer despawnTimer;

	private void Start() 
	{
		despawnTimer = new Timer(despawnTime);
		despawnTimer.OnTimer += OnDespawnTimer;
	}

	private void Update() 
	{
		despawnTimer.Tick(Time.deltaTime);	
	}
	
	private void OnCollisionEnter(Collision other) 
	{
		PhysicsDamageDetector damagable = other.gameObject.GetComponent<PhysicsDamageDetector>();
		if (damagable == null) damagable = other.gameObject.GetComponentInChildren<PhysicsDamageDetector>();
		if (damagable == null || other.gameObject.layer == LayerMask.NameToLayer("Human")) return;
		damagable.TakeDamage(1);
		GameObject.Destroy(this.gameObject);
	}
	
	private void OnDespawnTimer()
	{
		despawnTimer.OnTimer -= OnDespawnTimer;
		GameObject.Destroy(this.gameObject);
	}
}
