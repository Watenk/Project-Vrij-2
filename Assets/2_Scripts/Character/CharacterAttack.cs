using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : IAttack
{
	public static event Action<int> OnAttackSound = delegate { };
	public static event Action<string> OnAttackAnimation = delegate { };

	public event IAttack.KillEventhandler OnKill;
	
	// Grab
	private bool grabbing;
	private GameObject grabbedObject;
	private bool grabAllowed;
	private ITimer grabCooldownTimer;
	private bool slashAllowed;
	private ITimer slashCooldownTimer;
	private bool singAllowed;
	private ITimer singCooldownTimer;
	
	// Dependencies
	private CharacterAttackSettings characterAttackSettings;
	private Transform attackRoot;
	private TimerManager timerManager;

	public CharacterAttack(CharacterAttackSettings characterAttackSettings, Transform attackRoot)
	{
		this.characterAttackSettings = characterAttackSettings;
		this.attackRoot = attackRoot;
		
		timerManager = ServiceLocator.Instance.Get<TimerManager>();
		grabCooldownTimer = timerManager.Add(characterAttackSettings.DragCooldown);
		slashCooldownTimer = timerManager.Add(characterAttackSettings.SlashCooldown);
		singCooldownTimer = timerManager.Add(characterAttackSettings.SingCooldown);
		
		grabCooldownTimer.OnTimer += () => grabAllowed = true;
		slashCooldownTimer.OnTimer += () => slashAllowed = true;
		singCooldownTimer.OnTimer += () => singAllowed = true;
	}
	
	~CharacterAttack()
	{
		grabCooldownTimer.OnTimer -= () => grabAllowed = true;
		slashCooldownTimer.OnTimer -= () => slashAllowed = true;
		singCooldownTimer.OnTimer -= () => singAllowed = true;
	}
	
	//LMB
	public void Slash()
	{
		if (!slashAllowed) return;
		
		OnAttackSound(2);
		OnAttackAnimation("Slash");
		Collider[] hitColliders = Physics.OverlapSphere(attackRoot.transform.position, characterAttackSettings.AttackRange);
		slashAllowed = false;
		slashCooldownTimer.Reset();
		foreach (var collider in hitColliders)
		{
			PhysicsDamageDetector damagable = collider.gameObject.GetComponent<PhysicsDamageDetector>();
			if (damagable == null || collider.gameObject.layer == LayerMask.NameToLayer("Player")) continue;
			damagable.TakeDamage(characterAttackSettings.AttackDamage);
			OnKill?.Invoke();
		}
	}
	
	// Hold RMB
	public void GrabObject(GameObject other, GameObject player)
	{
		if (!grabbing || !grabAllowed) return;
		
		other.transform.SetParent(player.transform);
		grabCooldownTimer.Reset();
		grabAllowed = false;
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
		if (!singAllowed) return;
		
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
		
		singAllowed = false;
		singCooldownTimer.Reset();
	}
}