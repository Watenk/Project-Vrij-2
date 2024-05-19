using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictCollectionUpdate<T> : DictCollection<T>, IUpdateable where T : IID, IUpdateable
{
	public void Update() 
	{
		foreach (var obj in instances)
		{
			obj.Value.Update();
		}
	}
}
