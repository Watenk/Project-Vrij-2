using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SwarmChannel", menuName = "Channels/Swarm")]
public class SwarmChannel : ScriptableObject
{
	public UnityEvent<Vector3> OnFishDeath;
	
	public SwarmCollection SwarmCollection { get; private set; } = new SwarmCollection();
}