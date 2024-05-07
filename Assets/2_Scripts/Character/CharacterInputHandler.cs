using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : ICharacterInputHandler
{
	// Events
    public event ICharacterInputHandler.MoveEventHandler OnMove;
    public event ICharacterInputHandler.RotateEventHandler OnRotate;
	
	// Inputs
	private PlayerInputs inputs;
	private InputAction movement;
	private InputAction verticalMovement;
	private InputAction rotation;

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
		}
		
		movement.Enable();
		verticalMovement.Enable();
		rotation.Enable();
	}
	
	public void DisableInputs()
	{
		movement.Disable();
		verticalMovement.Disable();
		rotation.Disable();
	}
}
