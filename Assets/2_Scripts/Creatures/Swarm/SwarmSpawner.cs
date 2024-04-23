using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

/// <summary> Creates a new swarm and adds it to the swarmManager </summary>
public class SwarmSpawner : MonoBehaviour
{
	public List<CreatureData> creatures = new List<CreatureData>();
	public float WanderRadius = 10f;
	public byte Amount;
	public ISwarmAI SwarmAI;
	
	void Start()
	{
		Swarm swarm = new Swarm(SwarmAI);
		for (int i = 0; i < Amount; i++)
		{
			swarm.Add(creatures[Random.Range(0, creatures.Count - 1)]);
		} 
		
		SwarmManager swarmManager = ServiceManager.Instance.Get<SwarmManager>();
		swarmManager.Add(swarm);
		
		Destroy(this.gameObject);
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, WanderRadius);
	}
}
