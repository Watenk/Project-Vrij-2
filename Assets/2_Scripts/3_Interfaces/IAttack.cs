using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack 
{
	public delegate void KillEventhandler();
	public event KillEventhandler OnKill;
	
	public void Grab(GameObject other, GameObject player);
	public void Slash();
	public void Stun(SirenLocation sirenLocation);
}
