using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwarmBlackboard", menuName = "Swarm/SwarmBlackboard")]
public class SwarmBlackboard : ScriptableObject
{
	public DictCollection<Swarm> SwarmCollection { get; private set; } = new DictCollection<Swarm>();
}
