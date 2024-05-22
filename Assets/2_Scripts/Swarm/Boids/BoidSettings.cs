using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Watenk;

[CreateAssetMenu(fileName = "BoidSettings", menuName = "Swarm/BoidSettings")]
public class BoidSettings : ScriptableObject
{
	public Vector2 SpeedBounds { get { return speedBounds; } }
	[SerializeField] [Tooltip("The min and max speed of the boids")]
	private Vector2 speedBounds;
	
	public Vector2Int HealthBounds { get { return healthBounds; } }
	[SerializeField] [Tooltip("The min and max health of the boids")]
	private Vector2Int healthBounds;
	
	public float NeighbourDetectRange { get { return neighbourDetectRange; } }
	[SerializeField] [Tooltip("The range in which the boids will affect another")]
	private float neighbourDetectRange; 
	
	public float ObstacleDetectRange { get { return obstacleDetectRange; } }
	[SerializeField] [Tooltip("The range in which the boids will detect and avoid obstacles")]
	private float obstacleDetectRange; 
	
	public float SeparationForce { get { return separationForce; } }
	[SerializeField] [Tooltip("The force with which the boids try to seperate from each other")]
	private float separationForce; 
	
	public float AlignmentForce { get { return alignmentForce; } }
	[SerializeField] [Tooltip("The force with which the boids try to have the same velocity as each other")]
	private float alignmentForce; 
	
	public float CohesionForce { get { return cohesionForce; } }
	[SerializeField] [Tooltip("The force with which the boids will try to move to the center of their group")]
	private float cohesionForce; 
	
	public GameObject[] Prefabs { get { return prefabs; } }
	
	[SerializeField] [Tooltip("The kinds of boids in the swarm")]
	private GameObject[] prefabs;
}