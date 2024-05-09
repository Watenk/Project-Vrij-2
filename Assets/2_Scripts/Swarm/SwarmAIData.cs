using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Watenk;

[CreateAssetMenu(fileName = "SwarmAIData", menuName = "Data/SwarmAIData")]
public class SwarmAIData : ScriptableObject
{
	public float MinSpeed;
	public float MaxSpeed;
	[Tooltip("The range in which the boids will affect another")]
	public float NeighbourDetectRange; 
	[Tooltip("The range in which the boids will detect and avoid obstacles")]
	public float ObstacleDetectRange; 
	[Tooltip("The force with which the boids try to seperate from each other")]
	public float SeparationForce; 
	[Tooltip("The force with which the boids try to have the same velocity as each other")]
	public float AlignmentForce; 
	[Tooltip("The force with which the boids will try to move to the center of their group")]
	public float CohesionForce; 
}