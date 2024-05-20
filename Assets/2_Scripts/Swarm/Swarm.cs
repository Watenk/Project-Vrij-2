using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour, IID
{
	[Header("Settings")]
	[SerializeField] [Tooltip("The amount of boids in the swarm")]
	private byte amount;
	[SerializeField] [Tooltip("Note that the boids will be able to leave this range. Its a target range and not enforced")]
	private float wanderRadius;
	[SerializeField] [Tooltip("The kinds of boids in the swarm")]
	private GameObject[] creaturePrefabs;
	[SerializeField] [Tooltip("The boids will try to avoid objects in this array. Note that the boids will 'try' to avoid the objects. It's not enforced")]
	private Transform[] obstacles;
	[SerializeField] [Tooltip("The AI that controls the boids in the swarm")]
	private SwarmAISettings swarmAIData;
	
	[Header("Blackboards")]
	[SerializeField]
	private SwarmBlackboard swarmBlackboard;
	
	[Header("Channels")]
	[SerializeField]
	private SwarmChannel swarmChannel;

	[Header("Factory's")]
	private BoidFactory boidFactory = new BoidFactory();
	
	public uint ID { get; }
	
	private DictCollection<Boid> boidCollection = new DictCollection<Boid>();

	// Logic
	public void Start()
	{
		uint swarmID = swarmBlackboard.SwarmCollection.Add(this);
		
		// Populate boidCollection
		for (int i = 0; i < amount; i++)
		{
			Boid newBoid = boidFactory.Construct(creaturePrefabs, this.transform.position, this.transform);
			boidCollection.Add(newBoid);
			//boid.Init(swarmID, Random.Range(SwarmAIData.MinSpeed, SwarmAIData.MaxSpeed));
		} 
	}
	
	public void Update()
	{
	}
	
	#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, wanderRadius);
	}
	#endif
	
	public void ChangeID(uint newID)
	{
		throw new System.NotImplementedException();
	}
}
