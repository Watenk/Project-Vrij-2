using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A collection of boids </summary>
public class Swarm : MonoBehaviour, ISwarm
{
	public SwarmSettings SwarmSettings { get { return swarmSettings; } }
	[SerializeField]
	private SwarmSettings swarmSettings;
	
	public BoidSettings BoidSettings { get { return boidSettings; } }
	[Header("Shared Settings")]
	[SerializeField] [Tooltip("The settings for the AI of the boids in the swarm")]
	private BoidSettings boidSettings;
	
	public SwarmChannel SwarmChannel { get { return swarmChannel; } }
	[SerializeField]
	private SwarmChannel swarmChannel;
	
	public SirenLocation SirenLocation { get { return sirenLocation; } }
	[SerializeField]
	private SirenLocation sirenLocation;
	
	public GameObject GameObject { get; private set; }

	private DictCollection<IBoid> boidCollection = new DictCollection<IBoid>();

	public void Start()
	{
		GameObject = this.gameObject;
		
		for (int i = 0; i < swarmSettings.Amount; i++)
		{
			AddBoid();	
		}
	}
	
	public void FixedUpdate()
	{
		if (Vector3.Distance(sirenLocation.Position, gameObject.transform.position) > swarmSettings.SwarmSleepRange) return;
		
		foreach (var kvp in boidCollection.Collection)
		{
			kvp.Value.UpdateMovement(GetBoidNeighbours(kvp.Value, BoidSettings.NeighbourDetectRange), transform.position);
		}
	}
	
	#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, swarmSettings.WanderRadius);
	}
	#endif
	
	private void AddBoid()
	{
		Vector3 spawnPos = new Vector3(
			this.transform.position.x + Random.Range(-swarmSettings.WanderRadius, swarmSettings.WanderRadius),
			this.transform.position.y + Random.Range(-swarmSettings.WanderRadius, swarmSettings.WanderRadius),
			this.transform.position.z + Random.Range(-swarmSettings.WanderRadius, swarmSettings.WanderRadius)
		);
		IBoid newBoid = new Boid(swarmSettings, boidSettings, spawnPos, this.gameObject.transform);
		newBoid.Health.OnDeath += RemoveBoid;
		boidCollection.Add(newBoid);
	}
	
	private void RemoveBoid(IBoid boid)
	{
		swarmChannel.OnBoidDeath?.Invoke(boid);
		
		boidCollection.Remove(boid);
		boid.Health.OnDeath -= RemoveBoid;
		GameObject.Destroy(boid.GameObject);
	}
	
	private List<IBoid> GetBoidNeighbours(IBoid boid, float range){
		List<IBoid> neighbours = new List<IBoid>();
		foreach (var kvp in boidCollection.Collection)
		{
			IBoid otherBoid = kvp.Value;
			
			if (Vector3.Distance(boid.GameObject.transform.position, otherBoid.GameObject.transform.position) <= range)
			{
				neighbours.Add(otherBoid);
			}
		}
		return neighbours;
	}
}