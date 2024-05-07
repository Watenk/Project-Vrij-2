using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack
{
	// Dependencies
	private CharacterAttackSettings characterAttackSettings;
	private Transform attackRoot;
	
	public CharacterAttack(CharacterAttackSettings characterAttackSettings, Transform attackRoot)
	{
		this.characterAttackSettings = characterAttackSettings;
		this.attackRoot = attackRoot;
	}
	
	// private void Attack()
	// {
	// 	Collider[] hitColliders = Physics.OverlapSphere(player.GameObject.transform.position, AttackRange);
	// 	foreach (var hitCollider in hitColliders)
	// 	{
	// 		if (hitCollider.CompareTag("Fish"))
	// 		{
	// 			Debug.Log("Ik zie fish");

	// 			if (Input.GetMouseButtonDown(1))
	// 			{
	// 				GameObject.Destroy(hitCollider.gameObject);
	// 				EatedFish = true;

	// 			}
	// 		}
	// 	}
	// }
}

