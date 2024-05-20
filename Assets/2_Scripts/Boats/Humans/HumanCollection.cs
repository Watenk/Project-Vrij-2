using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCollection : ADictCollection<Human>, IFixedUpdateable
{
	public void FixedUpdate()
	{
		foreach (var kvp in instances)
		{
			kvp.Value.FixedUpdate();
		}
	}
}
