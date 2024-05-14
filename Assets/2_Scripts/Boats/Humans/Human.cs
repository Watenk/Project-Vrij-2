using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : IGameObject, IFixedUpdateable
{
	public uint ID { get; private set; }
	public GameObject GameObject { get; private set; }

	public void ChangeID(uint newID)
	{
		ID = newID;
	}

    public void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
