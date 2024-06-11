using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsStunDetector : MonoBehaviour
{
	public delegate void StunEventHandler();
	public event StunEventHandler OnStun;

    public void Stun()
    {
        OnStun();
    }
}
