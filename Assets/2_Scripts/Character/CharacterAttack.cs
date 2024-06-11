using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : IAttack
{
	public static event Action<int> OnAttack = delegate { };

	public event IAttack.KillEventhandler OnKill;
	
	// Grab
	private bool grabbing;
	private GameObject grabbedObject;
	
	// Dependencies
	private CharacterAttackSettings characterAttackSettings;
	private Transform attackRoot;

	public CharacterAttack(CharacterAttackSettings characterAttackSettings, Transform attackRoot)
	{
		this.characterAttackSettings = characterAttackSettings;
		this.attackRoot = attackRoot;
	}
	
	//LMB
	public void Slash()
	{
		OnAttack(2);
		Collider[] hitColliders = Physics.OverlapSphere(attackRoot.transform.position, characterAttackSettings.AttackRange);
		foreach (var collider in hitColliders)
		{
			IPhysicsDamagable damagable = collider.gameObject.GetComponent<IPhysicsDamagable>();
			if (damagable == null || collider.gameObject.layer == LayerMask.NameToLayer("Player")) return;
			damagable.TakeDamage(characterAttackSettings.AttackDamage);
			OnKill?.Invoke();
		}
	}
	
	// Hold RMB
	public void GrabObject(GameObject other, GameObject player)
	{
		if (!grabbing) return;
		other.transform.SetParent(player.transform);
	}
	
	public void Grab()
	{
		grabbing = true;
	}
	
	public void GrabRelease()
	{
		grabbing = false;
	}
	
	// E
	public void Stun(SirenLocation sirenLocation)
	{
		Vector3 direction = Camera.main.transform.forward;
		Vector3 origin = sirenLocation.Position;
		float radius = characterAttackSettings.SingRadius;
		float reach = characterAttackSettings.SingReach;
		int tries = 100;
		int currentTries = 0;
	
		while (tries != currentTries)
		{
			RaycastHit[] hits = Physics.RaycastAll(new Vector3(origin.x + UnityEngine.Random.Range(-radius, radius), origin.y, origin.z + UnityEngine.Random.Range(-radius, radius)), direction, reach);

			foreach (RaycastHit hit in hits)
			{
				Debug.DrawLine(origin, hit.point);
				if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Human"))
				{
					hit.collider.gameObject.GetComponent<PhysicsStunDetector>().Stun();
				}
			}
			
			currentTries++;
		}
	}
}