using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSettings : MonoBehaviour
{
	public byte Amount { get { return amount; } }
	[SerializeField] [Tooltip("The amount of boids in the swarm")]
	private byte amount;
	
	public float WanderRadius { get { return wanderRadius; } }
	[SerializeField] [Tooltip("Note that the boids will be able to leave this range. Its a target range and not enforced")]
	private float wanderRadius;
	
	public float SwarmSleepRange { get { return swarmSleepRange; } }
	[SerializeField] 
	private float swarmSleepRange;
	
	public Transform[] Obstacles { get { return obstacles; } }
	[SerializeField] [Tooltip("The boids will try to avoid objects in this array. Note that the boids will 'try' to avoid the objects. It's not enforced")]
	private Transform[] obstacles;
}
