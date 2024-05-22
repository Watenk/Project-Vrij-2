using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A collection of boids </summary>
public class Swarm : MonoBehaviour, ISwarm
{
	public byte Amount { get { return amount; } }
	[SerializeField] [Tooltip("The amount of boids in the swarm")]
	private byte amount;
	
	public float WanderRadius { get { return wanderRadius; } }
	[SerializeField] [Tooltip("Note that the boids will be able to leave this range. Its a target range and not enforced")]
	private float wanderRadius;
	
	public Transform[] Obstacles { get { return obstacles; } }
	[SerializeField] [Tooltip("The boids will try to avoid objects in this array. Note that the boids will 'try' to avoid the objects. It's not enforced")]
	private Transform[] obstacles;

	public BoidSettings BoidSettings { get { return boidSettings; } }
	[Header("Shared Settings")]
	[SerializeField] [Tooltip("The settings for the AI of the boids in the swarm")]
	private BoidSettings boidSettings;
	
	public SwarmChannel SwarmChannel { get { return swarmChannel; } }

	[Header("Events")]
	[SerializeField]
	private SwarmChannel swarmChannel;

	public GameObject GameObject { get; private set; }

	private DictCollection<IBoid> boidCollection = new DictCollection<IBoid>();

	public void Start()
	{
		GameObject = this.gameObject;
		PopulateBoidsCollection();
	}
	
	public void FixedUpdate()
	{
		foreach (var kvp in boidCollection.instances)
		{
			kvp.Value.FixedUpdate();
		}
	}
	
	public List<IBoid> GetBoidNeighbours(IBoid boid, float range){
		List<IBoid> neighbours = new List<IBoid>();
		foreach (var kvp in boidCollection.instances)
		{
			IBoid otherBoid = kvp.Value;
			
			if (Vector3.Distance(boid.GameObject.transform.position, otherBoid.GameObject.transform.position) <= range)
			{
				neighbours.Add(otherBoid);
			}
		}
		return neighbours;
	}

	#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, wanderRadius);
	}
	#endif
	
	private void PopulateBoidsCollection()
	{
		for (int i = 0; i < amount; i++)
		{
			Vector3 spawnPos = new Vector3(
				this.transform.position.x + Random.Range(-WanderRadius, WanderRadius),
				this.transform.position.y + Random.Range(-WanderRadius, WanderRadius),
				this.transform.position.z + Random.Range(-WanderRadius, WanderRadius)
			);
			IBoid newBoid = new Boid(this, spawnPos);
			newBoid.Health.OnDeath += OnBoidDeath;
			boidCollection.Add(newBoid);
		} 
	}
	
	private void OnBoidDeath(IBoid boid)
	{
		swarmChannel.OnBoidDeath?.Invoke(boid);
		
		boidCollection.Remove(boid);
		boid.Health.OnDeath -= OnBoidDeath;
		GameObject.Destroy(boid.GameObject);
	}
}