using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttackState : BaseState<Human>
{
	public static event Action<int> OnAttack = delegate { };
	private Timer attackDelay;

	public override void Init(Fsm<Human> owner, Human blackboard)
	{
		base.Init(owner, blackboard);
		attackDelay = new Timer(bb.humansSettings.AttackDelay);
	}

	public override void Enter()
	{
		attackDelay.Reset();
		attackDelay.OnTimer += Attack;
	}

	public override void Update()
	{
		attackDelay.Tick(Time.deltaTime);
		Debug.Log(attackDelay.TimeLeft);
		UpdateRotation();
	}

	public override void Exit()
	{
		attackDelay.OnTimer -= Attack;
	}

	private void Attack()
	{
		int weaponAmount = bb.humansSettings.ThrowingWeaponsPrefabs.Count;

		GameObject randomWeaponPrefab = bb.humansSettings.ThrowingWeaponsPrefabs[UnityEngine.Random.Range(0, weaponAmount)];
		GameObject weaponInstance = GameObject.Instantiate(randomWeaponPrefab, new Vector3(bb.GameObject.transform.position.x, bb.GameObject.transform.position.y + 3.0f, bb.GameObject.transform.position.z), Quaternion.identity);
		weaponInstance.transform.rotation = Quaternion.LookRotation(bb.sirenLocation.Position - bb.GameObject.transform.position);
		Rigidbody weaponRigidbody = weaponInstance.GetComponent<Rigidbody>();
		weaponRigidbody.AddForce(weaponInstance.transform.forward * bb.humansSettings.WeaponThrowSpeed);
		OnAttack(4);
		attackDelay.Reset();
	}
	
	private void UpdateRotation()
	{
		Vector3 direction = bb.sirenLocation.Position - bb.GameObject.transform.position;
		direction.y = 0f;

		Quaternion targetRotation = Quaternion.LookRotation(direction);
		bb.GameObject.transform.rotation = Quaternion.Slerp(bb.GameObject.transform.rotation, targetRotation, Time.deltaTime * bb.humansSettings.RotationSpeed);
	}
}
