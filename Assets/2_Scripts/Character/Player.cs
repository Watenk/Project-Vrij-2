using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class Player : MonoBehaviour, IPlayer
{
	public CharacterController CharacterController { get; private set; }
	public CharacterAttack CharacterAttack { get; private set; }
	public CharacterHealth Characterhealth { get; private set; }
	
	[Header("References")]
	[SerializeField][Tooltip("The point the camera will rotate around")]
	private Transform cameraRoot;
	[SerializeField]
	private Transform moddelRoot;
	[SerializeField]
	private CinemachineRecomposer cinemachineRecomposer;
	[SerializeField][Tooltip("The point the player will attack from")]
	private Transform attackRoot;
	
	[Header("Settings")]
	[SerializeField]
	private int maxHealth;
	[SerializeField]
	private CharacterControllerSettings characterControllerSettings;
	[SerializeField]
	private CharacterAttackSettings characterAttackSettings;

	public void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb == null) DebugUtil.ThrowError(this.name + " is missing a RigidBody");
		
		CharacterController = new CharacterController(characterControllerSettings, rb, cameraRoot, moddelRoot, cinemachineRecomposer);
		CharacterAttack = new CharacterAttack(characterAttackSettings, attackRoot);
		Characterhealth = new CharacterHealth(maxHealth);
	}
	
	public void Update()
	{
		CharacterController.Update();
	}

	#if UNITY_EDITOR
	public void OnDrawGizmosSelected()
	{
		// AttackRange Sphere
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackRoot.position, characterAttackSettings.AttackRange);
	}
	#endif
}
