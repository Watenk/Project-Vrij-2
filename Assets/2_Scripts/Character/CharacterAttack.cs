using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : IAttack
{
	public event IAttack.KillEventhandler OnKill;
	
	private GameObject grabbedObject;
	
	// Dependencies
	private CharacterAttackSettings characterAttackSettings;
	private Transform attackRoot;


	public CharacterAttack(CharacterAttackSettings characterAttackSettings, Transform attackRoot)
	{
		this.characterAttackSettings = characterAttackSettings;
		this.attackRoot = attackRoot;
	}
	
	public void Slash()
	{
		Collider[] hitColliders = Physics.OverlapSphere(attackRoot.transform.position, characterAttackSettings.AttackRange);
		foreach (var collider in hitColliders)
		{
			IPhysicsDamagable damagable = collider.gameObject.GetComponent<IPhysicsDamagable>();
			if (damagable == null) return;
			damagable.TakeDamage(characterAttackSettings.AttackDamage);
			OnKill?.Invoke();
		}
	}
	
	public void Grab(GameObject other, GameObject player)
	{
		other.transform.SetParent(player.transform);
	}
}