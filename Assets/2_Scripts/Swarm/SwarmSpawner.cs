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
		if (NullChecks())
		{
			DebugUtil.TrowError("Swarm didn't spawn because of error");
			return;	
		} 
		SwarmAIData.NullChecks();

		// Spawn Swarm
		Swarm swarm = new Swarm(SwarmAIData, WanderRadius, this.transform.position, Amount, Obstacles);
		for (int i = 0; i < Amount; i++)
		{
			GameObject randomPrefab = creaturePrefabs[Random.Range(0, creaturePrefabs.Count - 1)];
			Vector3 spawnPos = new Vector3(
				this.transform.position.x + Random.Range(-WanderRadius, WanderRadius),
				this.transform.position.y + Random.Range(-WanderRadius, WanderRadius),
				this.transform.position.z + Random.Range(-WanderRadius, WanderRadius)
				);
			GameObject instance = GameObject.Instantiate(randomPrefab, spawnPos, Quaternion.identity, this.transform);
			
			Rigidbody rigidbody;
			rigidbody = instance.GetComponent<Rigidbody>();
		
			if (rigidbody == null)
			{
				rigidbody = instance.GetComponentInChildren<Rigidbody>();

				if (rigidbody == null)
				{
					DebugUtil.TrowError("Tried to add a prefab to a swarm that doesn't contain a Rigidbody");
					return;
				}
			}

			Boid newBoid = new Boid(rigidbody, Random.Range(SwarmAIData.MinSpeed, SwarmAIData.MaxSpeed));
			swarm.Add(newBoid);
		} 
		
		// Add to SwarmManager
		SwarmManager swarmManager = ServiceManager.Instance.Get<SwarmManager>();
		swarmManager.Add(swarm);
	}
	
	private bool NullChecks()
	{
		if (creaturePrefabs.Count == 0)
		{
			DebugUtil.TrowError(this.gameObject.name + " has no prefabs assigned");
			return true;
		}
		
		if (SwarmAIData == null)
		{
			DebugUtil.TrowError(this.gameObject.name + " swarmAIData has no swarmAIData assigned");
			return true;
		}
		
		return false;
	}
	
	#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, WanderRadius);
	}
	#endif
}
