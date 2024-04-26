using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Boid
{
	public Rigidbody Rigidbody { get; private set; }
	public float Speed { get; private set; }
	
	public Boid(Rigidbody rigidbody, float speed)
	{
		Rigidbody = rigidbody;	
		Speed = speed;
	}
}
