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
	private bool onBoat;
	private Transform boatPos;
	private Vector3 previousBoatPos;
	
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
		CharacterInputHandler.OnGrabDown += CharacterAttack.Grab;
		CharacterInputHandler.OnGrabUp += CharacterAttack.GrabRelease;
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
		CharacterInputHandler.OnGrabDown -= CharacterAttack.Grab;
		CharacterInputHandler.OnGrabUp -= CharacterAttack.GrabRelease;
		damageTaker.OnDamage -= (amount) => CharacterHealth.ChangeHealth(amount * -1);
		CharacterAttack.OnKill -= () => CharacterHealth.ChangeHealth(1);
		CharacterHealth.OnHealthChanged -= (amount) => ServiceLocator.Instance.Get<EventManager>().Invoke(Event.OnPlayerHealth, amount.CharacterHealth.HP);
	}
	
	public void Update()
	{
		CharacterInputHandler.Update();
		sirenLocation.Position = gameObject.transform.position;
		
		if (onBoat)
		{
			CharacterMovement.rb.AddForce(CharacterMovement.rb.gameObject.transform.up * characterControllerSettings.Gravity * Time.deltaTime, ForceMode.Impulse);
			Vector3 difference = previousBoatPos - boatPos.position;
			gameObject.transform.position += difference;
			previousBoatPos = boatPos.position;
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Human"))
		{
			CharacterAttack.GrabObject(other.gameObject, this.gameObject, attackRoot);
		}
		
		if (other.gameObject.layer == LayerMask.NameToLayer("PlayerParent") && !onBoat)
		{
			onBoat = true;
			boatPos = other.transform;
			previousBoatPos = boatPos.position;
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("PlayerParent") && onBoat)
		{
			onBoat = false;
		}
	}
	
	#if UNITY_EDITOR
	public void OnDrawGizmosSelected()
	{
		// AttackRange Sphere
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackRoot.transform.position, characterAttackSettings.AttackRange);
	}
	#endif
}