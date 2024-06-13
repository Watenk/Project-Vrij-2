using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack 
{
	public delegate void KillEventhandler();
	public event KillEventhandler OnKill;
	
	public void Update();
	public void GrabObject(GameObject other, GameObject player, Transform attackRoot);
	public void Grab();
	public void GrabRelease();
	public void Slash();
	public void Stun(SirenLocation sirenLocation);
}
