using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Watenk;

public class Player : MonoBehaviour, IPlayer
{
	// Events
	public event IPlayer.HealthChangeEventHandler ChangeHealth;

	public ICharacterInputHandler CharacterInputHandler { get; private set; }
	public ICharacterMovement CharacterMovement { get; private set; }
	public IAttack CharacterAttack { get; private set; }
	public IHealth CharacterHealth { get; private set; }
	public ICharacterUI CharacterUI { get; private set; }
	
	// References / Settings
	[Header("References")]
	[SerializeField][Tooltip("The point the camera will rotate around")]
	private Transform cameraRoot;
	[SerializeField]
	private Transform moddelRoot;
	[SerializeField]
	private CinemachineRecomposer cinemachineRecomposer;
	[SerializeField][Tooltip("The point the player will attack from")]
	private Transform attackRoot;
	[SerializeField]
	private Transform waterSurface;
	
	[Header("UI References")]
	[SerializeField]
	private Slider healthSlider;
	[SerializeField]
	private Slider boostSlider;
	
	[Header("Settings")]
	[SerializeField]
	private int maxHealth;
	[SerializeField]
	private CharacterMovementSettings characterControllerSettings;
	[SerializeField]
	private CharacterAttackSettings characterAttackSettings;

	public void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb == null) DebugUtil.ThrowError(this.name + " is missing a RigidBody");
		
		CharacterInputHandler = new CharacterInputHandler();
		CharacterMovement = new CharacterController(characterControllerSettings, rb, cameraRoot, moddelRoot, cinemachineRecomposer, waterSurface);
		CharacterAttack = new CharacterAttack(characterAttackSettings, attackRoot);
		CharacterHealth = new CharacterHealth(maxHealth);
		CharacterUI = new CharacterUI(healthSlider, boostSlider);
		
		CharacterInputHandler.OnMove += CharacterMovement.UpdateMovement;
		CharacterInputHandler.OnRotate += CharacterMovement.UpdateRotation;
		CharacterInputHandler.OnAttack += CharacterAttack.Slash;
		ChangeHealth += CharacterHealth.ChangeHealth;
		CharacterHealth.OnHealthChanged += CharacterUI.UpdateHealthAmount;
	}
	
	public void OnDisable() 
	{
		CharacterInputHandler.OnMove -= CharacterMovement.UpdateMovement;
		CharacterInputHandler.OnRotate -= CharacterMovement.UpdateRotation;
		CharacterInputHandler.OnAttack -= CharacterAttack.Slash;
		ChangeHealth -= CharacterHealth.ChangeHealth;
		CharacterHealth.OnHealthChanged -= CharacterUI.UpdateHealthAmount;
	}
	
	public void Update()
	{
		CharacterInputHandler.Update();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Human"))
		{
			CharacterAttack.Grab(other.gameObject, this.gameObject);
		}
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