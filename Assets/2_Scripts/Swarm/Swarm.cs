using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A collection of boids </summary>
public class Swarm : MonoBehaviour
{
	[Header("Local Settings")]
	[SerializeField] [Tooltip("The amount of boids in the swarm")]
	private byte amount;
	
	[SerializeField] [Tooltip("Note that the boids will be able to leave this range. Its a target range and not enforced")]
	private float wanderRadius;
	
	[SerializeField] [Tooltip("The boids will try to avoid objects in this array. Note that the boids will 'try' to avoid the objects. It's not enforced")]
	private Transform[] obstacles;

	[Header("Shared Settings")]
	[SerializeField] [Tooltip("The settings for the AI of the boids in the swarm")]
	private BoidSettings boidSettings;
	
	[Header("Events")]
	[SerializeField]
	private SwarmChannel swarmChannel;

	[Header("Factory's")]
	[SerializeField]
	private BoidFactory boidFactory;
	
	private DictCollection<Boid> boidCollection = new DictCollection<Boid>();

	public void Start()
	{
		PopulateBoidsCollection();
	}
	
	public void FixedUpdate()
	{
		foreach (var kvp in boidCollection.instances)
		{
			kvp.Value.FixedUpdate();
		}
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
			Boid newBoid = boidFactory.Construct(boidSettings, this.transform.position, this.transform);
			newBoid.OnDeath += OnBoidDeath;
			boidCollection.Add(newBoid);
		} 
	}
	
	private void OnBoidDeath(Boid boid)
	{
		swarmChannel.OnBoidDeath?.Invoke(boid);
		boid.OnDeath -= OnBoidDeath;
		boidFactory.Deconstruct(boid);
	}
}