using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHealthDetector : MonoBehaviour
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
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			EventManager.Instance.Invoke(Event.OnPlayerHit);
			Destroy(this.gameObject);
		}
	}
}
