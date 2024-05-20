using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollection : ADictCollection<Boat>, IFixedUpdateable
{
	public void FixedUpdate()
	{
		foreach (var kvp in instances)
		{
			kvp.Value.FixedUpdate();
		}
	}
}
