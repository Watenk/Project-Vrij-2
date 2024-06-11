using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : ICharacterInputHandler
{
	// Events
	public event ICharacterInputHandler.MoveEventHandler OnMove;
	public event ICharacterInputHandler.RotateEventHandler OnRotate;
	public event ICharacterInputHandler.voidEventHandler OnAttack;
	public event ICharacterInputHandler.voidEventHandler OnStun;
	public event ICharacterInputHandler.voidEventHandler OnBoost;
	
	// Inputs
	private PlayerInputs inputs;
	private InputAction movement;
	private InputAction verticalMovement;
	private InputAction rotation;
	private InputAction attack;
	private InputAction stun;
	private InputAction boost;

	public CharacterInputHandler()
	{
		EnableInputs();
	}
	
	~CharacterInputHandler()
	{
		DisableInputs();
	}
	
	public void Update()
	{
		OnMove?.Invoke(movement.ReadValue<Vector2>(), verticalMovement.ReadValue<float>());
		OnRotate?.Invoke(rotation.ReadValue<Vector2>());
	}
	
	public void EnableInputs()
	{
		if (inputs == null)
		{
			inputs = new PlayerInputs();
			movement = inputs.Player.Movement;	
			verticalMovement = inputs.Player.VerticalMovement;
			rotation = inputs.Player.Rotation;
			attack = inputs.Player.Attack;
			stun = inputs.Player.Sing;
			boost = inputs.Player.Boost;
		}
		
		movement.Enable();
		verticalMovement.Enable();
		rotation.Enable();
		attack.Enable();
		stun.Enable();
		boost.Enable();
		
		attack.performed += context => OnAttack?.Invoke();
		stun.performed += context => OnStun?.Invoke();
		boost.performed += context => OnBoost?.Invoke();
	}
	
	public void DisableInputs()
	{
		movement.Disable();
		verticalMovement.Disable();
		rotation.Disable();
		attack.Disable();
		stun.Disable();
		boost.Disable();
		
		attack.performed -= context => OnAttack?.Invoke();
		stun.performed -= context => OnStun?.Invoke();
		boost.performed -= context => OnBoost?.Invoke();
	}
}
