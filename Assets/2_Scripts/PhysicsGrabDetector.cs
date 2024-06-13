using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsGrabDetector : MonoBehaviour
{
	public delegate void VoidEventHandler();
	public event VoidEventHandler OnGrab;

    public void Grab()
    {
        OnGrab();
    }
}
