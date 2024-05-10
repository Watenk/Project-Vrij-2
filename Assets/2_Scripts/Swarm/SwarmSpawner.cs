using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

/// <summary> Creates a new swarm and adds it to the swarmManager </summary>
public class SwarmSpawner : MonoBehaviour
{
	public List<GameObject> creaturePrefabs = new List<GameObject>();
	[Tooltip("Note that the boids will be able to leave this range. Its a target range and not enforced")]
	public float WanderRadius;
	public byte Amount;
	public SwarmAIData SwarmAIData;
	public Transform[] Obstacles;
	
	void Start()
	{
		// Spawn Swarm
		Swarm swarm = new Swarm(SwarmAIData, WanderRadius, this.transform.position, Amount, Obstacles);
		
		// Add to SwarmManager
		SwarmManager swarmManager = ServiceManager.Instance.Get<SwarmManager>();
		uint swarmID = swarmManager.Add(swarm);
		
		// Populate Swarm
		for (int i = 0; i < Amount; i++)
		{
			// Prefab Instance
			GameObject randomPrefab = creaturePrefabs[Random.Range(0, creaturePrefabs.Count - 1)];
			Vector3 spawnPos = new Vector3(
				this.transform.position.x + Random.Range(-WanderRadius, WanderRadius),
				this.transform.position.y + Random.Range(-WanderRadius, WanderRadius),
				this.transform.position.z + Random.Range(-WanderRadius, WanderRadius)
			);
			GameObject instance = GameObject.Instantiate(randomPrefab, spawnPos, Quaternion.identity, this.transform);
			
			// Boid
			Boid boid = instance.GetComponent<Boid>();
			if (boid == null)
			{
				boid = GetComponentInChildren<Boid>();
				if (boid == null)
				{
					DebugUtil.ThrowError(this.name + "doesn't contain a Boid");
				}
			}
			
			uint boidID = swarm.Add(boid);
			boid.Init(swarmID, boidID, Random.Range(SwarmAIData.MinSpeed, SwarmAIData.MaxSpeed));
		} 
	}
	
	#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, WanderRadius);
	}
	#endif
}
