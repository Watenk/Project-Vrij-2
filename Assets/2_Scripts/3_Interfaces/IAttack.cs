using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack 
{
	public void Slash();
	public void Grab(GameObject other, GameObject player);
}
