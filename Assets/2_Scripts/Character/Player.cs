using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Watenk;

public class Player : MonoBehaviour, IPlayer
{
	public ICharacterInputHandler CharacterInputHandler { get; private set; }
	public ICharacterMovement CharacterMovement { get; private set; }
	public IAttack CharacterAttack { get; private set; }
	public Health<IPlayer> CharacterHealth { get; private set; }
	
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
	private Image healthSlider;
	[SerializeField]
	private Image boostSlider;
	
	[Header("Settings")]
	[SerializeField]
	private int maxHealth;
	[SerializeField]
	private CharacterMovementSettings characterControllerSettings;
	[SerializeField]
	private CharacterAttackSettings characterAttackSettings;
	
	private PhysicsDamageDetector damageTaker;
	
	public void Awake()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb == null) DebugUtil.ThrowError(this.name + " is missing a RigidBody");
		damageTaker = GetComponentInChildren<PhysicsDamageDetector>();
		if (damageTaker == null) DebugUtil.ThrowError(this.name + " is missing a DamageTaker");
		
		CharacterInputHandler = new CharacterInputHandler();
		CharacterMovement = new CharacterController(characterControllerSettings, rb, cameraRoot, moddelRoot, cinemachineRecomposer, waterSurface);
		CharacterAttack = new CharacterAttack(characterAttackSettings, attackRoot);
		CharacterHealth = new Health<IPlayer>(this, maxHealth);
	}
	
	public void OnEnable() 
	{
		CharacterInputHandler.OnMove += CharacterMovement.UpdateMovement;
		CharacterInputHandler.OnRotate += CharacterMovement.UpdateRotation;
		CharacterInputHandler.OnAttack += CharacterAttack.Slash;
		CharacterInputHandler.OnStun += () => CharacterAttack.Stun(sirenLocation);
		CharacterInputHandler.OnBoost += CharacterMovement.Boost;
		damageTaker.OnDamage += (amount) => CharacterHealth.ChangeHealth(amount * -1);
		CharacterAttack.OnKill += () => CharacterHealth.ChangeHealth(1);
		CharacterHealth.OnHealthChanged += (amount) => ServiceLocator.Instance.Get<EventManager>().Invoke(Event.OnPlayerHealth, amount.CharacterHealth.HP);
	}
	
	public void OnDisable() 
	{
		CharacterInputHandler.OnMove -= CharacterMovement.UpdateMovement;
		CharacterInputHandler.OnRotate -= CharacterMovement.UpdateRotation;
		CharacterInputHandler.OnAttack -= CharacterAttack.Slash;
		CharacterInputHandler.OnStun -= () => CharacterAttack.Stun(sirenLocation);
		CharacterInputHandler.OnBoost -= CharacterMovement.Boost;
		damageTaker.OnDamage -= (amount) => CharacterHealth.ChangeHealth(amount * -1);
		CharacterAttack.OnKill -= () => CharacterHealth.ChangeHealth(1);
		CharacterHealth.OnHealthChanged -= (amount) => ServiceLocator.Instance.Get<EventManager>().Invoke(Event.OnPlayerHealth, amount.CharacterHealth.HP);
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
	
	#if UNITY_EDITOR
	public void OnDrawGizmosSelected()
	{
		// AttackRange Sphere
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackRoot.position, characterAttackSettings.AttackRange);
	}
	#endif
}