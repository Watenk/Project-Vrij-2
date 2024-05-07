using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : IAttack
{
	// Dependencies
	private CharacterAttackSettings characterAttackSettings;
	private Transform attackRoot;
	
	public CharacterAttack(CharacterAttackSettings characterAttackSettings, Transform attackRoot)
	{
		this.characterAttackSettings = characterAttackSettings;
		this.attackRoot = attackRoot;
	}
	
	public void Attack()
	{
		Collider[] hitColliders = Physics.OverlapSphere(attackRoot.transform.position, characterAttackSettings.AttackRange);
		foreach (var collider in hitColliders)
		{
			if (collider.gameObject == null) continue;
			IHealth health = collider.gameObject.GetComponent<IHealth>();
			if (health == null) continue;
			health.ChangeHealth(-characterAttackSettings.AttackDamage);
		}
	}
}

