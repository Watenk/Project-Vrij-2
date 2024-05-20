using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmCollection : ADictCollection<ISwarm>, IFixedUpdateable
{
	public void FixedUpdate()
	{
		foreach (var kvp in instances)
		{
			kvp.Value.FixedUpdate();
		}
	}
}
