using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttackState : BaseState<Human>
{
	private Timer attackDelay;

	public override void Init(Fsm<Human> owner, Human blackboard)
	{
		base.Init(owner, blackboard);
		attackDelay = new Timer(bb.humansSettings.AttackDelay);
	}

	public override void Enter()
	{
		attackDelay.Reset();
		attackDelay.OnTimer += OnAttackDelayTimer;
	}

	public override void Update()
	{
		attackDelay.Tick(Time.deltaTime);
		UpdateRotation();
	}

	public override void Exit()
	{
		attackDelay.OnTimer -= OnAttackDelayTimer;
	}

	private void OnAttackDelayTimer()
	{
		Attack();
		attackDelay.Reset();
	}

	private void Attack()
	{
		int weaponAmount = bb.humansSettings.ThrowingWeaponsPrefabs.Count;
		GameObject randomWeaponPrefab = bb.humansSettings.ThrowingWeaponsPrefabs[Random.Range(0, weaponAmount)];
		GameObject weaponInstance = GameObject.Instantiate(randomWeaponPrefab, bb.GameObject.transform.position, Quaternion.identity);
		weaponInstance.transform.rotation = Quaternion.LookRotation(bb.sirenLocation.Position - bb.GameObject.transform.position);
		Rigidbody weaponRigidbody = weaponInstance.GetComponent<Rigidbody>();
		weaponRigidbody.AddForce(weaponInstance.transform.forward * bb.humansSettings.WeaponThrowSpeed);
	}
	
	private void UpdateRotation()
	{
		Vector3 direction = bb.sirenLocation.Position - bb.GameObject.transform.position;
		direction.y = 0f;

		Quaternion targetRotation = Quaternion.LookRotation(direction);
		bb.GameObject.transform.rotation = Quaternion.Slerp(bb.GameObject.transform.rotation, targetRotation, Time.deltaTime * bb.humansSettings.RotationSpeed);
	}
}
