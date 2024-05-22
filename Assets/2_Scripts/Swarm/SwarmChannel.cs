using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SwarmChannel", menuName = "Swarm/Channel")]
public class SwarmChannel : ScriptableObject
{
	public UnityEvent<IBoid> OnBoidDeath;
}