using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwarm : IGameObject
{
	public SwarmSettings SwarmSettings { get; }
	public BoidSettings BoidSettings { get; }
	public SwarmChannel SwarmChannel { get; }
}
