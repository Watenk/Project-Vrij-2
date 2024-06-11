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
	
	private void OnDespawnTimer()
	{
		despawnTimer.OnTimer -= OnDespawnTimer;
		GameObject.Destroy(this.gameObject);
	}
}
