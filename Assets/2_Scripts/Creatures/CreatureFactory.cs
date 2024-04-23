using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Makes instances of creatures using creatureData </summary>
public class CreatureFactory : IFactory<GameObject, CreatureData>
{
	public GameObject Create(CreatureData data)
	{
		throw new System.NotImplementedException();
	}
}
