using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttackState : BaseState<Human>
{
	private float attackDelay;

	public override void Enter()
	{
		base.Enter();
		attackDelay = blackboard.humansSettings.AttackDelay;
	}

	public override void Update()
	{
		base.Update();
		
		Quaternion targetRotation = Quaternion.LookRotation(blackboard.sirenLocation.Position - blackboard.GameObject.transform.position);
		blackboard.GameObject.transform.rotation = Quaternion.Lerp(blackboard.GameObject.transform.rotation, targetRotation, Time.deltaTime * blackboard.humansSettings.RotationSpeed);
		attackDelay -= Time.deltaTime;
		
		if (attackDelay <= 0)
		{
			Attack();
			attackDelay = blackboard.humansSettings.AttackDelay;
		}
	}

	private void Attack()
	{
		int weaponAmount = blackboard.humansSettings.ThrowingWeaponsPrefabs.Count;
		GameObject randomWeaponPrefab = blackboard.humansSettings.ThrowingWeaponsPrefabs[Random.Range(0, weaponAmount)];
		GameObject weaponInstance = GameObject.Instantiate(randomWeaponPrefab, blackboard.GameObject.transform.position, Quaternion.identity);
		weaponInstance.transform.rotation = Quaternion.LookRotation(blackboard.sirenLocation.Position - blackboard.GameObject.transform.position);
		Rigidbody weaponRigidbody = weaponInstance.GetComponent<Rigidbody>();
		weaponRigidbody.AddForce(weaponInstance.transform.forward * blackboard.humansSettings.WeaponThrowSpeed);
	}
}
