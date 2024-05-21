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
	public IHealth<IPlayer> CharacterHealth { get; private set; }
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
	[SerializeField]
	private SirenLocation sirenLocation;
	
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
	
	private bool startHasRun = false;

	public void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb == null) DebugUtil.ThrowError(this.name + " is missing a RigidBody");
		
		CharacterInputHandler = new CharacterInputHandler();
		CharacterMovement = new CharacterController(characterControllerSettings, rb, cameraRoot, moddelRoot, cinemachineRecomposer, waterSurface);
		CharacterAttack = new CharacterAttack(characterAttackSettings, attackRoot);
		CharacterHealth = new CharacterHealth<IPlayer>(maxHealth);
		CharacterUI = new CharacterUI(healthSlider, boostSlider);
		
		EnableEvents();
		startHasRun = true;
	}
	
	public void OnEnable() 
	{
		if (startHasRun) EnableEvents();
	}
	
	public void OnDisable() 
	{
		CharacterInputHandler.OnMove -= CharacterMovement.UpdateMovement;
		CharacterInputHandler.OnRotate -= CharacterMovement.UpdateRotation;
		CharacterInputHandler.OnAttack -= CharacterAttack.Slash;
		ChangeHealth -= CharacterHealth.ChangeHealth;
		CharacterHealth.OnHealthChanged -= CharacterUI.UpdateHealthAmount;
		EventManager.Instance.AddListener(Event.OnPlayerHit, () => CharacterHealth.ChangeHealth(-1));
		EventManager.Instance.AddListener(Event.OnFishDeath, () => CharacterHealth.ChangeHealth(1));
	}
	
	public void Update()
	{
		CharacterInputHandler.Update();
		sirenLocation.Position = gameObject.transform.position;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Human"))
		{
			CharacterAttack.Grab(other.gameObject, this.gameObject);
		}
	}
	
	private void EnableEvents()
	{
		CharacterInputHandler.OnMove += CharacterMovement.UpdateMovement;
		CharacterInputHandler.OnRotate += CharacterMovement.UpdateRotation;
		CharacterInputHandler.OnAttack += CharacterAttack.Slash;
		ChangeHealth += CharacterHealth.ChangeHealth;
		CharacterHealth.OnHealthChanged += CharacterUI.UpdateHealthAmount;
		EventManager.Instance.AddListener(Event.OnPlayerHit, () => CharacterHealth.ChangeHealth(-1));
		EventManager.Instance.AddListener(Event.OnFishDeath, () => CharacterHealth.ChangeHealth(1));
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